using CSharpFunctionalExtensions;
using Dapper;
using MyResume.Domain.Dtos;
using MyResume.Domain.Interfaces.Repositories;
using MyResume.Domain.Models;
using MyResume.Infrasctructure.Entities;
using Npgsql;

namespace MyResume.Infrasctructure.Repositories
{
    public class VacancyRepository : IVacancyRepository
    {
        private NpgsqlConnection connection;

        public VacancyRepository()
        {
            connection = new NpgsqlConnection(NpgsqlConfig.CONNECTION_STRING);
            connection.Open();
        }

        public async Task<Guid> Create(Vacancy vacancy)
        {
            byte[] imageData = FileConverter.ToBytes(vacancy.File);

            var newVacancy = new VacancyEntity()
            {
                Id = vacancy.Id,
                EmployerId = vacancy.EmployerId,
                BranchId = vacancy.BranchId,
                Experience = vacancy.Experience.Value,
                Employment = vacancy.Employment.Value,
                ScheduleWork = vacancy.ScheduleWork.Value,
                Salary = vacancy.Salary,
                File = imageData,
                FileName = vacancy.FileName
            };

            string sql = $"INSERT INTO {nameof(Vacancy)} (id, employer_id, branch_id, experience, employment_type, schedule_work, file_name, file, salary) " +
                $"VALUES (@Id, @EmployerId, @BranchId, @Experience, @Employment, @ScheduleWork, @FileName, @File, @Salary) RETURNING id;";
            var newVacancyId = await connection.QuerySingleAsync<Guid>(sql, newVacancy);

            return newVacancyId;
        }

        public async Task Delete(Guid vacancyId)
        {
            string sqlQuery = $"DELETE FROM {nameof(Vacancy)} WHERE id = '{vacancyId}';";
            await connection.ExecuteAsync(sqlQuery);

            return;
        }

        public async Task<List<InfoOnCardVacancyDto>> GetInfoOnCardList()
        {
            var vacancyEntity = await connection.QueryAsync<InfoOnCardVacancyDto>($"SELECT v.id Id, e.company_name CompanyName, e.description Description, b.name BranchName, " +
                $"e.address Address, v.experience Experience, v.employment_type Employment, v.schedule_work ScheduleWork, v.salary Salary, a.image_file Avatar " +
                $"FROM {nameof(Vacancy)} v " +
                $"LEFT JOIN branch b ON b.id = v.branch_id " +
                $"LEFT JOIN employer e ON e.id = v.employer_id " +
                $"LEFT JOIN avatar a ON a.id = e.avatar_id;");

            return vacancyEntity.ToList();
        }

        public Task Update(Guid vacancyId, int BranchId, string experience, string employment, string scheduleWork, int salary, string fileName, byte[] file)
        {
            throw new NotImplementedException();
        }
    }
}
