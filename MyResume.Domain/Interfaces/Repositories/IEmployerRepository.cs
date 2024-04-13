using MyResume.Domain.Models;

namespace MyResume.Domain.Interfaces.Repositories
{
    public interface IEmployerRepository
    {
        Task Create(Employer employer);
        Task Delete(Guid id);
        Task<List<Employer>> Get();
        Task<Employer> GetById(Guid id);
        Task UpdatePersonalData(Guid id, string companyName, Email email, Password password, Guid avatarId,
            string description, string address, PhoneNumber? phoneNumber, int cityId);
    }
}
