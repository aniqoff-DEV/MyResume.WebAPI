using Microsoft.AspNetCore.Http;
using MyResume.Domain.Models;

namespace MyResume.Domain.Interfaces.Repositories
{
    public interface IResumeRepository
    {
        Task<Resume> GetByResumeId(Guid resumeId);
        Task<Guid> CreateResume(Resume resume);
        Task DeleteResume(Guid resumeId);
        Task UpdateResume(Guid resumeId, IFormFile file);
    }
}
