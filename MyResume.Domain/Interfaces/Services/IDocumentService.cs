using Microsoft.AspNetCore.Http;
using MyResume.Domain.Models;

namespace MyResume.Domain.Services.Repositories
{
    public interface IDocumentService
    {
        Task<Resume> GetByJobSeekerId(Guid jobSeekerId);
        Task CreateResume(Resume resume);
        Task DeleteResume(Guid jobSeekerId);
        Task UpdateResume(Guid jobSeekerId, IFormFile file);

        Task<Vacancy> GetByEmployerId(Guid employerId);
        Task CreateVacancy(Vacancy vacancy);
        Task DeleteVacancy(Guid employerId);
        Task UpdateVacancy(Guid employerId, IFormFile file);
    }
}
