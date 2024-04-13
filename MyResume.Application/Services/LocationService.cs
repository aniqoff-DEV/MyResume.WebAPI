using MyResume.Domain.Interfaces.Repositories;
using MyResume.Domain.Models;
using MyResume.Domain.Services.Repositories;

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

        public Task CreateCity(City city)
        {
            throw new NotImplementedException();
        }

        public Task CreateCountry(Country country)
        {
            throw new NotImplementedException();
        }

        public Task<List<City>> GetCities()
        {
            throw new NotImplementedException();
        }

        public Task<City> GetCityById(int cityId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Country>> GetCountries()
        {
            var countries = await _countryRepository.GetCountries();
            return countries;
        }

        public Task<Country> GetCountryById(int countryId)
        {
            throw new NotImplementedException();
        }
    }
}
