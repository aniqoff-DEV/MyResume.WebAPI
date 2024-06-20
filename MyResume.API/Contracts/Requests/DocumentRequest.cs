namespace MyResume.API.Contracts.Requests
{
    public record DocumentRequest(IFormFile File, CancellationToken Token);
}
