using Microsoft.AspNetCore.Http;
using MyResume.Domain.Interfaces.Services;
using MyResume.Domain.Models;

namespace MyResume.Application.Services
{
    public class DocumentService : IDocumentService
    {
        public Task<Guid> CreateResume(Resume resume)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateVacancy(Vacancy vacancy)
        {
            throw new NotImplementedException();
        }

        public Task DeleteResume(Guid jobSeekerId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteVacancy(Guid employerId)
        {
            throw new NotImplementedException();
        }

        public Task<Vacancy> GetByEmployerId(Guid employerId)
        {
            throw new NotImplementedException();
        }

        public Task<Resume> GetByJobSeekerId(Guid jobSeekerId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateResume(Guid jobSeekerId, IFormFile file)
        {
            throw new NotImplementedException();
        }

        public Task UpdateVacancy(Guid employerId, IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}
