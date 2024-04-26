using MyResume.Domain.ValueObjects;

namespace MyResume.API.Contracts.Requests
{
    public record JobSeekerRequest(
            string FullName,
            string Description,
            string Email,
            string Password,
            string? PhoneNumber,
            Guid? AvatarId,
            Guid? ResumeId,
            int? CityId,
            int? BranchId
        );
}
