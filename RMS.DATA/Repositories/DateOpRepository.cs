using System.Data;
using Dapper;
using RMS.DATA.BaseRepositories;
using RMS.DATA.Entities;

namespace RMS.DATA.Repositories
{
    internal sealed class DateOpRepository : IRepositoryMin<DateOp, int>
    {
        #region SQL
        private static readonly string Select = "SELECT DateId, DateOperation FROM [DateOp] WHERE DateId=@DateId;";
        private static readonly string Update = "UPDATE [DateOp] SET DateOperation=@DateOperation WHERE DateId=@DateId;";
        private static readonly string Insert = "INSERT INTO [DateOp] (DateOperation) VALUES (@DateOperation);";
        private static readonly string Delete = "DELETE FROM [DateOp] WHERE DateId=@DateId;";
        private static readonly string SqlIdentity = "SELECT last_insert_rowid()";
        #endregion
        public async Task<DateOp> CreateAsync(DateOp entity, IDbConnection connection)
        {
            connection.Open();
            var parameters = new DynamicParameters();
            parameters.Add("DateOperation", entity.DateOperation); 
            await connection.ExecuteAsync(Insert, parameters).ConfigureAwait(false);

            int? DateId = await connection.QueryFirstOrDefaultAsync<int>(SqlIdentity).ConfigureAwait(false);
            entity.DateId = DateId.Value;
            connection.Close();
            return entity;
        }

        public async Task DeleteAsync(DateOp entity, IDbConnection connection)
        {
            var parameters = new DynamicParameters();
            parameters.Add("DateId", entity.DateId);
            await connection.ExecuteAsync(Delete, parameters).ConfigureAwait(false);
        }

        public async Task<DateOp> ReadByIdAsync(int id, IDbConnection connection)
        {
            var parameters = new DynamicParameters();
            parameters.Add("DateId", id);
            return await connection.QueryFirstOrDefaultAsync<DateOp>(Select, parameters).ConfigureAwait(false);
        }

        public async Task UpdateAsync(DateOp entity, IDbConnection connection)
        {
            var parameters = new DynamicParameters();
            parameters.Add("DateOperation", entity.DateOperation);
            parameters.Add("DateId", entity.DateId);
            await connection.ExecuteAsync(Update, parameters).ConfigureAwait(false);
        }
    }
}
