using MyResume.Domain.Dtos;
using MyResume.Domain.Models;
using MyResume.Domain.ValueObjects;

namespace MyResume.Domain.Interfaces.Repositories
{
    public interface IJobSeekerRepository
    {
        Task<Guid> Create(JobSeeker jobSeeker);
        Task Delete(Guid id);
        Task<List<JobSeeker>> GetRawJobSeekers();
        Task<JobSeeker> GetByIdRawJobSeeker(Guid id);
        Task<InfoOnCardJobSeekerDto> GetInfoOnCard(Guid jobSeekerId);
        Task UpdateInfo(Guid id,
                                       string fullName,
                                       string? description,
                                       string? phoneNumber,
                                       Guid? avatarId,
                                       int? cityId,
                                       int? branchId);
    }
}
