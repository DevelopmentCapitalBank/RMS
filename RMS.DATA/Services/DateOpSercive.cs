using Microsoft.Data.Sqlite;
using RMS.DATA.BaseRepositories;
using RMS.DATA.BaseServices;
using RMS.DATA.Entities;

namespace RMS.DATA.Services
{
    internal class DateOpSercive : IServiceMin<DateOp, int>
    {
        private readonly IRepositoryMin<DateOp, int> repo;
        private readonly DbConfig dbConfig;
        public DateOpSercive(DbConfig dbConfig,
            IRepositoryMin<DateOp, int> repo)
        {
            this.dbConfig = dbConfig;
            this.repo = repo;
        }

        public async Task<DateOp> CreateAsync(DateOp entity)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            return await repo.CreateAsync(entity, connection).ConfigureAwait(false);
        }

        public async Task DeleteAsync(DateOp entity)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            await repo.DeleteAsync(entity, connection);
        }

        public async Task<DateOp> ReadByIdAsync(int id)
        {
            using var connection = new SqliteConnection(dbConfig.Name);

            return await repo.ReadByIdAsync(id, connection);
        }

        public async Task UpdateAsync(DateOp entity)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            await repo.UpdateAsync(entity, connection);
        }
    }
}
