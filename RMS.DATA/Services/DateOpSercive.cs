using Microsoft.Data.Sqlite;
using RMS.DATA.BaseRepositories;
using RMS.DATA.BaseServices;
using RMS.DATA.Entities;

namespace RMS.DATA.Services
{
    internal class DateOpSercive : IServiceStandart<DateOp>
    {
        private readonly IRepositoryStandart<DateOp> repo;
        private readonly DbConfig dbConfig;
        public DateOpSercive(DbConfig dbConfig,
            IRepositoryStandart<DateOp> repo)
        {
            this.dbConfig = dbConfig;
            this.repo = repo;
        }

        public async Task<DateOp> CreateAsync(DateOp entity)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            return await repo.CreateAsync(entity, connection).ConfigureAwait(false);
        }

        public async Task<IEnumerable<DateOp>> CreateListOfEntitiesAsync(IEnumerable<DateOp> list)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            return await repo.CreateListOfEntitiesAsync(list, connection).ConfigureAwait(false);
        }

        public async Task DeleteAsync(DateOp entity)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            await repo.DeleteAsync(entity, connection);
        }

        public async Task<IEnumerable<DateOp>> ReadAllAsync()
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            return await repo.ReadAllAsync(connection).ConfigureAwait(false);
        }

        public async Task UpdateAsync(DateOp entity)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            await repo.UpdateAsync(entity, connection);
        }

        public async Task UpdateListOfEntitiesAsync(IEnumerable<DateOp> items)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            await repo.UpdateListOfEntitiesAsync(items, connection);
        }
    }
}
