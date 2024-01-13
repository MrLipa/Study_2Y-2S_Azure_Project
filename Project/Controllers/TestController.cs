using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Azure.Messaging.EventGrid;

namespace Project.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TestController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet()]
        public IActionResult GetConfigValues()
        {

            var baseUrl = _configuration["ExternalApi:BaseUrl"];
            var myDbConnection = _configuration.GetConnectionString("MyDbConnection");

            var configValues = new
            {
                BaseUrl = baseUrl,
                MyDbConnection = myDbConnection
            };

            return Ok(configValues);
        }

        [HttpPost("sendEventToEventGrid")]
        public async Task<IActionResult> SendEventToEventGrid()
        {
            string topicEndpoint = _configuration["EventGrid:TopicEndpoint"];
            string topicKey = _configuration["EventGrid:TopicKey"];

            var client = new EventGridPublisherClient(new Uri(topicEndpoint), new Azure.AzureKeyCredential(topicKey));

            var eventData = new EventGridEvent(
                subject: $"test-event/{Guid.NewGuid()}",
                eventType: "Test.Event",
                dataVersion: "1.0",
                data: new { Message = "Hello from ASP.NET Core!" }
            );

            try
            {
                await client.SendEventAsync(eventData);
                return Ok("Zdarzenie zostało wysłane do Event Grid");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Błąd podczas wysyłania zdarzenia: {ex.Message}");
            }
        }
    }
}
