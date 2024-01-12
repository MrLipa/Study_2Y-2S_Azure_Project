using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

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
    }
}
