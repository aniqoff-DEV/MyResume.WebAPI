using Microsoft.AspNetCore.Mvc;
using MyResume.Domain.Services.Repositories;
using MyResume.Infrasctructure.Entities;

namespace MyResume.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet("country/all")]
        public async Task<ActionResult<IEnumerable<CountryEntity>>> GetContries()
        {
            var countries = await _locationService.GetCountries();
            return Ok(countries);
        }
    }
}
