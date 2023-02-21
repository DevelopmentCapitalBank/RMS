using System.Data;
using Dapper;
using RMS.DATA.BaseRepositories;
using RMS.DATA.Entities;

namespace RMS.DATA.Repositories
{
    internal class OperationRepository : IRepositoryUnload<Operation, DateTime, DateTime>
    {
        #region SQL
        private static readonly string Insert = "INSERT INTO [Operation] " +
            "(DateOfUnloading, Amount, AmountEquivalent, Purpose, Payer, Recipient, DateOperation) " +
            "VALUES(@DateOfUnloading, @Amount, @AmountEquivalent, @Purpose, @Payer, @Recipient, @DateOperation);";
        private static readonly string Delete = "DELETE FROM [Operation] WHERE DateOfUnloading=@d;";
        private static readonly string Read = "SELECT * FROM [Operation] WHERE DateOfUnloading BETWEEN @v1 AND @v2 ORDER BY DateOfUnloading DESC;";
        #endregion
        public async Task CreateListOfEntitiesAsync(IEnumerable<Operation> list, IDbConnection connection)
        {
            connection.Open();
            foreach (var c in list)
            {
                var parameters = new DynamicParameters();
                parameters.Add("DateOfUnloading", c.DateOfUnloading);
                parameters.Add("Amount", c.Amount);
                parameters.Add("AmountEquivalent", c.AmountEquivalent);
                parameters.Add("Purpose", c.Purpose);
                parameters.Add("Payer", c.Payer);
                parameters.Add("Recipient", c.Recipient);
                parameters.Add("DateOperation", c.DateOperation);
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

        public async Task<IEnumerable<Operation>> ReadBetweenAsync(DateTime v1, DateTime v2, IDbConnection connection)
        {
            return await connection.QueryAsync<Operation>(Read, new { v1, v2 }).ConfigureAwait(false);
        }
    }
}