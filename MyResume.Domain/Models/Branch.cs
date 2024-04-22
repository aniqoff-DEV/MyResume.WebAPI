using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace MyResume.Domain.Models
{
    public class Branch
    {
        private const string nameRegex = @"^[0-9]*$";
        private Branch(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }
        public string Name { get; }

        public static Result<Branch> Create(int id, string name)
        {
            if (Regex.IsMatch(name, nameRegex))
                return Result.Failure<Branch>($"{nameof(name)} must not contain numbers");

            var branch = new Branch(id, name);

            return Result.Success(branch);
        }
    }
}
