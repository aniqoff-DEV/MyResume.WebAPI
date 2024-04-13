using MyResume.Domain.Models;

namespace MyResume.Domain.Interfaces.Repositories
{
    public interface ICityRepository
    {
        Task<List<City>> GetCities();
        Task<City> GetCityById(int cityId);
        Task CreateCity(City city);
    }
}
