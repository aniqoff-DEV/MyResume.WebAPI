using MyResume.Domain.Models;

namespace MyResume.Domain.Services.Repositories
{
    public interface IBranchService
    {
        Task<List<Branch>> GetBranches();
        Task<Branch> GetBranchById(int id);
        Task CreateBranch(Branch branch);
    }
}
