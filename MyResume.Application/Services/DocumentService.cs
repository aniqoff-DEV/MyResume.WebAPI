using Microsoft.AspNetCore.Http;
using MyResume.Domain.Interfaces.Repositories;
using MyResume.Domain.Interfaces.Services;
using MyResume.Domain.Models;

namespace MyResume.Application.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IResumeRepository _resumeRepository;

        public DocumentService(IResumeRepository resumeRepository)
        {
            _resumeRepository = resumeRepository;
        }

        public async Task<Guid> CreateResume(Resume resume)
        {
            Guid newResumeId = await _resumeRepository.CreateResume(resume);
            return newResumeId;
        }

        public Task<Guid> CreateVacancy(Vacancy vacancy)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteResume(Guid resumeId)
        {
            await _resumeRepository.DeleteResume(resumeId);
            return;
        }

        public Task DeleteVacancy(Guid employerId)
        {
            throw new NotImplementedException();
        }

        public Task<Vacancy> GetByVcancyId(Guid employerId)
        {
            throw new NotImplementedException();
        }

        public async Task<Resume> GetByResumeId(Guid resumeId)
        {
            var resume = await _resumeRepository.GetByResumeId(resumeId);
            return resume;
        }

        public async Task UpdateResume(Guid resumeId, IFormFile file)
        {
            await _resumeRepository.UpdateResume(resumeId,file);
            return;
        }

        public Task UpdateVacancy(Guid employerId, IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}
