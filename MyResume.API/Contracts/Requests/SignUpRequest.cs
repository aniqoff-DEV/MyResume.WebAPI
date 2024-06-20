namespace MyResume.API.Contracts.Requests
{
    public record SignUpRequest(string Role,
                                string Name,
                                string Email,
                                string Password,
                                int CityId = 0);
}
