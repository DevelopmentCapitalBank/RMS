using Microsoft.Data.Sqlite;
using RMS.DATA.BaseRepositories;
using RMS.DATA.BaseServices;
using RMS.DATA.Entities;

namespace RMS.DATA.Services
{
    internal class GroupService : IServiceStandart<Group>
    {
        private readonly IRepositoryStandart<Group> repo;
        private readonly DbConfig dbConfig;
        public GroupService(DbConfig dbConfig,
            IRepositoryStandart<Group> repo)
        {
            this.dbConfig = dbConfig;
            this.repo = repo;
        }

        public async Task<Group> CreateAsync(Group entity)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            return await repo.CreateAsync(entity, connection).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Group>> CreateListOfEntitiesAsync(IEnumerable<Group> list)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            return await repo.CreateListOfEntitiesAsync(list, connection).ConfigureAwait(false);
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

        public async Task UpdateAsync(Group entity)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            await repo.UpdateAsync(entity, connection);
        }

        public async Task UpdateListOfEntitiesAsync(IEnumerable<Group> items)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            await repo.UpdateListOfEntitiesAsync(items, connection);
        }
    }
}
