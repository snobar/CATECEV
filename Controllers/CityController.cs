using hijazi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hijazi.Controllers
{
    public class CityController : Controller
    {
        private readonly ILogger<CityController> _logger;
        private readonly AppDBContext _appContext;

        public CityController(ILogger<CityController> logger, AppDBContext appContext)
        {
            _logger = logger;
            _appContext = appContext;
        }

        [HttpGet]
        public JsonResult GetCitiesByCountry(int countryId)
        {
            // Retrieve cities based on the selected country
            var cities = _appContext.City
                                 .Where(c => c.CountryId == countryId)
                                 .Select(c => new { c.Id, Name = c.EnglishName })  // Adjust the properties as needed
                                 .ToList();

            // Return the cities as JSON
            return Json(cities);
        }
    }
}
