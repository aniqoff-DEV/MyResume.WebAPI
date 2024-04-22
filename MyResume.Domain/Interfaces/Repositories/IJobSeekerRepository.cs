using MyResume.Domain.Models;
using MyResume.Domain.ValueObjects;

namespace MyResume.Domain.Interfaces.Repositories
{
    public interface IJobSeekerRepository
    {
        Task Create(JobSeeker jobSeeker);
        Task Delete(Guid id);
        Task<List<JobSeeker>> Get();
        Task<JobSeeker> GetById(Guid id);
        Task UpdatePersonalData(Guid id, string fullName, string description, Email email, float reputation,
            Password password, PhoneNumber? phoneNumber, Guid avatarId, int cityId, int branchId);
    }
}
