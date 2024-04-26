using MyResume.Domain.Models;

namespace MyResume.Infrasctructure.Entities
{
    public class JobSeekerEntity
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public float Reputation { get; set; } 
        public int CountFeedBack { get; set; } 

        public Guid? AvatarId { get; set; }
        public Guid? ResumeId { get; set; }
        public int? CityId { get; set; }
        public int? BranchId { get; set; }
    }
}
