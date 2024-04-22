using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace MyResume.Domain.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        private const string phoneRegex = @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[d\- ]{7,10}$";
        public string Number { get; }

        private PhoneNumber(string number) => Number = number;

        public static Result<PhoneNumber> Create(string input)
        {
            if (string.IsNullOrWhiteSpace(input) || Regex.IsMatch(input, phoneRegex) == false)
                return Result.Failure<PhoneNumber>("inputted wrong format!");

            return new PhoneNumber(input);
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Number;
        }
    }
}
