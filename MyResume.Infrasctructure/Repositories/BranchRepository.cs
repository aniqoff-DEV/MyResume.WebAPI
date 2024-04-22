using Dapper;
using MyResume.Domain.Interfaces.Repositories;
using MyResume.Domain.Models;
using MyResume.Infrasctructure.Entities;
using Npgsql;

namespace MyResume.Infrasctructure.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private NpgsqlConnection connection;

        public BranchRepository()
        {
            connection = new NpgsqlConnection(NpgsqlConfig.CONNECTION_STRING);
            connection.Open();
        }

        public async Task<int> Create(Branch branch)
        {
            var branchEntity = new BranchEntity
            {
                Id = branch.Id,
                Name = branch.Name,
            };

            string sql = $"INSERT INTO {nameof(Branch)} (name) VALUES (@Name) RETURNING id;";
            var branchId = await connection.QueryAsync<int>(sql, branchEntity);

            return branchId.FirstOrDefault();
        }

        public async Task Delete(int id)
        {
            string sqlQuery = $"DELETE FROM {nameof(Branch)} WHERE id = {id};";
            await connection.ExecuteAsync(sqlQuery);

            return;
        }

        public async Task<List<Branch>> Get()
        {
            var branches = await connection.QueryAsync<Branch>($"SELECT * FROM {nameof(Branch)}");
            return branches.ToList();
        }

        public async Task<Branch> GetById(int id)
        {
            var branch = await connection.QueryAsync<Branch>($"SELECT * FROM {nameof(Branch)} WHERE id = @Id", id);

            return branch.FirstOrDefault()!;
        }
    }
}
