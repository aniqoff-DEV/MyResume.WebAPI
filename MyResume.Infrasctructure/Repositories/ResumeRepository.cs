using Dapper;
using Microsoft.AspNetCore.Http;
using MyResume.Domain.Interfaces.Repositories;
using MyResume.Domain.Models;
using MyResume.Infrasctructure.Entities;
using Npgsql;

namespace MyResume.Infrasctructure.Repositories
{
    public class ResumeRepository : IResumeRepository
    {
        private NpgsqlConnection connection;

        public ResumeRepository()
        {
            connection = new NpgsqlConnection(NpgsqlConfig.CONNECTION_STRING);
            connection.Open();
        }

        public async Task<Guid> CreateResume(Resume resume)
        {
            byte[] imageData = FileConverter.ToBytes(resume.File);

            var newResume = new ResumeEntity()
            {
                Id = resume.Id,
                File = imageData,
                FileName = resume.FileName
            };

            string sql = $"INSERT INTO {nameof(Resume)} (id, file_name, file) VALUES (@Id, @FileName, @File) RETURNING id;";
            var newResumeId = await connection.QuerySingleAsync<Guid>(sql, newResume);

            return newResumeId;
        }

        public async Task DeleteResume(Guid resumeId)
        {
            string sqlQuery = $"DELETE FROM {nameof(Resume)} WHERE id = '{resumeId}';";
            await connection.ExecuteAsync(sqlQuery);

            return;
        }

        public async Task<Resume> GetByResumeId(Guid resumeId)
        {
            var resumeEntity = await connection.QuerySingleAsync<ResumeEntity>($"SELECT id, file_name FileName,file File" +
                $" FROM {nameof(Resume)} WHERE id = '{resumeId}';");

            var file = FileConverter.ToFile(resumeEntity.File, resumeEntity.FileName);

            var resume = Resume.Create(
                resumeId,
                resumeEntity.FileName,
                file);

            return resume.Value;
        }

        public async Task UpdateResume(Guid resumeId, IFormFile file)
        {
            byte[] imageData = FileConverter.ToBytes(file);

            var resumeToUpdate = new ResumeEntity()
            {
                Id = resumeId,
                File = imageData,
                FileName = file.FileName
            };

            string sqlQuery = $"UPDATE {nameof(Resume)} SET file = @File, file_name = @FileName WHERE id = '{resumeId}';";
            await connection.ExecuteAsync(sqlQuery, resumeToUpdate);
            return;
        }
    }
}
