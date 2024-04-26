using CSharpFunctionalExtensions;
using MyResume.Domain.ValueObjects;
using System.Text.RegularExpressions;

namespace MyResume.Domain.Models
{
    public class JobSeeker
    {
        public const int MAX_LENGHT_FULLNAME = 70;
        public const int MAX_LENGHT_DESCRIPTION = 250;

        private const string nameRegex = @"^[^<>.!@#%/]$";
        private const string nameRegexWithoutNumbers = @"[0-9]";

        private JobSeeker(Guid id, string fullName, string description, Email email,
            Password password, PhoneNumber? phoneNumber, Guid? avatarId, Guid? resumeId, int? cityId, int? branchId)
        {
            Id = id;
            FullName = fullName;
            Description = description;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
            AvatarId = avatarId;
            ResumeId = resumeId;
            CityId = cityId;
            BranchId = branchId;
        }

        public Guid Id { get; }
        public string FullName { get; }
        public string Description { get; } = string.Empty;
        public Email Email { get; }
        public Password Password { get; }
        public PhoneNumber PhoneNumber { get; }
        public float Reputation { get; private set; } = 0;
        public int CountFeedBack { get; private set; } = 0;

        public Guid? AvatarId { get; }
        public Guid? ResumeId { get; }
        public int? CityId { get; }
        public int? BranchId { get; } 

        public void UpdateReputation(int star) 
        {
            CountFeedBack++;
            Reputation = star / CountFeedBack;
        }

        public static Result<JobSeeker> Create(
            Guid id, 
            string fullName,
            string description,
            Email email,
            Password password,
            PhoneNumber? phoneNumber,
            Guid? avatarId,
            Guid? resumeId,
            int? cityId, 
            int? branchId)
        {
            if (fullName.Length > MAX_LENGHT_FULLNAME)
                return Result.Failure<JobSeeker>("Too many characters of name!");

            var r1 = Regex.IsMatch(fullName, nameRegex);
            var r2 = Regex.IsMatch(fullName, nameRegexWithoutNumbers);
            if (Regex.IsMatch(fullName,nameRegex) || Regex.IsMatch(fullName, nameRegexWithoutNumbers))
                return Result.Failure<JobSeeker>("The name must not contain special characters and/or numbers!");

            if (description.Length > MAX_LENGHT_DESCRIPTION)
                return Result.Failure<JobSeeker>("Too many characters of description!");

            var employer = new JobSeeker(id,fullName,description,email,  password, phoneNumber,  avatarId, resumeId, cityId, branchId);

            return Result.Success(employer);
        }
    }
}
