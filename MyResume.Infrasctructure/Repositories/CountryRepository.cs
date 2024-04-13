using Dapper;
using MyResume.Domain.Interfaces.Repositories;
using MyResume.Domain.Models;
using Npgsql;

namespace MyResume.Infrasctructure.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private const string TABLE_NAME = "Countries";
        private NpgsqlConnection connection;

        public CountryRepository()
        {
            connection = new NpgsqlConnection("Host=localhost:5432;" +
          "Username=postgres;" +
          "Password=@niqoff2612;" +
          "Database=myresumedb");
            connection.Open();
        }

        public Task CreateCountry(Country country)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Country>> GetCountries()
        {
            var countries = await connection.QueryAsync<Country>($"SELECT * FROM {TABLE_NAME}");
            return countries.ToList();
        }

        public Task<Country> GetCountryById(int countryId)
        {
            throw new NotImplementedException();
        }
    }
}
