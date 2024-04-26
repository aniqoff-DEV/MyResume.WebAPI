using MyResume.Domain.Dtos;
using MyResume.Domain.Models;

namespace MyResume.Domain.Interfaces.Services
{
    public interface ILocationService
    {
        Task<List<CityDto>> GetCities(int countryId);
        Task<List<Country>> GetCountries();
        Task<Country> GetCountryById(int countryId);
        Task<CityDto> GetCityById(int cityId);
        Task<int> CreateCity(City city);
        Task<int> CreateCountry(Country country);
        Task DeleteCountry(int countryId);
    }
}
