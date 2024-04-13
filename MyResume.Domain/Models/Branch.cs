using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace MyResume.Domain.Models
{
    public class Branch
    {
        private const string nameRegex = @"^[0-9]*$";
        private Branch(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; }
        public string Title { get; }

        public static Result<Branch> Create(int id, string title)
        {
            if (Regex.IsMatch(title, nameRegex))
                return Result.Failure<Branch>($"{nameof(title)} must not contain numbers");

            var branch = new Branch(id, title);

            return Result.Success(branch);
        }
    }
}
