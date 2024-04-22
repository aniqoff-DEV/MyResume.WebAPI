using CSharpFunctionalExtensions;

namespace MyResume.Domain.ValueObjects
{
    public class Experience : ValueObject
    {
        public static readonly Experience DoesntMatter = new("Не имеет значения");
        public static readonly Experience NoExperience = new("Без опыта");
        public static readonly Experience From1To3Years = new("От 1 года до 3 лет");
        public static readonly Experience From3To6Years = new("От 3 до 6 лет");
        public static readonly Experience More6Years = new("Более 6 лет");

        private static readonly Experience[] _all = [
            DoesntMatter,
            NoExperience,
            From1To3Years,
            From3To6Years,
            More6Years
            ];

        public string Value { get; }

        private Experience(string value)
        {
            Value = value;
        }

        public static Result<Experience> Create(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return Result.Failure<Experience>("The line must not be empty");

            var experience = input.ToLower();

            if (_all.Any(e => e.Value.ToLower() == input) == false)
                return Result.Failure<Experience>("The value is not valid");

            return new Experience(experience);
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
