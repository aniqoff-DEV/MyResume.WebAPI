using Dapper;
using MyResume.Domain.Dtos;
using MyResume.Domain.Interfaces.Repositories;
using MyResume.Domain.Models;
using MyResume.Infrasctructure.Entities;
using Npgsql;

namespace MyResume.Infrasctructure.Repositories
{
    public class JobSeekerRepository : IJobSeekerRepository
    {
        public const string TABLE_NAME = "job_seeker";
        private NpgsqlConnection connection;

        public JobSeekerRepository()
        {
            connection = new NpgsqlConnection(NpgsqlConfig.CONNECTION_STRING);
            connection.Open();
        }

        public async Task<Guid> Create(JobSeeker jobSeeker)
        {
            var newJobSeeker = new JobSeekerEntity()
            {
                Id = jobSeeker.Id,
                FullName = jobSeeker.FullName,
                Description = jobSeeker.Description,
                Email = jobSeeker.Email.Value,
                Password = jobSeeker.Password.Value,
                PhoneNumber = jobSeeker.PhoneNumber.Number,
                AvatarId = jobSeeker.AvatarId,
                BranchId = jobSeeker.BranchId,
                CityId =  jobSeeker.CityId,
                ResumeId = jobSeeker.ResumeId
            };

            string sql = $"INSERT INTO {TABLE_NAME}" +
                $" (id, full_name, description, email, password, phone_number," +
                $" count_feedback, reputation, avatar_id, city_id, branch_id, resume_id)" +
                $" VALUES (@Id, @FullName, @Description, @Email, @Password, @PhoneNumber," +
                $" @CountFeedBack, @Reputation, @AvatarId, @CityId, @BranchId, @ResumeId)" +
                $" RETURNING id;";

            var newJobSeekerId = await connection.QuerySingleAsync<Guid>(sql, newJobSeeker);

            return newJobSeekerId;
        }

        public async Task Delete(Guid id)
        {
            string sqlQuery = $"BEGIN;DELETE FROM avatar WHERE id IN (SELECT avatar_id FROM job_seeker WHERE id = '{id}'); " +
                $"DELETE FROM resume WHERE id IN (SELECT resume_id FROM job_seeker WHERE id = '{id}'); " +
                $"DELETE FROM job_seeker WHERE id = '{id}';COMMIT;";
            await connection.ExecuteAsync(sqlQuery);

            return;
        }

        public async Task<InfoOnCardJobSeekerDto> GetInfoOnCardJobSeekerById(Guid jobSeekerId)
        {
            string sql = "SELECT js.id Id, js.full_Name FullName, js.description Description, b.name BranchName, c.name CityName " +
                "FROM job_seeker js " +
                "LEFT JOIN branch b ON b.id = js.branch_id " +
                "LEFT JOIN city c ON c.id = js.city_id " +
                $"WHERE js.id = '{jobSeekerId}';";

            var jobSeeker = await connection.QuerySingleAsync<InfoOnCardJobSeekerDto>(sql);
            return jobSeeker;
        }

        public async Task<List<InfoOnCardJobSeekerDto>> GetInfoOnCardJobSeekerOnList()
        {
            string sql = "SELECT js.id Id, js.full_Name FullName, js.description Description, b.name BranchName, c.name CityName " +
                "FROM job_seeker js " +
                "LEFT JOIN branch b ON b.id = js.branch_id " +
                "LEFT JOIN city c ON c.id = js.city_id;";

            var jobSeeker = await connection.QueryAsync<InfoOnCardJobSeekerDto>(sql);
            return jobSeeker.ToList();
        }

        public async Task<List<JobSeeker>> GetRawJobSeekers()
        {
            var jobSeekers = await connection.QueryAsync<JobSeeker>($"SELECT * FROM {TABLE_NAME}");           

            return jobSeekers.ToList();
        }

        public async Task<JobSeeker> GetByIdRawJobSeeker(Guid id)
        {
            var jobSeeker = await connection.QuerySingleAsync<JobSeeker>($"SELECT * FROM {TABLE_NAME} WHERE id = '{id}';");

            return jobSeeker;
        }
        // TODO: remove to CQRS include some methods such as UpdatePersonalData(email and password) and maybe UpdateResume
        public async Task UpdateInfo(Guid id,
                                       string fullName,
                                       string? description,
                                       string? phoneNumber,
                                       Guid? avatarId,
                                       int? cityId,
                                       int? branchId)
        {
            string sqlQuery = $"UPDATE {TABLE_NAME} SET full_name = @fullName, description = @description, phone_number = @string, avatar_id = @avatarId," +
                $" city_id = @cityId, branch_id = @branchId, WHERE id = '{id}';"; //to PATCH
            await connection.ExecuteAsync(sqlQuery, new { fullName , description, phoneNumber, avatarId, cityId, branchId });
            return;
        }
    }
}
