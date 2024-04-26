namespace MyResume.Domain.Dtos
{
    public class JobSeekerDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public float Reputation { get; set; }
        public int CountFeedBack { get; set; }

        public Guid AvatarId { get; set; }
        public Guid? ResumeId { get; set; }
        public string CityName { get; set; }
        public string BranchName { get; set; }
    }
}
