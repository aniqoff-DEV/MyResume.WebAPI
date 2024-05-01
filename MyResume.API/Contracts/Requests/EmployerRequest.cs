namespace MyResume.API.Contracts.Requests
{
    public record EmployerRequest(
        string CompanyName,
        string Description,
        string Email,
        string Password,
        string Address,
        string? PhoneNumber,
        Guid? AvatarId,
        int CityId
        );
}
