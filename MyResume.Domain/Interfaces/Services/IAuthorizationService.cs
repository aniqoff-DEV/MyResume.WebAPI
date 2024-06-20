using CSharpFunctionalExtensions;

namespace MyResume.Domain.Interfaces.Services
{
    public interface IAuthorizationService
    {
        public Task<IResult<string>> SignInUser(string role, string email, string password);
        public Task<IResult<string>> SignUpUser(string role, string name, string email, string password, int cityId = 0);
    }
}
