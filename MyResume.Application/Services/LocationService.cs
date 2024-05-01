using MyResume.Domain.Dtos;
using MyResume.Domain.Interfaces.Repositories;
using MyResume.Domain.Interfaces.Services;
using MyResume.Domain.Models;

namespace MyResume.Application.Services
{
    public class LocationService : ILocationService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly ICityRepository _cityRepository;

        public LocationService(ICountryRepository countryRepository, ICityRepository cityRepository)
        {
            _countryRepository = countryRepository;
            _cityRepository = cityRepository;
        }

        public async Task<int> CreateCity(City city)
        {
            var cityId = await _cityRepository.CreateCity(city);
            return cityId;
        }

        public async Task<int> CreateCountry(Country country)
        {
            var countryId = await _countryRepository.CreateCountry(country);
            return countryId;
        }

        public async Task DeleteCountry(int countryId)
        {
            await _countryRepository.DeleteCountry(countryId);
            return;
        }

        public async Task<List<CityDto>> GetCityDtos(int countryId)
        {
            var cities = await _cityRepository.GetCityDtos(countryId);
            return cities;
        }

        public async Task<List<City>> GetCities(int countryId)
        {
            var cities = await _cityRepository.GetCities(countryId);
            return cities;
        }

        public async Task<CityDto> GetCityById(int cityId)
        {
            var city = await _cityRepository.GetCityById(cityId);
            return city;
        }

        public async Task<List<Country>> GetCountries()
        {
            var countries = await _countryRepository.GetCountries();
            return countries;
        }

        public async Task<Country> GetCountryById(int countryId)
        {
            var countries = await _countryRepository.GetCountryById(countryId);
            return countries;
        }
    }
}
