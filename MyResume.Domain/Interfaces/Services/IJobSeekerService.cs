using MyResume.Domain.Dtos;
using MyResume.Domain.Models;

namespace MyResume.Domain.Interfaces.Services
{
    public interface IJobSeekerService
    {
        Task<Guid> CreateJobSeeker(JobSeeker jobSeeker);
        Task DeleteJobSeeker(Guid id);
        Task<InfoOnCardJobSeekerDto> GetInfoOnCardById(Guid jobSeekerId);
        Task<InfoOnPageJobSeekerDto> GetInfoOnPageJobSeekerById(Guid jobSeekerId);
        Task<List<InfoOnCardJobSeekerDto>> GetInfoOnCardOnList();
        Task<JobSeekerDto> GetJobSeekerById(Guid id);
        Task UpdatePersonalDataOnJobSeeker(Guid id, string fullName, string description, string? phoneNumber, Guid avatarId, int cityId, int branchId);
    }
}
