using Dapper;
using MyResume.Domain.Dtos;
using MyResume.Domain.Interfaces.Repositories;
using MyResume.Domain.Models;
using MyResume.Infrasctructure.Entities;
using Npgsql;

namespace MyResume.Infrasctructure.Repositories
{
    public class CityRepository : ICityRepository
    {
        private NpgsqlConnection connection;

        public CityRepository()
        {
            connection = new NpgsqlConnection(NpgsqlConfig.CONNECTION_STRING);
            connection.Open();
        }

        public async Task<int> CreateCity(City city)
        {
            var cityEntity = new CityEntity
            {
                Id = city.Id,
                CountryId = city.CountryId,
                Name = city.Name,
            };

            string sql = $"INSERT INTO {nameof(City)} (name, country_Id) VALUES (@Name, @Country_Id) RETURNING id;";
            var cityId = await connection.QueryAsync<int>(sql, cityEntity);

            return cityId.FirstOrDefault();
        }

        public async Task<List<CityDto>> GetCities(int countryId)
        {
            var cities = await connection.QueryAsync<CityDto>($"SELECT ci.id, ct.name CountryName, ci.name " +
                $"FROM {nameof(City)} ci " +
                $"INNER JOIN {nameof(Country)} ct ON ct.id = ci.country_id WHERE ct.id = @ct.Id;"
                , countryId);

            return cities.ToList();
        }

        public async Task<CityDto> GetCityById(int cityId)
        {
            var city = await connection.QueryAsync<CityDto>($"SELECT ci.id, ct.name CountryName, ci.name " +
                $"FROM {nameof(City)} ci " +
                $"INNER JOIN {nameof(Country)} ct ON ct.id = ci.country_id WHERE ci.id = @ci.Id;"
                , cityId);

            return city.FirstOrDefault()!;
        }
    }
}
