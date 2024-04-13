using MyResume.Domain.Models;

namespace MyResume.Domain.Interfaces.Repositories
{
    public interface IEmployerFeedBackRepository
    {
        Task CreateEmployerFeedback(EmployerFeedback employerFeedback);
        Task DeleteEmployerFeedback(Guid employerFeedbackId);
        Task<List<EmployerFeedback>> GetEmployerFeedbacks();
        Task<EmployerFeedback> GetEmployerFeedbackById(Guid employerFeedbackId);
    }
}
