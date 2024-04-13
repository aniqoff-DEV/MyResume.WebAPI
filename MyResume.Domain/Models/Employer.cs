using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace MyResume.Domain.Models
{
    public class Employer
    {
        public const int MAX_LENGHT_COMPANY_NAME = 50;
        public const int MAX_LENGHT_DESCRIPTION = 380;
        private const string companyNameRegex = @"^[0-9]{1,2}";

        private Employer(Guid id, string companyName, Email email, Password password, Guid avatarId,
            string description, string address, PhoneNumber? phoneNumber, int cityId)
        {
            Id = id;
            CompanyName = companyName;
            Description = description;
            Address = address;
            PhoneNumber = phoneNumber;
            CityId = cityId;
            Email = email;
            Password = password;
            AvatarId = avatarId;
        }

        public Guid Id { get; }
        public string CompanyName { get; } 
        public string Description { get; } = string.Empty;
        public Email Email {  get; }
        public Password Password { get; }
        public string Address { get; } = string.Empty;
        public PhoneNumber? PhoneNumber { get; }
        public float Reputation { get; private set; } = 0;
        public int CountFeedBack { get; private set; } = 0;

        public Guid AvatarId {  get; }
        public int CityId { get; }

        public void UpdateReputation(int star)
        {
            CountFeedBack++;
            Reputation = star / CountFeedBack;
        }

        public static Result<Employer> Create(
            Guid id, 
            string companyName, 
            Email email,
            Password password,
            Guid avatarId,
            string description,
            string address,
            PhoneNumber? phoneNumber,
            int cityId)
        {
            if (companyName.Length > MAX_LENGHT_COMPANY_NAME ||
                string.IsNullOrEmpty(companyName) ||
                string.IsNullOrWhiteSpace(companyName) ||
                companyName.Length <= 1 ||
                Regex.IsMatch(companyName, companyNameRegex) ||
                !companyName.All(c => char.IsLetterOrDigit(c) || c == ' ')
                )
            {
                return Result.Failure<Employer>($"{nameof(companyName)} in wrong format!");
            }

            if (description.Length > MAX_LENGHT_DESCRIPTION)
            {
                return Result.Failure<Employer>($"length {nameof(description)} cannot be more than 380 characters");
            }

            var employer = new Employer(id,companyName,email, password, avatarId, description,address, phoneNumber, cityId);
            
            return Result.Success(employer);
        }
    }
}
