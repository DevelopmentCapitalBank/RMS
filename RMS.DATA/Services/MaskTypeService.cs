using Microsoft.Data.Sqlite;
using RMS.DATA.BaseRepositories;
using RMS.DATA.BaseServices;
using RMS.DATA.Entities;

namespace RMS.DATA.Services
{
    internal class MaskTypeService : IServiceStandart<MaskType>
    {
        private readonly IRepositoryStandart<MaskType> repo;
        private readonly DbConfig dbConfig;
        public MaskTypeService(DbConfig dbConfig, IRepositoryStandart<MaskType> repo)
        {
            this.repo = repo;
            this.dbConfig = dbConfig;
        }

        public async Task<MaskType> CreateAsync(MaskType entity)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            return await repo.CreateAsync(entity, connection).ConfigureAwait(false);
        }

        public async Task<IEnumerable<MaskType>> CreateListOfEntitiesAsync(IEnumerable<MaskType> list)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            return await repo.CreateListOfEntitiesAsync(list, connection).ConfigureAwait(false);
        }

        public async Task DeleteAsync(MaskType entity)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            await repo.DeleteAsync(entity, connection);
        }

        public async Task<IEnumerable<MaskType>> ReadAllAsync()
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            return await repo.ReadAllAsync(connection).ConfigureAwait(false);
        }

        public async Task UpdateAsync(MaskType entity)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            await repo.UpdateAsync(entity, connection);
        }

        public async Task UpdateListOfEntitiesAsync(IEnumerable<MaskType> items)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            await repo.UpdateListOfEntitiesAsync(items, connection);
        }
    }
}
