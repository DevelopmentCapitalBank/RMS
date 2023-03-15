using Microsoft.Data.Sqlite;
using RMS.DATA.BaseRepositories;
using RMS.DATA.BaseServices;
using RMS.DATA.Entities;

namespace RMS.DATA.Services
{
    internal class CompanyService : IServiceExtended<Company, int, string>
    {
        private readonly IRepositoryExtended<Company, int, string> repo;
        private readonly DbConfig dbConfig;
        public CompanyService(DbConfig dbConfig,
            IRepositoryExtended<Company, int, string> repo)
        {
            this.dbConfig = dbConfig;
            this.repo = repo; ;
        }

        public async Task<Company> CreateAsync(Company entity)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            return await repo.CreateAsync(entity, connection).ConfigureAwait(false);
        }

        public async Task DeleteAsync(Company entity)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            await repo.DeleteAsync(entity, connection);
        }

        public async Task<IEnumerable<Company>> FindAsync(string f)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            return await repo.FindAsync(f, connection).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Company>> ReadAllAsync()
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            return await repo.ReadAllAsync(connection).ConfigureAwait(false);
        }

        public async Task<Company> ReadByIdAsync(int id)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            var account = await repo.ReadByIdAsync(id, connection).ConfigureAwait(false);
            return account;
        }

        public async Task<IEnumerable<Company>> ReadListByIdAsync(int id)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            return await repo.ReadListByIdAsync(id, connection).ConfigureAwait(false);
        }

        public async Task UpdateAsync(Company entity)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            await repo.UpdateAsync(entity, connection);
        }
    }
}
