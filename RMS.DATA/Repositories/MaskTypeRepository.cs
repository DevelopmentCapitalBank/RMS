using System.Data;
using Dapper;
using RMS.DATA.BaseRepositories;
using RMS.DATA.Entities;

namespace RMS.DATA.Repositories
{
    internal class MaskTypeRepository : IRepositoryStandart<MaskType>
    {
        #region SQL
        private static readonly string Update = "UPDATE [MaskType] SET Name=@Name WHERE MaskTypeId=@MaskTypeId;";
        private static readonly string Insert = "INSERT INTO [MaskType] (Name) VALUES (@Name);";
        private static readonly string Delete = "DELETE FROM [MaskType] WHERE MaskTypeId=@MaskTypeId;";
        private static readonly string Select = "SELECT MaskTypeId, Name FROM [MaskType];";
        private static readonly string SqlIdentity = "SELECT last_insert_rowid()";
        #endregion

        public async Task<MaskType> CreateAsync(MaskType entity, IDbConnection connection)
        {
            connection.Open();
            var parameters = new DynamicParameters();
            parameters.Add("Name", entity.Name);
            await connection.ExecuteAsync(Insert, parameters).ConfigureAwait(false);

            int? MaskTypeId = await connection.QueryFirstOrDefaultAsync<int>(SqlIdentity).ConfigureAwait(false);
            entity.MaskTypeId = MaskTypeId.Value;
            connection.Close();
            return entity;
        }

        public async Task CreateListOfEntitiesAsync(IEnumerable<MaskType> list, IDbConnection connection)
        {
            connection.Open();
            foreach (var entity in list)
            {
                var parameters = new DynamicParameters();
                parameters.Add("Name", entity.Name);
                await connection.ExecuteAsync(Insert, parameters).ConfigureAwait(false);
            }
            connection.Close();
        }

        public async Task DeleteAsync(MaskType entity, IDbConnection connection)
        {
            var parameters = new DynamicParameters();
            parameters.Add("MaskTypeId", entity.MaskTypeId);
            await connection.ExecuteAsync(Delete, parameters).ConfigureAwait(false);
        }

        public async Task<IEnumerable<MaskType>> ReadAllAsync(IDbConnection connection)
        {
            return await connection.QueryAsync<MaskType>(Select).ConfigureAwait(false);
        }

        public async Task UpdateAsync(MaskType entity, IDbConnection connection)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Name", entity.Name);
            parameters.Add("MaskTypeId", entity.MaskTypeId);
            await connection.ExecuteAsync(Update, parameters).ConfigureAwait(false);
        }

        public async Task UpdateListOfEntitiesAsync(IEnumerable<MaskType> items, IDbConnection connection)
        {
            connection.Open();
            foreach (var entity in items)
            {
                var parameters = new DynamicParameters();
                parameters.Add("Name", entity.Name);
                parameters.Add("MaskTypeId", entity.MaskTypeId);
                await connection.ExecuteAsync(Update, parameters).ConfigureAwait(false);
            }
            connection.Close();
        }
    }
}
