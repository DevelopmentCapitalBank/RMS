using Microsoft.Data.Sqlite;
using RMS.DATA.BaseRepositories;
using RMS.DATA.BaseServices;
using RMS.DATA.Entities;

namespace RMS.DATA.Services
{
    internal class GroupService : IServiceAvg<Group, int>
    {
        private readonly IRepositoryAvg<Group, int> repo;
        private readonly DbConfig dbConfig;
        public GroupService(DbConfig dbConfig,
            IRepositoryAvg<Group, int> repo)
        {
            this.dbConfig = dbConfig;
            this.repo = repo;
        }

        public async Task<Group> CreateAsync(Group entity)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            return await repo.CreateAsync(entity, connection).ConfigureAwait(false);
        }

        public async Task DeleteAsync(Group entity)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            await repo.DeleteAsync(entity, connection);
        }

        public async Task<IEnumerable<Group>> ReadAllAsync()
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            return await repo.ReadAllAsync(connection).ConfigureAwait(false);
        }

        public async Task<Group> ReadByIdAsync(int id)
        {
            using var connection = new SqliteConnection(dbConfig.Name);

            return await repo.ReadByIdAsync(id, connection);
        }

        public async Task UpdateAsync(Group entity)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            await repo.UpdateAsync(entity, connection);
        }
    }
}
