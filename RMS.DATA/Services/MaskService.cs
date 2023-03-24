using Microsoft.Data.Sqlite;
using RMS.DATA.BaseRepositories;
using RMS.DATA.BaseServices;
using RMS.DATA.Entities;

namespace RMS.DATA.Services
{
    internal class MaskService : IServiceStandart<Mask>
    {
        private readonly DbConfig dbConfig;
        private readonly IRepositoryStandart<Mask> repo;

        public MaskService(DbConfig dbConfig, IRepositoryStandart<Mask> repo)
        {
            this.dbConfig = dbConfig;
            this.repo = repo;
        }

        public async Task<Mask> CreateAsync(Mask entity)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            return await repo.CreateAsync(entity, connection).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Mask>> CreateListOfEntitiesAsync(IEnumerable<Mask> list)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            return await repo.CreateListOfEntitiesAsync(list, connection).ConfigureAwait(false);
        }

        public async Task DeleteAsync(Mask entity)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            await repo.DeleteAsync(entity, connection);
        }

        public async Task<IEnumerable<Mask>> ReadAllAsync()
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            return await repo.ReadAllAsync(connection).ConfigureAwait(false);
        }

        public async Task UpdateAsync(Mask entity)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            await repo.UpdateAsync(entity, connection);
        }

        public async Task UpdateListOfEntitiesAsync(IEnumerable<Mask> items)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            await repo.UpdateListOfEntitiesAsync(items, connection);
        }
    }
}
