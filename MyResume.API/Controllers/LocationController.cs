﻿using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyResume.API.Contracts.Requests;
using MyResume.API.Contracts.Responses;
using MyResume.Domain.Dtos;
using MyResume.Domain.Interfaces.Services;
using MyResume.Domain.Models;

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

        #region City
        [HttpGet("citydto/all/bycountry/id={countryId}")]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetCityDtos(int countryId)
        {
            var cities = await _locationService
                .GetCityDtos(countryId);

            if (cities is null)
                return BadRequest();

            return Ok(cities);
        }

        [HttpGet("city/all/bycountry/id={countryId}")]
        public async Task<ActionResult<IEnumerable<City>>> GetCities(int countryId)
        {
            var cities = await _locationService
                .GetCities(countryId);

            if (cities is null)
                return BadRequest();

            return Ok(cities);
        }

        [HttpGet("city/id={cityId}")]
        public async Task<ActionResult<CityDto>> GetCity(int cityId)
        {
            var city = await _locationService.GetCityById(cityId);

            if (city is null)
                return NotFound();

            return Ok(city);
        }

        [HttpPost("city/insert")]
        public async Task<ActionResult<CityResponse>> InsertCity([FromBody] CityRequest cityRequest)
        {
            var city = City.Create(0,cityRequest.CountryId, cityRequest.Name);

            if (!city.IsSuccess)
                return BadRequest(city.Error);

            var countryId = await _locationService.CreateCity(city.Value);

            if (countryId <= 0)
                return BadRequest();

            return Created(Request.GetDisplayUrl(), new CityResponse(
                countryId,
                cityRequest.Name));
        }
        #endregion

        #region Country
        [HttpGet("country/all")]
        public async Task<ActionResult<IEnumerable<CountryResponse>>> GetContries()
        {
            var countries = await _locationService
                .GetCountries();

            var response = countries.Select( c =>
                new CountryResponse(c.Id,c.Name));

            if (response.IsNullOrEmpty())
                return NotFound();

            return Ok(response);
        }

        [HttpGet("country/id={countryId}")]
        public async Task<ActionResult<CountryResponse>> GetContry(int countryId)
        {
            var country = await _locationService.GetCountryById(countryId);

            if (country is null)
                return NotFound();

            var response = new CountryResponse(country.Id, country.Name);

            return Ok(response);
        }

        [HttpPost("country/insert")]
        public async Task<ActionResult<CountryResponse>> InsertContry([FromBody] CountryRequest countryRequest)
        {
            var country = Country.Create(0,countryRequest.Name);

            if(!country.IsSuccess)
                return BadRequest(country.Error);

            var countryId = await _locationService.CreateCountry(country.Value);

            if (countryId <= 0)
                return BadRequest();
            
            return Created(Request.GetDisplayUrl(), new CountryResponse(countryId,countryRequest.Name));
        }

        [HttpDelete("country/delete/id={countryId}")]
        public async Task<ActionResult> DeleteCountry(int countryId)
        {
            try
            {
                await _locationService.DeleteCountry(countryId);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        #endregion
    }
}
