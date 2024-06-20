using MyResume.Domain.Dtos;
using MyResume.Domain.Models;

namespace MyResume.Domain.Interfaces.Services
{
    public interface IEmployerService
    {
        Task<EmployerDto> GetCompanyCardById(Guid employerId);
        Task<List<EmployerDto>> GetCompanyCards();
    }
}
