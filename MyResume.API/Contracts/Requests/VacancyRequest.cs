namespace MyResume.API.Contracts.Requests
{
    public record VacancyRequest(
        Guid EmloyerId,
        int BranchId,
        string Experience,
        string Employment,
        string ScheduleWork,
        int Salary,
        IFormFile File
        );
}
