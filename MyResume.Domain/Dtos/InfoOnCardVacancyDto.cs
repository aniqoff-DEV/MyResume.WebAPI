namespace MyResume.Domain.Dtos
{
    public class InfoOnCardVacancyDto
    {
        public Guid Id { get; set; }
        public required string CompanyName { get; set; }
        public required string Description { get; set; }
        public required string BranchName { get; set; }
        public required string Address { get; set; }

        public required string Experience { get; set; }
        public required string Employment { get; set; }
        public required string ScheduleWork { get; set; }
        public required int Salary { get; set; }
        public byte[]? Avatar { get; set; }
    }
}
