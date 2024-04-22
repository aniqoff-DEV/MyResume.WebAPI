using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using MyResume.Domain.ValueObjects;

namespace MyResume.Domain.Models
{
    public class Vacancy
    {
        public const int MAX_FILE_SIZE = 2 * 1024 * 1024; // max file size of 2 MB
        public static readonly string[] ALLOWED_EXTENSIONS = [".txt", ".md"];

        private Vacancy(Guid id, Guid employerId, IFormFile file,
            Experience experience, EmploymentType employment, Schedule scheduleWork)
        {
            Id = id;
            EmployerId = employerId;
            File = file;
            Experience = experience;
            Employment = employment;
            ScheduleWork = scheduleWork;
        }

        public Guid Id { get; }
        public Guid EmployerId {  get; }
        public Experience Experience { get; }
        public EmploymentType Employment { get; }
        public Schedule ScheduleWork { get; }
        public IFormFile File { get; }

        public static Result<Vacancy> Create(
            Guid id,
            Guid employerId,
            IFormFile file,
            Experience experience,
            EmploymentType employment,
            Schedule scheduleWork)
        {
            if (file.Length > MAX_FILE_SIZE)
                return Result.Failure<Vacancy>("File size exceeded");

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!ALLOWED_EXTENSIONS.Contains(extension))
                return Result.Failure<Vacancy>("Invalid file type");

            var vacancy = new Vacancy(id, employerId,file, experience,employment,scheduleWork);

            return Result.Success(vacancy);
        }
    }
}
