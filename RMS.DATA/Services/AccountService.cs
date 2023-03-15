using Microsoft.Data.Sqlite;
using RMS.DATA.BaseRepositories;
using RMS.DATA.BaseServices;
using RMS.DATA.Entities;

namespace RMS.DATA.Services
{
    internal class AccountService : IServiceExtended<Account, int, string>
    {
        private readonly IRepositoryExtended<Account, int, string> repo;
        private readonly DbConfig dbConfig;
        public AccountService(DbConfig dbConfig,
            IRepositoryExtended<Account, int, string> repo)
        {
            this.dbConfig = dbConfig;
            this.repo = repo; ;
        }

        public async Task<Account> CreateAsync(Account entity)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            return await repo.CreateAsync(entity, connection).ConfigureAwait(false);
        }

        public async Task CreateListOfEntitiesAsync(IEnumerable<Account> list)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            await repo.CreateListOfEntitiesAsync(list, connection).ConfigureAwait(false);
        }

        public async Task DeleteAsync(Account entity)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            await repo.DeleteAsync(entity, connection);
        }

        public async Task<IEnumerable<Account>> FindAsync(string f)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            return await repo.FindAsync(f, connection).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Account>> ReadAllAsync()
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            return await repo.ReadAllAsync(connection).ConfigureAwait(false);
        }

        public async Task<Account> ReadByIdAsync(int id)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            var account = await repo.ReadByIdAsync(id, connection).ConfigureAwait(false);
            return account;
        }

        public async Task<IEnumerable<Account>> ReadListByIdAsync(int id)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            return await repo.ReadListByIdAsync(id, connection).ConfigureAwait(false);
        }

        public async Task UpdateAsync(Account entity)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            await repo.UpdateAsync(entity, connection);
        }

        public async Task UpdateListOfEntitiesAsync(IEnumerable<Account> items)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            await repo.UpdateListOfEntitiesAsync(items, connection);
        }
    }
}
