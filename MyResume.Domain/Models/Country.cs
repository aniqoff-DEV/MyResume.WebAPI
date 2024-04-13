using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace MyResume.Domain.Models
{
    public class Country
    {
        private const string nameRegex = @"^[0-9]*$";
        private Country(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }
        public string Name { get; }

        public static Result<Country> Create(int id,  string name)
        {
            if (Regex.IsMatch(name, nameRegex))
                return Result.Failure<Country>($"{nameof(name)} must not contain numbers");

            var country = new Country(id,  name);

            return Result.Success(country);
        }
    }
}
