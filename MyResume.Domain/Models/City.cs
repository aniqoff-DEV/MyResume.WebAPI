using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace MyResume.Domain.Models
{
    public class City
    {
        private const string nameRegex = @"^[0-9]*$";
        private City(int id, int countryId, string name)
        {
            Id = id;
            CountryId = countryId;
            Name = name;
        }

        public int Id { get; }
        public int CountryId { get; }
        public string Name { get; }

        public static Result<City> Create(int id, int countryId, string name)
        {
            if (Regex.IsMatch(name, nameRegex))
                return Result.Failure<City>($"{nameof(name)} must not contain numbers");

            if(countryId <= 0)
                return Result.Failure<City>("country code cannot be a negative number or equal to zero");

            var сity = new City(id, countryId, name);

            return Result.Success(сity);
        }
    }
}
