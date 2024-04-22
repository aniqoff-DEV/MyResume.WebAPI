using MyResume.Domain.Interfaces.Repositories;
using MyResume.Domain.Models;
using MyResume.Domain.Services.Repositories;

namespace MyResume.Application.Services
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _repository;

        public BranchService(IBranchRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateBranch(Branch branch)
        {
            var newBranchId = await _repository.Create(branch);
            return newBranchId;
        }

        public async Task DeleteBranch(int id)
        {
            await _repository.Delete(id);
            return;
        }

        public async Task<Branch> GetBranchById(int id)
        {
            var branch = await _repository.GetById(id);
            return branch;
        }

        public async Task<List<Branch>> GetBranches()
        {
            var branches = await _repository.Get();
            return branches;
        }
    }
}
