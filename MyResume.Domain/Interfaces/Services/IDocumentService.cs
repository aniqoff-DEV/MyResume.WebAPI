using Microsoft.AspNetCore.Http;
using MyResume.Domain.Models;

namespace MyResume.Domain.Interfaces.Services
{
    public interface IDocumentService
    {
        Task<Resume> GetByJobSeekerId(Guid jobSeekerId);
        Task<Guid> CreateResume(Resume resume);
        Task DeleteResume(Guid jobSeekerId);
        Task UpdateResume(Guid jobSeekerId, IFormFile file);

        Task<Vacancy> GetByEmployerId(Guid employerId);
        Task<Guid> CreateVacancy(Vacancy vacancy);
        Task DeleteVacancy(Guid employerId);
        Task UpdateVacancy(Guid employerId, IFormFile file);
    }
}
