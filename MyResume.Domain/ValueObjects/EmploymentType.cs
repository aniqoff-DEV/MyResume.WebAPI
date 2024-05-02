using CSharpFunctionalExtensions;

namespace MyResume.Domain.ValueObjects
{
    public class EmploymentType : ValueObject
    {
        public static readonly EmploymentType FullEmployment = new("Полная занятость");
        public static readonly EmploymentType PartTimeEmployment = new("Частичная занятость");
        public static readonly EmploymentType ProjectWork = new("Проектная работа");
        public static readonly EmploymentType Internship = new("Стажировка");
        public static readonly EmploymentType Volunteering = new("Волонтерство");
        public static readonly EmploymentType RegistrationGPC = new("Оформление по гпх");

        private static readonly EmploymentType[] _all = [
            FullEmployment,
            PartTimeEmployment,
            ProjectWork,
            Internship,
            Volunteering,
            RegistrationGPC
            ];

        public string Value { get; }

        private EmploymentType(string value)
        {
            Value = value;
        }

        public static Result<EmploymentType> Create(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return Result.Failure<EmploymentType>("The line must not be empty");

            var employment = input.ToLower();

            if (_all.Any(e => e.Value.ToLower() == employment) == false)
                return Result.Failure<EmploymentType>("The value is not valid");

            return new EmploymentType(employment);
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
