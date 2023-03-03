using System.Data;
using Dapper;
using RMS.DATA.BaseRepositories;
using RMS.DATA.Entities;

namespace RMS.DATA.Repositories
{
    internal class AcquiringRepository : IRepositoryMin<Acquiring, int>
    {
        #region SQL
        private static readonly string Select = "SELECT AccountId, Comission, Tarif, WriteOffOther, IsActive FROM [Acquiring];";
        private static readonly string SelectById = "SELECT AccountId, Comission, Tarif, WriteOffOther, IsActive FROM [Acquiring] WHERE AccountId=@AccountId;";
        private static readonly string Update = "UPDATE [Acquiring] " +
            "SET Comission=@Comission, Tarif=@Tarif, WriteOffOther=@WriteOffOther, IsActive=@IsActive " +
            "WHERE AccountId=@AccountId;";
        private static readonly string Insert = "INSERT INTO [Acquiring] " +
            "(AccountId, Comission, Tarif, WriteOffOther, IsActive) " +
            "VALUES (@AccountId, @Comission, @Tarif, @WriteOffOther, @IsActive);";
        private static readonly string Delete = "DELETE FROM [Acquiring] WHERE AccountId=@AccountId;";
        #endregion

        public async Task<Acquiring> CreateAsync(Acquiring entity, IDbConnection connection)
        {
            connection.Open();
            var parameters = new DynamicParameters();
            parameters.Add("AccountId", entity.AccountId);
            parameters.Add("Comission", entity.Comission);
            parameters.Add("Tarif", entity.Tarif);
            parameters.Add("WriteOffOther", entity.WriteOffOther);
            parameters.Add("IsActive", entity.IsActive);
            await connection.ExecuteAsync(Insert, parameters).ConfigureAwait(false);
            connection.Close();
            return entity;
        }

        public async Task DeleteAsync(Acquiring entity, IDbConnection connection)
        {
            var parameters = new DynamicParameters();
            parameters.Add("AccountId", entity.AccountId);
            await connection.ExecuteAsync(Delete, parameters).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Acquiring>> ReadAllAsync(IDbConnection connection)
        {
            return await connection.QueryAsync<Acquiring>(Select).ConfigureAwait(false);
        }

        public async Task<Acquiring> ReadByIdAsync(int id, IDbConnection connection)
        {
            var parameters = new DynamicParameters();
            parameters.Add("AccountId", id);
            return await connection.QueryFirstOrDefaultAsync<Acquiring>(SelectById, parameters).ConfigureAwait(false);
        }

        public async Task UpdateAsync(Acquiring entity, IDbConnection connection)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Comission", entity.Comission);
            parameters.Add("Tarif", entity.Tarif);
            parameters.Add("WriteOffOther", entity.WriteOffOther);
            parameters.Add("IsActive", entity.IsActive);
            parameters.Add("AccountId", entity.AccountId);
            await connection.ExecuteAsync(Update, parameters).ConfigureAwait(false);
        }
    }
}
