using MyResume.Domain.Models;

namespace MyResume.Domain.Interfaces.Repositories
{
    public interface ICountryRepository
    {
        Task<List<Country>> GetCountries();
        Task<Country> GetCountryById(int countryId);
        Task<int> CreateCountry(Country country);
    }
}
