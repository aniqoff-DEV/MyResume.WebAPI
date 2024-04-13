using MyResume.Domain.Models;

namespace MyResume.Domain.Services.Repositories
{
    public interface IJobSeekerService
    {
        Task CreateJobSeeker(JobSeeker jobSeeker);
        Task DeleteJobSeeker(Guid id);
        Task<List<JobSeeker>> GetJobSeekers();
        Task<JobSeeker> GetJobSeekerById(Guid id);
        Task UpdatePersonalDataOnJobSeeker(Guid id, string fullName, string description, Email email, float reputation,
            Password password, PhoneNumber? phoneNumber, Guid avatarId, int cityId, int branchId);
    }
}
