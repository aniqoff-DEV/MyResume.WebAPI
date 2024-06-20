using Dapper;
using MyResume.Domain.Dtos;
using MyResume.Domain.Interfaces.Repositories;
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

        public async Task<Guid> Create(JobSeekerDto jobSeeker)
        {
            string sql = $"INSERT INTO {TABLE_NAME}" +
                $" (id, full_name, description, email, password_hash, phone_number, desired_salary, " +
                $" count_feedback, reputation, avatar_id, city_id, branch_id, resume_id)" +
                $" VALUES (@Id, @FullName, @Description, @Email, @PasswordHash, @PhoneNumber, @DesiredSalary, " +
                $" @CountFeedBack, @Reputation, @AvatarId, @CityId, @BranchId, @ResumeId)" +
                $" RETURNING id;";

            var newJobSeekerId = await connection.QuerySingleAsync<Guid>(sql, jobSeeker);

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
            string sql = "SELECT js.id Id, js.full_Name FullName, js.description Description," +
                " a.image_file Avatar, b.name BranchName, c.name CityName, js.desired_salary DesiredSalary " +
                "FROM job_seeker js " +
                "LEFT JOIN branch b ON b.id = js.branch_id " +
                "LEFT JOIN avatar a ON a.id = js.avatar_id " +
                "LEFT JOIN city c ON c.id = js.city_id " +
                $"WHERE js.id = '{jobSeekerId}';";

            var jobSeeker = await connection.QuerySingleAsync<InfoOnCardJobSeekerDto>(sql);
            return jobSeeker;
        }

        public async Task<List<InfoOnCardJobSeekerDto>> GetInfoOnCardJobSeekerOnList()
        {
            string sql = "SELECT js.id Id, js.full_Name FullName, js.description Description, a.image_file Avatar," +
                " b.name BranchName, c.name CityName, js.desired_salary DesiredSalary " +
                "FROM job_seeker js " +
                "LEFT JOIN branch b ON b.id = js.branch_id " +
                "LEFT JOIN avatar a ON a.id = js.avatar_id " +
                "LEFT JOIN city c ON c.id = js.city_id;";

            var jobSeeker = await connection.QueryAsync<InfoOnCardJobSeekerDto>(sql);
            return jobSeeker.ToList();
        }

        public async Task<InfoOnPageJobSeekerDto> GetInfoOnPageJobSeekerById(Guid jobSeekerId)
        {
            string sql = "SELECT js.id Id, js.full_Name FullName, js.description Description, js.email Email, " +
                "js.reputation Reputation, js.count_feedback CountFeedBack, a.image_file Avatar, r.file Resume, js.desired_salary DesiredSalary, " +
                "b.name Branch, c.name City " +
                $"FROM {TABLE_NAME} js " +
                "LEFT JOIN branch b ON b.id = js.branch_id " +
                "LEFT JOIN avatar a ON a.id = js.avatar_id " +
                "LEFT JOIN city c ON c.id = js.city_id " +
                "LEFT JOIN resume r ON r.id = js.resume_id " +
                $"WHERE js.id = '{jobSeekerId}';";

            var jobSeekers = await connection.QuerySingleAsync<InfoOnPageJobSeekerDto>(sql);

            return jobSeekers;
        }

        public async Task<JobSeekerDto> GetByEmail(string email)
        {
            var jobSeeker = await connection.QuerySingleAsync<JobSeekerDto>(
                $"SELECT id Id, full_Name FullName, description Description, email Email, password_hash PasswordHash," +
                $" phone_number PhoneNumber, count_feedback CountFeedBack, reputation Reputation, avatar_id AvatarId," +
                $" city_id CityId, branch_id BranchId, resume_id ResumeId, desired_salary DesiredSalary" +
                $" FROM {TABLE_NAME} WHERE email= '{email}';");

            return jobSeeker;
        }
        
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
