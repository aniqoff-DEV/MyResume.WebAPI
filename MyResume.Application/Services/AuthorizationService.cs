using CSharpFunctionalExtensions;
using MyResume.Domain.Dtos;
using MyResume.Domain.Interfaces;
using MyResume.Domain.Interfaces.Repositories;
using MyResume.Domain.Interfaces.Services;
using MyResume.Domain.Models;
using MyResume.Domain.ValueObjects;

namespace MyResume.Application.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IEmployerRepository _employerRepository;
        private readonly IJobSeekerRepository _jobSeekerRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly IPasswordHasher _passwordHasher;

        private string verifyError = "Неверно введен логин или пароль!"; // Incorrect login or password!
        private string notFoundUserRoleError = "Роль назначена неверно!"; // The assigned role is incorrect!
        private string employerRole = "employer";
        private string jobSeekerRole = "jobseeker";

        public AuthorizationService(
            IEmployerRepository employerRepository,
            IJobSeekerRepository jobSeekerRepository,
            IJwtProvider jwtProvider,
            IPasswordHasher passwordHasher)
        {
            _employerRepository = employerRepository;
            _jobSeekerRepository = jobSeekerRepository;
            _jwtProvider = jwtProvider;
            _passwordHasher = passwordHasher;
        }

        public async Task<IResult<string>> SignInUser(string role, string email, string password)
        {
            var token = role switch {
                "jobseeker" => await LoginJobSeeker(email, password),
                "employer" => await LoginEmployer(email, password),
                _ => Result.Failure<string>(notFoundUserRoleError)
            };

            return token;
        }

        public async Task<IResult<string>> SignUpUser(string role, string name, string email, string password, int cityId = 0)
        {
            var token = role switch {
                "jobseeker" => await RegisterJobSeeker(name, email, password),
                "employer" => await RegisterEmployer(name, cityId, email, password),
                _ => Result.Failure<string>(notFoundUserRoleError)
            };

            return token;
        }

        private async Task<IResult<string>> RegisterEmployer(string companyName, int cityId, string email, string password)
        {
            var isEmail = Email.Create(email);

            if (isEmail.IsFailure)
                return Result.Failure<string>(isEmail.Error);

            var isPassword = Password.Create(password);

            if (isPassword.IsFailure)
                return Result.Failure<string>(isPassword.Error);

            if (cityId == 0)
                return Result.Failure<string>("Ошибка наименования!");

            var hashedPassword = _passwordHasher.Generate(password);

            #region Data
            var isEmployer = Employer.Create(
                Guid.NewGuid(),
                companyName,
                isEmail.Value,
                isPassword.Value,
        null,
        string.Empty,
        PhoneNumber.Create(null).Value,
                cityId
                );
            #endregion

            if (isEmployer.IsFailure)
                return Result.Failure<string>(isEmployer.Error);

            Guid newEmployerId = await _employerRepository.Create(isEmployer.Value, hashedPassword);
            var token = _jwtProvider.GenerateToken(employerRole, newEmployerId);

            return Result.Success(token);
        }

        private async Task<IResult<string>> RegisterJobSeeker(string fullName, string email, string password)
        {
            var isEmail = Email.Create(email);

            if (isEmail.IsFailure)
                return Result.Failure<string>(isEmail.Error);

            var isPassword = Password.Create(password);

            if (isPassword.IsFailure)
                return Result.Failure<string>(isPassword.Error);

            var hashedPassword = _passwordHasher.Generate(password);

            #region Data
            var isJobSeeker = JobSeeker.Create(
                Guid.NewGuid(),
                fullName,
                string.Empty,
                isEmail.Value,
        isPassword.Value,
        PhoneNumber.Create(null).Value,
        null,
        null,
        null,
        null,
        null
                );
            #endregion


            if (isJobSeeker.IsFailure)
                return Result.Failure<string>(isJobSeeker.Error);

            var jobSeeker = isJobSeeker.Value;

            var newJobSeeker = new JobSeekerDto()
            {
                Id = jobSeeker.Id,
                FullName = jobSeeker.FullName,
                Description = jobSeeker.Description,
                Email = jobSeeker.Email.Value,
                PasswordHash = hashedPassword
            };

            Guid newJobSeekerId = await _jobSeekerRepository.Create(newJobSeeker);
            var token = _jwtProvider.GenerateToken(jobSeekerRole,newJobSeekerId);

            return Result.Success(token);
        }

        private async Task<IResult<string>> LoginJobSeeker(string email, string password)
        {
            var jobSeeker = await _jobSeekerRepository.GetByEmail(email);

            if (jobSeeker is null)
                return Result.Failure<string>(verifyError);

            var result = _passwordHasher.Verify(password, jobSeeker.PasswordHash);

            if (result == false)
                return Result.Failure<string>(verifyError);

            var token = _jwtProvider.GenerateToken(jobSeekerRole,jobSeeker.Id);

            return Result.Success(token);
        }

        private async Task<IResult<string>> LoginEmployer(string email, string password)
        {
            var employer = await _employerRepository.GetByEmail(email);

            if (employer is null)
                return Result.Failure<string>(verifyError);

            var result = _passwordHasher.Verify(password, employer.PasswordHash);

            if (result == false)
                return Result.Failure<string>(verifyError);

            var token = _jwtProvider.GenerateToken(employerRole,employer.Id);

            return Result.Success(token);
        }
    }
}
