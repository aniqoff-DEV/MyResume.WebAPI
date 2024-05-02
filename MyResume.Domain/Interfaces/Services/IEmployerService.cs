using MyResume.Domain.Dtos;
using MyResume.Domain.Models;

namespace MyResume.Domain.Interfaces.Services
{
    public interface IEmployerService
    {
        Task<Guid> Create(Employer employer);
        Task<EmployerDto> GetCompanyCardById(Guid employerId);
        Task<List<EmployerDto>> GetCompanyCards();
    }
}
