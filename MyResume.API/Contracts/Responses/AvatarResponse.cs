namespace MyResume.API.Contracts.Responses
{
    public record AvatarResponse(Guid Id, string FileName, byte[] Bytes);
}
