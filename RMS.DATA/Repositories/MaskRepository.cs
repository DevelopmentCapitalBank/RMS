﻿using System.Data;
using Dapper;
using RMS.DATA.BaseRepositories;
using RMS.DATA.Entities;

namespace RMS.DATA.Repositories
{
    internal class MaskRepository : IRepositoryStandart<Mask>
    {
        #region SQL
        private static readonly string Update = "UPDATE [Mask] SET MaskTypeId=@MaskTypeId, Content=@Content, SequenceNumber=@SequenceNumber WHERE MaskId=@MaskId;";
        private static readonly string Insert = "INSERT INTO [Mask] (MaskTypeId, Content, SequenceNumber) VALUES (@MaskTypeId, @Content, @SequenceNumber);";
        private static readonly string Delete = "DELETE FROM [Mask] WHERE MaskId=@MaskId;";
        private static readonly string Select = "SELECT MaskId, MaskTypeId, Content, SequenceNumber FROM [Mask];";
        private static readonly string SqlIdentity = "SELECT last_insert_rowid()";
        #endregion

        public async Task<Mask> CreateAsync(Mask entity, IDbConnection connection)
        {
            connection.Open();
            var parameters = new DynamicParameters();
            parameters.Add("MaskTypeId", entity.MaskTypeId);
            parameters.Add("Content", entity.Content);
            parameters.Add("SequenceNumber", entity.SequenceNumber);
            await connection.ExecuteAsync(Insert, parameters).ConfigureAwait(false);

            int? MaskId = await connection.QueryFirstOrDefaultAsync<int>(SqlIdentity).ConfigureAwait(false);
            entity.MaskId = MaskId.Value;
            connection.Close();
            return entity;
        }

        public async Task<IEnumerable<Mask>> CreateListOfEntitiesAsync(IEnumerable<Mask> list, IDbConnection connection)
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                foreach (var entity in list)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("MaskTypeId", entity.MaskTypeId);
                    parameters.Add("Content", entity.Content);
                    await connection.ExecuteAsync(Insert, parameters).ConfigureAwait(false);

                    int? MaskId = await connection.QueryFirstOrDefaultAsync<int>(SqlIdentity).ConfigureAwait(false);
                    entity.MaskId = MaskId.Value;
                }
                transaction.Commit();
            }
            connection.Close();
            return list;
        }

        public async Task DeleteAsync(Mask entity, IDbConnection connection)
        {
            var parameters = new DynamicParameters();
            parameters.Add("MaskId", entity.MaskId);
            await connection.ExecuteAsync(Delete, parameters).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Mask>> ReadAllAsync(IDbConnection connection)
        {
            return await connection.QueryAsync<Mask>(Select).ConfigureAwait(false);
        }

        public async Task UpdateAsync(Mask entity, IDbConnection connection)
        {
            var parameters = new DynamicParameters();
            parameters.Add("MaskTypeId", entity.MaskTypeId);
            parameters.Add("Content", entity.Content);
            parameters.Add("MaskId", entity.MaskId);
            parameters.Add("SequenceNumber", entity.SequenceNumber);
            await connection.ExecuteAsync(Update, parameters).ConfigureAwait(false);
        }

        public async Task UpdateListOfEntitiesAsync(IEnumerable<Mask> items, IDbConnection connection)
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                foreach (var entity in items)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("MaskTypeId", entity.MaskTypeId);
                    parameters.Add("Content", entity.Content);
                    parameters.Add("MaskId", entity.MaskId);
                    parameters.Add("SequenceNumber", entity.SequenceNumber);
                    await connection.ExecuteAsync(Update, parameters).ConfigureAwait(false);
                }
                transaction.Commit();
            }
            connection.Close();
        }
    }
}
