using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace MyResume.Domain.Models
{
    public class Password : ValueObject
    {
        private const string passwordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,30}$";

        public string Value { get; }
        private Password(string value) => Value = value;

        public static Result<Password> Create(string input)
        {
            if (string.IsNullOrWhiteSpace(input) ||
                Regex.IsMatch(input, passwordRegex) == false ||
                string.IsNullOrEmpty(input))
                return Result.Failure<Password>("Wrong format! The line length must be between 8 and 30 characters. " +
                    "The line must contain at least one number. The string must contain at least one capital letter. The string must" +
                    " contain at least one lowercase letter.");

            return new Password(input);
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}