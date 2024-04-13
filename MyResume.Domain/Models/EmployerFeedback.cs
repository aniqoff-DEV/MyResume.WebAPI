using CSharpFunctionalExtensions;

namespace MyResume.Domain.Models
{
    public class EmployerFeedback
    {
        public const int MAX_LENGHT_TITLE = 50;
        public const int MAX_LENGHT_DESCRIPTION = 800; 
        
        public const int MIN_LENGHT_TITLE = 10;
        public const int MIN_LENGHT_DESCRIPTION = 100;

        private static Predicate<int> isValidAssessment = (int x) => x >= 1 || x <= 5;

        private EmployerFeedback(Guid id, Guid employerId, string title, string description, int numberStars)
        {
            Id = id;
            EmployerId = employerId;
            Title = title;
            Description = description;
            NumberStars = numberStars;
        }

        public Guid Id { get; }
        public Guid EmployerId { get; }

        public string Title { get; }
        public string Description { get; }
        public int NumberStars { get; }

        public static Result<EmployerFeedback> Create(
            Guid id, 
            Guid employerId, 
            string title,
            string description,
            int numberStars)
        {
            if (title.Length > MAX_LENGHT_TITLE || title.Length < MIN_LENGHT_TITLE)
                return Result.Failure<EmployerFeedback>
                    ($"The {nameof(title)} must be more than {MAX_LENGHT_TITLE} characters and not less than {MIN_LENGHT_TITLE}");

            if (description.Length > MAX_LENGHT_DESCRIPTION || description.Length < MIN_LENGHT_DESCRIPTION)
                return Result.Failure<EmployerFeedback>
                    ($"The {nameof(description)} must be more than {MAX_LENGHT_DESCRIPTION} characters and not less than {MIN_LENGHT_DESCRIPTION}");

            if (isValidAssessment(numberStars))
                return Result.Failure<EmployerFeedback>("Error: the score does not correspond to a number from 1 to 5");

            var feedBack = new EmployerFeedback(id,employerId,title,description,numberStars);

            return Result.Success(feedBack);
        }
    }
}
