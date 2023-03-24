using Microsoft.Data.Sqlite;
using RMS.DATA.BaseRepositories;
using RMS.DATA.BaseServices;
using RMS.DATA.Entities;

namespace RMS.DATA.Services
{
    internal class MaskService : IServiceExtended<Mask, int, string>
    {
        private readonly DbConfig dbConfig;
        private readonly IRepositoryExtended<Mask, int, string> repo;

        public MaskService(DbConfig dbConfig, IRepositoryExtended<Mask, int, string> repo)
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

        public async Task<IEnumerable<Mask>> FindAsync(string f)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            return await repo.FindAsync(f, connection).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Mask>> ReadAllAsync()
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            return await repo.ReadAllAsync(connection).ConfigureAwait(false);
        }

        public async Task<Mask> ReadByIdAsync(int id)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            var account = await repo.ReadByIdAsync(id, connection).ConfigureAwait(false);
            return account;
        }

        public async Task<IEnumerable<Mask>> ReadListByIdAsync(int id)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            return await repo.ReadListByIdAsync(id, connection).ConfigureAwait(false);
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
