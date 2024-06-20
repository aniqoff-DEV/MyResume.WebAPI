
namespace MyResume.Domain.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateToken(string role, Guid userId);
    }
}