using Microsoft.Data.Sqlite;
using RMS.DATA.BaseRepositories;
using RMS.DATA.BaseServices;
using RMS.DATA.Entities;

namespace RMS.DATA.Services
{
    internal class AcquiringService : IServiceMin<Acquiring, int>
    {
        private readonly IRepositoryMin<Acquiring, int> repo;
        private readonly DbConfig dbConfig;
        public AcquiringService(DbConfig dbConfig,
            IRepositoryMin<Acquiring, int> repo)
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

        public async Task<Acquiring> ReadByIdAsync(int id)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            var acq = await repo.ReadByIdAsync(id, connection).ConfigureAwait(false);
            return acq;
        }

        public async Task UpdateAsync(Acquiring entity)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            await repo.UpdateAsync(entity, connection);
        }
    }
}
