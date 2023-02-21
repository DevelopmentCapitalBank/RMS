using System.Data;
using Dapper;
using RMS.DATA.BaseRepositories;
using RMS.DATA.Entities;

namespace RMS.DATA.Repositories
{
    internal class CurrencyRepository : IRepositoryStandart<Currency>
    {
        #region SQL
        private static readonly string Select = "SELECT Iso, Code, Description Name FROM [Currency];";
        private static readonly string Update = "UPDATE [Currency] SET Code=@Code, Description=@Description WHERE Iso=@Iso;";
        private static readonly string Insert = "INSERT INTO [Currency] (Iso, Code, Description) VALUES (@Iso, @Code, @Description);";
        private static readonly string Delete = "DELETE FROM [Currency] WHERE Iso=@Iso;";
        #endregion

        public async Task<Currency> CreateAsync(Currency entity, IDbConnection connection)
        {
            connection.Open();
            var parameters = new DynamicParameters();
            parameters.Add("Iso", entity.Iso);
            parameters.Add("Code", entity.Code);
            parameters.Add("Description", entity.Description);
            await connection.ExecuteAsync(Insert, parameters).ConfigureAwait(false);
            return entity;
        }

        public async Task DeleteAsync(Currency entity, IDbConnection connection)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Iso", entity.Iso);
            await connection.ExecuteAsync(Delete, parameters).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Currency>> ReadAllAsync(IDbConnection connection)
        {
            return await connection.QueryAsync<Currency>(Select).ConfigureAwait(false);
        }

        public async Task UpdateAsync(Currency entity, IDbConnection connection)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Code", entity.Code);
            parameters.Add("Description", entity.Description);
            parameters.Add("Iso", entity.Iso);
            await connection.ExecuteAsync(Update, parameters).ConfigureAwait(false);
        }
    }
}
