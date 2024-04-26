using MyResume.Domain.Dtos;
using MyResume.Domain.Models;
using MyResume.Domain.ValueObjects;

namespace MyResume.Domain.Interfaces.Services
{
    public interface IJobSeekerService
    {
        Task<Guid> CreateJobSeeker(JobSeeker jobSeeker);
        Task DeleteJobSeeker(Guid id);
        Task<InfoOnCardJobSeekerDto> GetInfoOnCardById(Guid jobSeekerId);
        Task<List<InfoOnCardJobSeekerDto>> GetInfoOnCardOnList();
        Task<List<JobSeeker>> GetJobSeekers();
        Task<JobSeeker> GetJobSeekerById(Guid id);
        Task UpdatePersonalDataOnJobSeeker(Guid id, string fullName, string description, string? phoneNumber, Guid avatarId, int cityId, int branchId);
    }
}
