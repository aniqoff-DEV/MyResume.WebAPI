using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace MyResume.Domain.Models
{
    public class Avatar
    {
        public const int MAX_FILE_SIZE = 4 * 1024 * 1024; // max file size of 2 MB
        public static readonly string[] ALLOWED_EXTENSIONS = [".jpg", ".jpeg", ".png"];

        private Avatar(Guid id, IFormFile imageFile)
        {
            Id = id;
            ImageFile = imageFile;
        }

        public Guid Id { get; }
        public IFormFile ImageFile { get; }

        public static Result<Avatar> Create(Guid id, IFormFile imageFile)
        {
            if (imageFile.Length > MAX_FILE_SIZE)
                return Result.Failure<Avatar>("File size exceeded");

            var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();

            if (!ALLOWED_EXTENSIONS.Contains(extension))
                return Result.Failure<Avatar>("Invalid file type");

            var vacancy = new Avatar(id, imageFile);

            return Result.Success(vacancy);
        }
    }
}