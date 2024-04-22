using MyResume.Domain.Models;

namespace MyResume.Domain.Interfaces.Repositories
{
    public interface IBranchRepository
    {
        Task<List<Branch>> Get();
        Task<Branch> GetById(int id);
        Task<int> Create(Branch branch);
        Task Delete(int id);
    }
}
