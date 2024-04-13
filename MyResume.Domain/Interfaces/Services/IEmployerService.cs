using MyResume.Domain.Models;

namespace MyResume.Domain.Services.Repositories
{
    public interface IEmployerService
    {
        Task CreateEmployer(Employer employer);
        Task DeleteEmployer(Guid id);
        Task<List<Employer>> GetEmployers();
        Task<Employer> GetEmployerById(Guid id);
        Task UpdatePersonalDataOnEmployer(Guid id, string companyName, Email email, Password password, Guid avatarId,
            string description, string address, PhoneNumber? phoneNumber, int cityId);
    }
}
