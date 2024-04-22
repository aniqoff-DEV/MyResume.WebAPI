using MyResume.Domain.Dtos;
using MyResume.Domain.Models;

namespace MyResume.Domain.Interfaces.Repositories
{
    public interface ICityRepository
    {
        Task<List<CityDto>> GetCities(int countryId);
        Task<CityDto> GetCityById(int cityId);
        Task<int> CreateCity(City city);
    }
}
