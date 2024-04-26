namespace MyResume.API.Contracts.Requests
{
    public record JobSeekerStartDataRequest(
            string FullName,
            string Description,
            string Email,
            string Password
        );
}
