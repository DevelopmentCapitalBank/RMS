using System.Data;
using Dapper;
using RMS.DATA.BaseRepositories;
using RMS.DATA.Entities;

namespace RMS.DATA.Repositories
{
    internal class ExchangeRateRepository : IRepositoryStandart<ExchangeRate>
    {
        #region SQL
        private static readonly string Select = "SELECT Iso, DateId, Rate FROM [ExchangeRate];";
        private static readonly string Update = "UPDATE [ExchangeRate] SET Rate=@Rate WHERE Iso=@Iso AND DateId=@DateId ;";
        private static readonly string Insert = "INSERT INTO [ExchangeRate] (Iso, DateId, Rate) VALUES (@Iso, @DateId, @Rate);";
        private static readonly string Delete = "DELETE FROM [ExchangeRate] WHERE Iso=@Iso AND DateId=@DateId;";
        #endregion

        public async Task<ExchangeRate> CreateAsync(ExchangeRate entity, IDbConnection connection)
        {
            connection.Open();
            var parameters = new DynamicParameters();
            parameters.Add("Iso", entity.Iso);
            parameters.Add("DateId", entity.DateId);
            parameters.Add("Rate", entity.Rate);
            await connection.ExecuteAsync(Insert, parameters).ConfigureAwait(false);
            return entity;
        }

        public async Task DeleteAsync(ExchangeRate entity, IDbConnection connection)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Iso", entity.Iso);
            parameters.Add("DateId", entity.DateId);
            await connection.ExecuteAsync(Delete, parameters).ConfigureAwait(false);
        }

        public async Task<IEnumerable<ExchangeRate>> ReadAllAsync(IDbConnection connection)
        {
            return await connection.QueryAsync<ExchangeRate>(Select).ConfigureAwait(false);
        }

        public async Task UpdateAsync(ExchangeRate entity, IDbConnection connection)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Rate", entity.Rate);
            parameters.Add("Iso", entity.Iso);
            parameters.Add("DateId", entity.DateId);
            await connection.ExecuteAsync(Update, parameters).ConfigureAwait(false);
        }
    }
}
