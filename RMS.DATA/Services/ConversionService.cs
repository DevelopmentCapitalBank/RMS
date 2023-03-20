using Microsoft.Data.Sqlite;
using RMS.DATA.BaseRepositories;
using RMS.DATA.BaseServices;
using RMS.DATA.Entities;

namespace RMS.DATA.Services
{
    internal class ConversionService : IServiceUnload<Conversion, DateTime, DateTime>
    {
        private readonly IRepositoryUnload<Conversion, DateTime, DateTime> repo;
        private readonly DbConfig dbConfig;
        public ConversionService(DbConfig dbConfig, IRepositoryUnload<Conversion, DateTime, DateTime> repo)
        {
            this.repo = repo;
            this.dbConfig = dbConfig;
        }

        public async Task<IEnumerable<Conversion>> CreateListOfEntitiesAsync(IEnumerable<Conversion> list)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            return await repo.CreateListOfEntitiesAsync(list, connection).ConfigureAwait(false);
        }

        public async Task DeleteByConditionAsync(DateTime condition)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            await repo.DeleteByConditionAsync(condition, connection).ConfigureAwait(false);
        }

        public async Task<int> GetCountAsync(DateTime value)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            return await repo.GetCountAsync(value, connection).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Conversion>> ReadBetweenAsync(DateTime v1, DateTime v2)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            return await repo.ReadBetweenAsync(v1, v2, connection).ConfigureAwait(false);
        }
    }
}
