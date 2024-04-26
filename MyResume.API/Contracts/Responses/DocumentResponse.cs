namespace MyResume.API.Contracts.Responses
{
    public record DocumentResponse(Guid Id, string FileName, byte[] Bytes);
}
