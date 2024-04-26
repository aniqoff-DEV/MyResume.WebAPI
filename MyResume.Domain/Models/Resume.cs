using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;

namespace MyResume.Domain.Models
{
    public class Resume
    {
        public const int MAX_FILE_SIZE = 2 * 1024 * 1024; // max file size of 2 MB
        public static readonly string[] ALLOWED_EXTENSIONS = [".txt", ".md"];

        private Resume(Guid id, string fileName, IFormFile file)
        {
            Id = id;
            FileName = fileName;
            File = file;
        }

        public Guid Id { get; }
        public string FileName { get; set; }
        public IFormFile File { get; }

        public static Result<Resume> Create(Guid id, string fileName, IFormFile file)
        {
            if (file.Length > MAX_FILE_SIZE)
                return Result.Failure<Resume>("File size exceeded");

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!ALLOWED_EXTENSIONS.Contains(extension))
                return Result.Failure<Resume>("Invalid file type");

            var resume = new Resume(id, fileName, file);

            return Result.Success(resume);
        }
    }
}
