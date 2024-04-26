namespace MyResume.Domain.Dtos
{
    public class InfoOnCardJobSeekerDto
    {
        public Guid Id { get; set; }
        public required string FullName { get; set; }
        public required string Description { get; set; }
        public string? BranchName { get; set; }
        public string? CityName { get; set; }
    }
}
