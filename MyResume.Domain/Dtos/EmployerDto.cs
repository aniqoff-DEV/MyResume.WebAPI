namespace MyResume.Domain.Dtos
{
    public class EmployerDto
    {
        public Guid Id { get; set; }
        public required string CompanyName { get; set; }
        public string Description { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string Address { get; set; }
        public string? PhoneNumber { get; set; }
        public float Reputation { get; set; }
        public int CountFeedBack { get; set; }

        public Guid? AvatarId { get; set; }
        public int CityId { get; set; }
    }
}
