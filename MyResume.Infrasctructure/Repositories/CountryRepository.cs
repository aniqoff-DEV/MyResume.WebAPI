using Dapper;
using MyResume.Domain.Interfaces.Repositories;
using MyResume.Domain.Models;
using MyResume.Infrasctructure.Entities;
using Npgsql;

namespace MyResume.Infrasctructure.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private NpgsqlConnection connection;

        public CountryRepository()
        {
            connection = new NpgsqlConnection(NpgsqlConfig.CONNECTION_STRING);
            connection.Open();
        }

        public async Task<int> CreateCountry(Country country)
        {
            var countryEntity = new CountryEntity
            {
                Id = country.Id,
                Name = country.Name,
            };

            string sql = $"INSERT INTO {nameof(Country)} (name) VALUES (@Name) RETURNING id;";
            var countryId = await connection.QueryAsync<int>(sql, countryEntity);

            return countryId.FirstOrDefault();
        }

        public async Task DeleteCountry(int countryId)
        {
            string sql = $"BEGIN; DELETE FROM city WHERE country_id IN (SELECT id FROM country WHERE id = {countryId}); " +
                $"DELETE FROM country WHERE id = {countryId}; COMMIT;";

            await connection.ExecuteAsync(sql);

            return;
        }

        public async Task<List<Country>> GetCountries()
        {
            var countries = await connection.QueryAsync<Country>($"SELECT * FROM {nameof(Country)}");
            return countries.ToList();
        }

        public async Task<Country> GetCountryById(int countryId)
        {
            var country = await connection.QuerySingleAsync<Country>($"SELECT * FROM {nameof(Country)} WHERE id = {countryId}");

            return country;
        }
    }
}
