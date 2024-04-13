using MyResume.Domain.Interfaces.Repositories;
using MyResume.Domain.Models;

namespace MyResume.Infrasctructure.Repositories
{
    public class CityRepository : ICityRepository
    {
        public Task CreateCity(City city)
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
    }
}
