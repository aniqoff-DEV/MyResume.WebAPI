using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;

namespace MyResume.Domain.Models
{
    public class Vacancy
    {
        public const int MAX_FILE_SIZE = 2 * 1024 * 1024; // max file size of 2 MB
        public static readonly string[] ALLOWED_EXTENSIONS = [".txt", ".md"];

        private Vacancy(Guid id, Guid employerId, IFormFile file)
        {
            Id = id;
            EmployerId = employerId;
            File = file;
        }

        public Guid Id { get; }
        public Guid EmployerId {  get; }
        public IFormFile File { get; }

        public static Result<Vacancy> Create(Guid id, Guid employerId, IFormFile file)
        {
            if (file.Length > MAX_FILE_SIZE)
                return Result.Failure<Vacancy>("File size exceeded");

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!ALLOWED_EXTENSIONS.Contains(extension))
                return Result.Failure<Vacancy>("Invalid file type");

            var vacancy = new Vacancy(id, employerId,file);

            return Result.Success(vacancy);
        }
    }
}
