using Dapper;
using MyResume.Domain.Dtos;
using MyResume.Domain.Interfaces.Repositories;
using MyResume.Domain.Models;
using MyResume.Infrasctructure.Entities;
using Npgsql;

namespace MyResume.Infrasctructure.Repositories
{
    public class EmployerRepository : IEmployerRepository
    {
        private NpgsqlConnection connection;

        public EmployerRepository()
        {
            connection = new NpgsqlConnection(NpgsqlConfig.CONNECTION_STRING);
            connection.Open();
        }

        public async Task<Guid> Create(Employer employer)
        {
            var newEmployer = new EmployerEntity()
            {
                Id = employer.Id,
                CompanyName = employer.CompanyName,
                Description = employer.Description,
                Email = employer.Email.Value,
                Password = employer.Password.Value,
                Address = employer.Address,
                PhoneNumber = employer.PhoneNumber.Number,
                Reputation = employer.Reputation,
                CountFeedBack = employer.CountFeedBack,
                AvatarId = employer.AvatarId,
                CityId = employer.CityId
            };

            string sql = $"INSERT INTO {nameof(Employer)}" +
                $" (id, company_name, description, email, password, phone_number," +
                $" count_feedback, reputation, avatar_id, city_id, address)" +
                $" VALUES (@Id, @CompanyName, @Description, @Email, @Password, @PhoneNumber," +
                $" @CountFeedBack, @Reputation, @AvatarId, @CityId, ( SELECT ct.name || ' ' || ci.name " +
                $"FROM city ci INNER JOIN country ct ON ct.id = ci.country_id WHERE ci.id = {employer.CityId} ))" +
                $" RETURNING id;";

            var newJobSeekerId = await connection.QuerySingleAsync<Guid>(sql, newEmployer);

            return newJobSeekerId;
        }

        public async Task<EmployerDto> GetCompanyCardById(Guid employerId)
        {
            string sql = $"SELECT e.id Id, e.company_name CompanyName, e.description Description, " +
                $"e.address Address,  e.reputation Reputation, e.email Email, " +
                $"e.count_feedback CountFeedBack, a.image_file Avatar " +
                $"FROM {nameof(Employer)} e " +
                $"LEFT JOIN {nameof(Avatar)} a ON a.id = e.avatar_id " +
                $"WHERE e.id = '{employerId}';";

            var companyCard = await connection.QuerySingleAsync<EmployerDto>(sql);
            return companyCard;
        }

        public async Task<List<EmployerDto>> GetCompanyCards()
        {
            string sql = $"SELECT e.id Id, e.company_name CompanyName, e.description Description, " +
                $"e.address Address, e.reputation Reputation, e.email Email, " +
                $"e.count_feedback CountFeedBack, a.image_file Avatar " +
                $"FROM {nameof(Employer)} e " +
                $"LEFT JOIN {nameof(Avatar)} a ON a.id = e.avatar_id;";

            var companyCards = await connection.QueryAsync<EmployerDto>(sql);
            return companyCards.ToList();
        }
    }
}
