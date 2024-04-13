using Microsoft.AspNetCore.Http;
using MyResume.Domain.Models;

namespace MyResume.Domain.Interfaces.Repositories
{
    public interface IImageService
    {
        Task<Avatar> GetById(Guid id);
        Task Create(Avatar avatar);
        Task Delete(Guid id);
        Task Update(Guid id, IFormFile imageFile);
    }
}
