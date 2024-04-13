using MyResume.Domain.Models;

namespace MyResume.Domain.Services.Repositories
{
    public interface ILocationService
    {
        Task<List<City>> GetCities();
        Task<List<Country>> GetCountries();
        Task<Country> GetCountryById(int countryId);
        Task<City> GetCityById(int cityId);
        Task CreateCity(City city);
        Task CreateCountry(Country country);
    }
}
