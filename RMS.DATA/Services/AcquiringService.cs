using Microsoft.Data.Sqlite;
using RMS.DATA.BaseRepositories;
using RMS.DATA.BaseServices;
using RMS.DATA.Entities;

namespace RMS.DATA.Services
{
    internal class AcquiringService : IServiceStandart<Acquiring>
    {
        private readonly IRepositoryStandart<Acquiring> repo;
        private readonly DbConfig dbConfig;
        public AcquiringService(DbConfig dbConfig,
            IRepositoryStandart<Acquiring> repo)
        {
            this.dbConfig = dbConfig;
            this.repo = repo;
        }

        public async Task<Acquiring> CreateAsync(Acquiring entity)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            return await repo.CreateAsync(entity, connection).ConfigureAwait(false);
        }

        public async Task DeleteAsync(Acquiring entity)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            await repo.DeleteAsync(entity, connection);
        }

        public async Task<IEnumerable<Acquiring>> ReadAllAsync()
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            return await repo.ReadAllAsync(connection).ConfigureAwait(false);
        }

        public async Task UpdateAsync(Acquiring entity)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            await repo.UpdateAsync(entity, connection);
        }
    }
}
