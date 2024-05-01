using MyResume.Domain.Dtos;
using MyResume.Domain.Interfaces.Repositories;
using MyResume.Domain.Interfaces.Services;
using MyResume.Domain.Models;

namespace MyResume.Application.Services
{
    public class EmployerService : IEmployerService
    {
        private readonly IEmployerRepository _repository;

        public EmployerService(IEmployerRepository repository) => _repository = repository;

        public async Task<Guid> Create(Employer employer)
        {
            Guid employerId = await _repository.Create(employer);
            return employerId;
        }

        public async Task<EmployerDto> GetEmployerById(Guid employerId)
        {
            var employer = await _repository.GetEmployerById(employerId);
            return employer;
        }
    }
}
