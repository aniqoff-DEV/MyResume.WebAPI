namespace MyResume.Domain.Dtos
{
    public class InfoOnPageJobSeekerDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public float Reputation { get; set; }
        public int CountFeedBack { get; set; }
        public int? DesiredSalary { get; set; }

        public byte[]? Avatar { get; set; }
        public byte[]? Resume { get; set; }
        public string? City { get; set; }
        public string? Branch { get; set; }
    }
}
