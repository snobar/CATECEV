using Microsoft.AspNetCore.Mvc;
using CATECEV.API.Models.AMPECO;
namespace CATECEV.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IHttpClientService _httpClientService;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpClientService httpClientService)
        {
            _logger = logger;
            _httpClientService = httpClientService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("GetUserListTest")]
        public async Task<IActionResult> GetUserListTest()
        {
            var userData = await _httpClientService.GetAsync<Data<IEnumerable<Models.AMPECO.resource.users.User>>>("https://shabikuae.eu.charge.ampeco.tech/public-api/resources/users/v1.0", "38ed9669-94ca-4c80-9cc9-8ac4fd549d5f");


            return Ok(userData);
        }
    }
}
