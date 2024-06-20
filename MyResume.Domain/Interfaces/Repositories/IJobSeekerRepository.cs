using MyResume.Domain.Dtos;

namespace MyResume.Domain.Interfaces.Repositories
{
    public interface IJobSeekerRepository
    {
        Task<Guid> Create(JobSeekerDto jobSeeker);
        Task Delete(Guid id);
        Task<JobSeekerDto> GetByEmail(string email);
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
