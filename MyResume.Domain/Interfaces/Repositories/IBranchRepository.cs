using MyResume.Domain.Models;

namespace MyResume.Domain.Interfaces.Repositories
{
    public interface IBranchRepository
    {
        Task<List<Branch>> Get();
        Task<Branch> GetById(int id);
        Task Create(Branch branch);
    }
}
