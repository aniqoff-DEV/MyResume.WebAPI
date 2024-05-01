using MyResume.Domain.Dtos;
using MyResume.Domain.Models;

namespace MyResume.Domain.Interfaces.Repositories
{
    public interface IJobSeekerRepository
    {
        Task<Guid> Create(JobSeeker jobSeeker);
        Task Delete(Guid id);
        Task<JobSeekerDto> GetByIdRawJobSeeker(Guid id);
        Task<InfoOnPageJobSeekerDto> GetInfoOnPageJobSeekerById(Guid jobSeekerId);
        Task<InfoOnCardJobSeekerDto> GetInfoOnCardJobSeekerById(Guid jobSeekerId);
        Task<List<InfoOnCardJobSeekerDto>> GetInfoOnCardJobSeekerOnList();

        Task UpdateInfo(Guid id,
                        string fullName,
                        string? description,
                        string? phoneNumber,
                        Guid? avatarId,
                        int? cityId,
                        int? branchId);
    }
}
