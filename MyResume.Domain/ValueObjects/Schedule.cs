using CSharpFunctionalExtensions;

namespace MyResume.Domain.ValueObjects
{
    public class Schedule : ValueObject
    {
        public static readonly Schedule FullDay = new ("Полный день");
        public static readonly Schedule ShiftWork = new ("Сменный график");
        public static readonly Schedule ShiftMethod = new ("Вахтовый метод");
        public static readonly Schedule FlexibleSchedule = new ("Гибкий график");
        public static readonly Schedule DistantWork = new ("Удаленная работа");

        private static readonly Schedule[] _all = [
            FullDay,
            ShiftWork,
            ShiftMethod,
            FlexibleSchedule, 
            DistantWork
            ];

        public string Value { get; }

        private Schedule(string value)
        {
            Value = value;
        }

        public static Result<Schedule> Create(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return Result.Failure<Schedule>("The line must not be empty");

            var experience = input.ToLower();

            if (_all.Any(e => e.Value.ToLower() == input) == false)
                return Result.Failure<Schedule>("The value is not valid");

            return new Schedule(experience);
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
