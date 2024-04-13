using Microsoft.AspNetCore.Http;
using MyResume.Domain.Models;

namespace MyResume.Domain.Interfaces.Repositories
{
    public interface IResumeRepository
    {
        Task<Resume> GetByJobSeekerId(Guid jobSeekerId);
        Task CreateResume(Resume resume);
        Task DeleteResume(Guid jobSeekerId);
        Task UpdateResume(Guid jobSeekerId, IFormFile file);
    }
}
