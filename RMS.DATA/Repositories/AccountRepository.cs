﻿using System.Data;
using Dapper;
using RMS.DATA.BaseRepositories;
using RMS.DATA.Entities;

namespace RMS.DATA.Repositories
{
    internal class AccountRepository : IRepositoryExtended<Account, int, string>
    {
        #region SQL
        private static readonly string Select = "SELECT AccountId, CompanyId, OfficeId, DateOpen, " +
            "DateClose, DateTimeLastOperation, AccountNumber FROM [Account] WHERE AccountId=@AccountId;";
        private static readonly string SelectAll = "SELECT AccountId, CompanyId, OfficeId, DateOpen, " +
            "DateClose, DateTimeLastOperation, AccountNumber FROM [Account];";
        private static readonly string SelectByCompanyId = "SELECT AccountId, CompanyId, OfficeId, DateOpen, " +
            "DateClose, DateTimeLastOperation, AccountNumber FROM [Account] WHERE CompanyId=@CompanyId;";
        private static readonly string SelectByAccountNumber = "SELECT AccountId, CompanyId, OfficeId, DateOpen, " +
            "DateClose, DateTimeLastOperation, AccountNumber " +
            "FROM [Account] " +
            "WHERE AccountNumber LIKE @f " +
            "LIMIT 50;";
        private static readonly string Update = "UPDATE [Account] " +
            "SET CompanyId=@CompanyId, OfficeId=@OfficeId, DateOpen=@DateOpen, DateClose=@DateClose, DateTimeLastOperation=@DateTimeLastOperation, AccountNumber=@AccountNumber " +
            "WHERE AccountId=@AccountId;";
        private static readonly string Insert = "INSERT INTO [Account] " +
            "(CompanyId, OfficeId, DateOpen, DateClose, DateTimeLastOperation, AccountNumber) " +
            "VALUES (@CompanyId, @OfficeId, @DateOpen, @DateClose, @DateTimeLastOperation, @AccountNumber);";
        private static readonly string Delete = "DELETE FROM [Account] WHERE AccountId=@AccountId;";
        private static readonly string SqlIdentity = "SELECT last_insert_rowid()";
        #endregion

        public async Task<Account> CreateAsync(Account entity, IDbConnection connection)
        {
            connection.Open();
            var parameters = new DynamicParameters();
            parameters.Add("CompanyId", entity.CompanyId);
            parameters.Add("OfficeId", entity.OfficeId);
            parameters.Add("DateOpen", entity.DateOpen);
            parameters.Add("DateClose", entity.DateClose);
            parameters.Add("DateTimeLastOperation", entity.DateTimeLastOperation);
            parameters.Add("AccountNumber", entity.AccountNumber);
            await connection.ExecuteAsync(Insert, parameters).ConfigureAwait(false);

            int? AccountId = await connection.QueryFirstOrDefaultAsync<int>(SqlIdentity).ConfigureAwait(false);
            entity.AccountId = AccountId.Value;
            connection.Close();
            return entity;
        }

        public async Task<IEnumerable<Account>> CreateListOfEntitiesAsync(IEnumerable<Account> list, IDbConnection connection)
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                foreach (var c in list)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("CompanyId", c.CompanyId);
                    parameters.Add("OfficeId", c.OfficeId);
                    parameters.Add("DateOpen", c.DateOpen);
                    parameters.Add("DateClose", c.DateClose);
                    parameters.Add("DateTimeLastOperation", c.DateTimeLastOperation);
                    parameters.Add("AccountNumber", c.AccountNumber);
                    await connection.ExecuteAsync(Insert, parameters).ConfigureAwait(false);

                    int? AccountId = await connection.QueryFirstOrDefaultAsync<int>(SqlIdentity).ConfigureAwait(false);
                    c.AccountId = AccountId.Value;
                }
                transaction.Commit();
            }
            connection.Close();
            return list;
        }

        public async Task DeleteAsync(Account entity, IDbConnection connection)
        {
            var parameters = new DynamicParameters();
            parameters.Add("AccountId", entity.AccountId);
            await connection.ExecuteAsync(Delete, parameters).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Account>> FindAsync(string f, IDbConnection connection)
        {
            return await connection.QueryAsync<Account>(SelectByAccountNumber, new { f }).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Account>> ReadAllAsync(IDbConnection connection)
        {
            return await connection.QueryAsync<Account>(SelectAll).ConfigureAwait(false);
        }

        public async Task<Account> ReadByIdAsync(int id, IDbConnection connection)
        {
            var parameters = new DynamicParameters();
            parameters.Add("AccountId", id);
            return await connection.QueryFirstOrDefaultAsync<Account>(Select, parameters).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Account>> ReadListByIdAsync(int id, IDbConnection connection)
        {
            var parameters = new DynamicParameters();
            parameters.Add("CompanyId", id);
            return await connection.QueryAsync<Account>(SelectByCompanyId, parameters).ConfigureAwait(false);
        }

        public async Task UpdateAsync(Account entity, IDbConnection connection)
        {
            var parameters = new DynamicParameters();
            parameters.Add("CompanyId", entity.CompanyId);
            parameters.Add("OfficeId", entity.OfficeId);
            parameters.Add("DateOpen", entity.DateOpen);
            parameters.Add("DateClose", entity.DateClose);
            parameters.Add("DateTimeLastOperation", entity.DateTimeLastOperation);
            parameters.Add("AccountNumber", entity.AccountNumber);
            parameters.Add("AccountId", entity.AccountId);
            await connection.ExecuteAsync(Update, parameters).ConfigureAwait(false);
        }

        public async Task UpdateListOfEntitiesAsync(IEnumerable<Account> items, IDbConnection connection)
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                foreach (var entity in items)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("CompanyId", entity.CompanyId);
                    parameters.Add("OfficeId", entity.OfficeId);
                    parameters.Add("DateOpen", entity.DateOpen);
                    parameters.Add("DateClose", entity.DateClose);
                    parameters.Add("DateTimeLastOperation", entity.DateTimeLastOperation);
                    parameters.Add("AccountNumber", entity.AccountNumber);
                    parameters.Add("AccountId", entity.AccountId);
                    await connection.ExecuteAsync(Update, parameters).ConfigureAwait(false);
                }
                transaction.Commit();
            }
            connection.Close();
        }
    }
}
