using Dapper;
using Microsoft.AspNetCore.Http;
using MyResume.Domain.Interfaces.Repositories;
using MyResume.Domain.Models;
using MyResume.Infrasctructure.Entities;
using Npgsql;

namespace MyResume.Infrasctructure.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private NpgsqlConnection connection;

        public ImageRepository()
        {
            connection = new NpgsqlConnection(NpgsqlConfig.CONNECTION_STRING);
            connection.Open();
        }

        public async Task<Guid> CreateAvatar(Avatar avatar)
        {
            byte[] imageData = FileConverter.ToBytes(avatar.ImageFile);

            var newAvatar = new AvatarEntity() 
            { 
                Id = avatar.Id,
                ImageFile = imageData,
                FileName = avatar.FileName
            };

            string sql = $"INSERT INTO {nameof(Avatar)} (id, image_file, file_name) VALUES (@Id, @ImageFile, @FileName) RETURNING id;";
            var newAvatarId = await connection.QuerySingleAsync<Guid>(sql, newAvatar);

            return newAvatarId;
        }

        public async Task DeleteAvatar(Guid id)
        {
            string sqlQuery = $"DELETE FROM {nameof(Avatar)} WHERE id = '{id}';";
            await connection.ExecuteAsync(sqlQuery);

            return;
        }

        public async Task<Avatar> GetByIdAvatar(Guid id)
        {
            var avatarEntity = await connection.QuerySingleAsync<AvatarEntity>($"SELECT id, image_file ImageFile,file_name FileName" +
                $" FROM {nameof(Avatar)} WHERE id = '{id}';");

            var file = FileConverter.ToFile(avatarEntity.ImageFile,avatarEntity.FileName);

            var avatar = Avatar.Create(
                id,
                avatarEntity.FileName,
                file);

            return avatar.Value;
        }

        public async Task UpdateAvatar(Guid id, IFormFile imageFile)
        {
            byte[] imageData = FileConverter.ToBytes(imageFile);

            var avatarToUpdate = new AvatarEntity()
            {
                Id = id,
                ImageFile = imageData,
                FileName = imageFile.FileName
            };

            string sqlQuery = $"UPDATE {nameof(Avatar)} SET image_file = @ImageFile, file_name = @FileName WHERE id = '{id}';";
            await connection.ExecuteAsync(sqlQuery, avatarToUpdate);
            return;
        }
    }
}
