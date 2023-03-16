using System.Data;
using Dapper;
using RMS.DATA.BaseRepositories;
using RMS.DATA.Entities;

namespace RMS.DATA.Repositories
{
    internal class ConversionRepository : IRepositoryUnload<Conversion, DateTime, DateTime>
    {
        #region SQL
        private static readonly string Insert = "INSERT INTO [Conversion] " +
            "(DateOperation, TypeOfTransaction, ReceivesAmount, ReceivedCurrency, GivesAmount, GivesCurrency, RateCurrencyOfCrediting, RateCurrencyOfDebiting, ReceivesToAccount, GivesFromAccount) " +
            "VALUES(@DateOperation, @TypeOfTransaction, @ReceivesAmount, @ReceivedCurrency, @GivesAmount, @GivesCurrency, @RateCurrencyOfCrediting, @RateCurrencyOfDebiting, @ReceivesToAccount, @GivesFromAccount);";
        private static readonly string Delete = "DELETE FROM [Conversion] WHERE DateOperation BETWEEN @d1 AND @d2;";
        private static readonly string Read = "SELECT * FROM [Conversion] WHERE DateOperation BETWEEN @v1 AND @v2 ORDER BY DateOperation DESC;";
        #endregion
        public async Task<IEnumerable<Conversion>> CreateListOfEntitiesAsync(IEnumerable<Conversion> list, IDbConnection connection)
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                foreach (var c in list)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("DateOperation", c.DateOperation);
                    parameters.Add("TypeOfTransaction", c.TypeOfTransaction);
                    parameters.Add("ReceivesAmount", c.ReceivesAmount);
                    parameters.Add("ReceivedCurrency", c.ReceivedCurrency);
                    parameters.Add("GivesAmount", c.GivesAmount);
                    parameters.Add("GivesCurrency", c.GivesCurrency);
                    parameters.Add("RateCurrencyOfCrediting", c.RateCurrencyOfCrediting);
                    parameters.Add("RateCurrencyOfDebiting", c.RateCurrencyOfDebiting);
                    parameters.Add("ReceivesToAccount", c.ReceivesToAccount);
                    parameters.Add("GivesFromAccount", c.GivesFromAccount);
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
            parameters.Add("d1", condition);
            parameters.Add("d2", new DateTime(condition.Year, condition.Month + 1, 1).AddDays(-1));
            await connection.ExecuteAsync(Delete, parameters).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Conversion>> ReadBetweenAsync(DateTime v1, DateTime v2, IDbConnection connection)
        {
            return await connection.QueryAsync<Conversion>(Read, new { v1, v2 }).ConfigureAwait(false);
        }
    }
}
