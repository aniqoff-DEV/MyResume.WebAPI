using Microsoft.AspNetCore.Http;
using MyResume.Domain.Models;

namespace MyResume.Domain.Interfaces.Repositories
{
    public interface IVacancyService
    {       
        Task<Vacancy> GetByEmployerId(Guid employerId);
        Task<Guid> CreateVacancy(Vacancy vacancy);
        Task DeleteVacancy(Guid employerId);
        Task UpdateVacancy(Guid employerId, IFormFile file);
    }
}
