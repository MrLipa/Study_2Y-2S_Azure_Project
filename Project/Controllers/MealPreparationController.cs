using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;
using Project.Models;
using Project.Repositories;
using Project.Interfaces;
using Project.Helper;
using Microsoft.Extensions.Options;
using Azure.Messaging.EventGrid;
using System.Configuration;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MealPreparationController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly ExternalApiSettings _externalApiSettings;
        private readonly IProductRepository _productRepository;
        private readonly IMealRepository _mealRepository;
        private readonly IMealProductRepository _mealProductRepository;
        private readonly IConfiguration _configuration;


        public MealPreparationController(IConfiguration configuration, IHttpClientFactory httpClientFactory, IProductRepository productRepository, IMealRepository mealRepository, IMealProductRepository mealProductRepository, IOptions<ExternalApiSettings> externalApiSettings)
        {
            _httpClient = httpClientFactory.CreateClient();
            _productRepository = productRepository;
            _mealRepository = mealRepository;
            _mealProductRepository = mealProductRepository;
            _externalApiSettings = externalApiSettings.Value;
            _configuration = configuration;
        }

        public class MealPreparationRequest
        {
            public string MealName { get; set; }
            public List<int> ProductIds { get; set; }
            public NutritionalLimits Limits { get; set; }
        }

        public class NutritionalLimits
        {
            public int MinCalories { get; set; }
            public int MaxCalories { get; set; }
            public int Protein { get; set; }
            public int Fat { get; set; }
            public int Carbohydrates { get; set; }
        }

        public class MealProductInfo
        {
            public int ProductId { get; set; }
            public string Name { get; set; }
            public float QuantityInGrams { get; set; }
        }

        [HttpPost("prepare")]
        public async Task<IActionResult> PrepareMeal(MealPreparationRequest request)
        {
            var products = request.ProductIds
                .Select(id => _productRepository.GetProductById(id))
                .Where(p => p != null)
                .ToList();

            var payload = new { products, limits = request.Limits };
            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var url = _externalApiSettings.BaseUrl;

            var response = await _httpClient.PostAsync(url, content);
            // await SendEventToEventGrid(JsonConvert.SerializeObject(payload));

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var productQuantities = JsonConvert.DeserializeObject<List<MealProductInfo>>(responseContent);

                var meal = new Meal { Name = request.MealName };
                _mealRepository.AddMeal(meal);

                foreach (var productQuantity in productQuantities)
                {
                    var mealProduct = new MealProduct
                    {
                        MealId = meal.MealId,
                        ProductId = productQuantity.ProductId,
                        QuantityInGrams = productQuantity.QuantityInGrams
                    };
                    _mealProductRepository.AddMealProduct(mealProduct);
                }

                return Ok("Posiłek został przygotowany pomyślnie.");
            }
            else
            {
                return StatusCode((int)response.StatusCode, "Error calling the external API");
            }
        }

        private async Task SendEventToEventGrid(object data)
        {
            string topicEndpoint = _configuration["EventGrid:TopicEndpoint"];
            string topicKey = _configuration["EventGrid:TopicKey"];

            var client = new EventGridPublisherClient(new Uri(topicEndpoint), new Azure.AzureKeyCredential(topicKey));

            var eventData = new EventGridEvent(
                subject: $"meal-preparation/{Guid.NewGuid()}",
                eventType: "MealPreparation.Request",
                dataVersion: "1.0",
                data: data
            );

            await client.SendEventAsync(eventData);
        }

    }
}
