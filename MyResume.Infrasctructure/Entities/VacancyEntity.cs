namespace MyResume.Infrasctructure.Entities
{
    public class VacancyEntity
    {
        public Guid Id { get; set; }
        public Guid EmployerId { get; set; }
        public int BranchId { get; set; }

        public required string Experience { get; set; }
        public required string Employment {  get; set; }
        public required string ScheduleWork { get; set; }
        public required string FileName { get; set; }
        public required byte[] File { get; set; }
    }
}
