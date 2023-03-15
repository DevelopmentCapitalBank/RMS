using Microsoft.Data.Sqlite;
using RMS.DATA.BaseRepositories;
using RMS.DATA.BaseServices;
using RMS.DATA.Entities;

namespace RMS.DATA.Services
{
    internal class ManagerService : IServiceStandart<Manager>
    {
        private readonly IRepositoryStandart<Manager> repo;
        private readonly DbConfig dbConfig;
        public ManagerService(DbConfig dbConfig,
            IRepositoryStandart<Manager> repo)
        {
            this.dbConfig = dbConfig;
            this.repo = repo;
        }

        public async Task<Manager> CreateAsync(Manager entity)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            return await repo.CreateAsync(entity, connection).ConfigureAwait(false);
        }

        public async Task CreateListOfEntitiesAsync(IEnumerable<Manager> list)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            await repo.CreateListOfEntitiesAsync(list, connection).ConfigureAwait(false);
        }

        public async Task DeleteAsync(Manager entity)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            await repo.DeleteAsync(entity, connection);
        }

        public async Task<IEnumerable<Manager>> ReadAllAsync()
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            return await repo.ReadAllAsync(connection).ConfigureAwait(false);
        }

        public async Task UpdateAsync(Manager entity)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            await repo.UpdateAsync(entity, connection);
        }

        public async Task UpdateListOfEntitiesAsync(IEnumerable<Manager> items)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            await repo.UpdateListOfEntitiesAsync(items, connection);
        }
    }
}
