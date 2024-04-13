using MyResume.Domain.Models;

namespace MyResume.Domain.Interfaces.Repositories
{
    public interface IJobSeekerFeedBackRepository
    {
        Task CreateJobSeekerFeedback(JobSeekerFeedback jobSeekerFeedback);
        Task DeleteJobSeekerFeedback(Guid jobSeekerFeedbackId);
        Task<List<JobSeekerFeedback>> GetJobSeekerFeedbacks();
        Task<JobSeekerFeedback> GetJobSeekerFeedbackById(Guid jobSeekerFeedbackId);
    }
}
