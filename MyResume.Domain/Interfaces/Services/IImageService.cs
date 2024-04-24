using Microsoft.AspNetCore.Http;
using MyResume.Domain.Models;

namespace MyResume.Domain.Interfaces.Services
{
    public interface IImageService
    {
        Task<Avatar> GetAvatarById(Guid id);
        Task<Guid> CreateAvatar(Avatar avatar);
        Task DeleteAvatar(Guid id);
        Task UpdateAvatar(Guid id, IFormFile imageFile);
    }
}
