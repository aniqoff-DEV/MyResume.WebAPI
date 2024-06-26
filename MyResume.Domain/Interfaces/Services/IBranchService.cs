﻿using MyResume.Domain.Models;

namespace MyResume.Domain.Interfaces.Services
{
    public interface IBranchService
    {
        Task<List<Branch>> GetBranches();
        Task<Branch> GetBranchById(int id);
        Task<int> CreateBranch(Branch branch);
        Task DeleteBranch(int id);
    }
}
