using MyResume.Domain.Dtos;
using MyResume.Domain.Models;

namespace MyResume.Domain.Interfaces.Repositories
{
    public interface IEmployerRepository
    {
        Task<EmployerDto> GetByEmail(string email);
        Task<Guid> Create(Employer employer, string passwordHash);
        Task<EmployerDto> GetCompanyCardById(Guid employerId);
        Task<List<EmployerDto>> GetCompanyCards();
    }
}
