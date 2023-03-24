using System.Data;
using Dapper;
using RMS.DATA.BaseRepositories;
using RMS.DATA.Entities;

namespace RMS.DATA.Repositories
{
    internal class MaskRepository : IRepositoryExtended<Mask, int, string>
    {
        #region SQL
        private static readonly string Update = "UPDATE [Mask] SET MaskTypeId=@MaskTypeId, Content=@Content, SequenceNumber=@SequenceNumber WHERE MaskId=@MaskId;";
        private static readonly string Insert = "INSERT INTO [Mask] (MaskTypeId, Content, SequenceNumber) VALUES (@MaskTypeId, @Content, @SequenceNumber);";
        private static readonly string Delete = "DELETE FROM [Mask] WHERE MaskId=@MaskId;";
        private static readonly string Select = "SELECT * FROM [Mask];";
        private static readonly string SelectByContent = "SELECT * FROM [Mask] WHERE Content LIKE @f;";
        private static readonly string SelectByMaskId = "SELECT * FROM [Mask] WHERE MaskId=@MaskId;";
        private static readonly string SelectByMaskTypeId = "SELECT * FROM [Mask] WHERE MaskTypeId=@MaskTypeId;";
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

        public async Task<IEnumerable<Mask>> FindAsync(string f, IDbConnection connection)
        {
            return await connection.QueryAsync<Mask>(SelectByContent, new { f }).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Mask>> ReadAllAsync(IDbConnection connection)
        {
            return await connection.QueryAsync<Mask>(Select).ConfigureAwait(false);
        }

        public async Task<Mask> ReadByIdAsync(int id, IDbConnection connection)
        {
            var parameters = new DynamicParameters();
            parameters.Add("MaskId", id);
            return await connection.QueryFirstOrDefaultAsync<Mask>(SelectByMaskId, parameters).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Mask>> ReadListByIdAsync(int id, IDbConnection connection)
        {
            var parameters = new DynamicParameters();
            parameters.Add("MaskTypeId", id);
            return await connection.QueryAsync<Mask>(SelectByMaskTypeId, parameters).ConfigureAwait(false);
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
