using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;
using Project.Models;
using Project.Repositories;
using Project.Interfaces;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MealPreparationController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IProductRepository _productRepository;
        private readonly IMealRepository _mealRepository;
        private readonly IMealProductRepository _mealProductRepository;


        public MealPreparationController(IHttpClientFactory httpClientFactory, IProductRepository productRepository, IMealRepository mealRepository, IMealProductRepository mealProductRepository)
        {
            _httpClient = httpClientFactory.CreateClient();
            _productRepository = productRepository;
            _mealRepository = mealRepository;
            _mealProductRepository = mealProductRepository;
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

            var response = await _httpClient.PostAsync("http://localhost:7071/api/http_trigger", content);
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
    }
}
