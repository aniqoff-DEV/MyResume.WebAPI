using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace MyResume.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        private const string emailRegex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

        public string EmailValue { get; }
        private Email(string email) => EmailValue = email;

        public static Result<Email> Create(string input)
        {
            if (string.IsNullOrWhiteSpace(input) ||
                Regex.IsMatch(input, emailRegex) == false ||
                string.IsNullOrEmpty(input))
                return Result.Failure<Email>($"{input} is wrong format!");

            return new Email(input);
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return EmailValue;
        }
    }
}