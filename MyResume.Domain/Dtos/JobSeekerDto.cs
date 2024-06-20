namespace MyResume.Domain.Dtos
{
    public class JobSeekerDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } 
        public string Description { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string? PhoneNumber { get; set; } = null;
        public float Reputation { get; set; } = 0f;
        public int CountFeedBack { get; set; } = 0;
        public int? DesiredSalary { get; set; } = null;

        public Guid? AvatarId { get; set; } = null;
        public Guid? ResumeId { get; set; } = null;
        public int? CityId { get; set; } = null;
        public int? BranchId { get; set; } = null;
    }
}
