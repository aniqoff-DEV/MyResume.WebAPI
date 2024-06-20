using MyResume.Domain.Dtos;

namespace MyResume.Domain.Interfaces.Services
{
    public interface IJobSeekerService
    {
        Task DeleteJobSeeker(Guid id);
        Task<InfoOnCardJobSeekerDto> GetInfoOnCardById(Guid jobSeekerId);
        Task<InfoOnPageJobSeekerDto> GetInfoOnPageJobSeekerById(Guid jobSeekerId);
        Task<List<InfoOnCardJobSeekerDto>> GetInfoOnCardOnList();
        Task UpdatePersonalDataOnJobSeeker(Guid id, string fullName, string description, string? phoneNumber, Guid avatarId, int cityId, int branchId);
    }
}
