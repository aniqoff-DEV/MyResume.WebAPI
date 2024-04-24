using Microsoft.AspNetCore.Http;
using MyResume.Domain.Models;

namespace MyResume.Domain.Interfaces.Repositories
{
    public interface IImageRepository
    {
        Task<Avatar> GetByIdAvatar(Guid id);
        Task<Guid> CreateAvatar(Avatar avatar);
        Task DeleteAvatar(Guid id);
        Task UpdateAvatar(Guid id, IFormFile imageFile);
    }
}
