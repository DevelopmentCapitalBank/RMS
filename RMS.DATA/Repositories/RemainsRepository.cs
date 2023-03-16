using System.Data;
using Dapper;
using RMS.DATA.BaseRepositories;
using RMS.DATA.Entities;

namespace RMS.DATA.Repositories
{
    internal class RemainsRepository : IRepositoryUnload<Remains, DateTime, DateTime>
    {
        #region SQL
        private static readonly string Insert = "INSERT INTO [Remains] " +
            "(DateOfUnloading, Account, Debit, Credit, AverageBalance) " +
            "VALUES(@DateOfUnloading, @Account, @Debit, @Credit, @AverageBalance);";
        private static readonly string Delete = "DELETE FROM [Remains] WHERE DateOfUnloading=@d;";
        private static readonly string Read = "SELECT * FROM [Remains] WHERE DateOfUnloading BETWEEN @v1 AND @v2 ORDER BY DateOfUnloading DESC;";
        #endregion
        public async Task<IEnumerable<Remains>> CreateListOfEntitiesAsync(IEnumerable<Remains> list, IDbConnection connection)
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
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
                transaction.Commit();
            }
            connection.Close();
            return list;
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
