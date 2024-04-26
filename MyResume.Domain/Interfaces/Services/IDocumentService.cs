using Microsoft.AspNetCore.Http;
using MyResume.Domain.Models;

namespace MyResume.Domain.Interfaces.Services
{
    public interface IDocumentService
    {
        Task<Resume> GetByResumeId(Guid resumeId);
        Task<Guid> CreateResume(Resume resume);
        Task DeleteResume(Guid resumeId);
        Task UpdateResume(Guid resumeId, IFormFile file);

        Task<Vacancy> GetByVcancyId(Guid employerId);
        Task<Guid> CreateVacancy(Vacancy vacancy);
        Task DeleteVacancy(Guid employerId);
        Task UpdateVacancy(Guid employerId, IFormFile file);
    }
}
