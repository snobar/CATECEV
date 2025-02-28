using Microsoft.AspNetCore.Mvc;
using CATECEV.API.Models.AMPECO;
using CATECEV.CORE.Framework;
using CATECEV.API.Helper.IService;
namespace CATECEV.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IHttpClientService _httpClientService;
        private readonly IUser _user;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpClientService httpClientService, IUser user)
        {
            _logger = logger;
            _httpClientService = httpClientService;
            _user = user;
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
            var userData = await _user.GetUsers();


            return Ok(userData);
        }
    }
}
