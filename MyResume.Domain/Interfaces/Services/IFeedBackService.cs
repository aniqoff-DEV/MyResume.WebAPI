using MyResume.Domain.Models;

namespace MyResume.Domain.Services.Repositories
{
    public interface IFeedBackService
    {
        Task CreateEmployerFeedback(EmployerFeedback employerFeedback);
        Task DeleteEmployerFeedback(Guid employerFeedbackId);
        Task<List<EmployerFeedback>> GetEmployerFeedbacks();
        Task<EmployerFeedback> GetEmployerFeedbackById(Guid employerFeedbackId);

        Task CreateJobSeekerFeedback(JobSeekerFeedback jobSeekerFeedback);
        Task DeleteJobSeekerFeedback(Guid jobSeekerFeedbackId);
        Task<List<JobSeekerFeedback>> GetJobSeekerFeedbacks();
        Task<JobSeekerFeedback> GetJobSeekerFeedbackById(Guid jobSeekerFeedbackId);
    }
}
