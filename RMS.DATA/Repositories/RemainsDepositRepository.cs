using System.Data;
using Dapper;
using RMS.DATA.BaseRepositories;
using RMS.DATA.Entities;

namespace RMS.DATA.Repositories
{
    internal class RemainsDepositRepository : IRepositoryUnload<Remains, DateTime, DateTime>
    {
        #region SQL
        private static readonly string Insert = "INSERT INTO [RemainsDeposit] " +
            "(DateOfUnloading, Account, Debit, Credit, AverageBalance) " +
            "VALUES(@DateOfUnloading, @Account, @Debit, @Credit, @AverageBalance);";
        private static readonly string Delete = "DELETE FROM [RemainsDeposit] WHERE DateOfUnloading=@d;";
        private static readonly string Read = "SELECT * FROM [RemainsDeposit] WHERE DateOfUnloading BETWEEN @v1 AND @v2 ORDER BY DateOfUnloading DESC;";
        #endregion
        public async Task CreateListOfEntitiesAsync(IEnumerable<Remains> list, IDbConnection connection)
        {
            connection.Open();
            foreach (var c in list)
            {
                var parameters = new DynamicParameters();
                parameters.Add("DateOfUnloading", c.DateOfUnloading);
                parameters.Add("Account", c.Account);
                parameters.Add("Debit", c.Debit);
                parameters.Add("Credit", c.Credit);
                parameters.Add("AverageBalance", c.AverageBalance);
                await connection.ExecuteAsync(Insert, parameters).ConfigureAwait(false);
            }
            connection.Close();
        }

        public async Task DeleteByConditionAsync(DateTime condition, IDbConnection connection)
        {
            var parameters = new DynamicParameters();
            parameters.Add("d", condition);
            await connection.ExecuteAsync(Delete, parameters).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Remains>> ReadBetweenAsync(DateTime v1, DateTime v2, IDbConnection connection)
        {
            return await connection.QueryAsync<Remains>(Read, new { v1, v2 }).ConfigureAwait(false);
        }
    }
}
