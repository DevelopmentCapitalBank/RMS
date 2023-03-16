using System.Data;
using Dapper;
using RMS.DATA.BaseRepositories;
using RMS.DATA.Entities;

namespace RMS.DATA.Repositories
{
    internal class OfficeRepository : IRepositoryStandart<Office>
    {
        #region SQL
        private static readonly string Select = "SELECT OfficeId, Name FROM [Office];";
        private static readonly string Update = "UPDATE [Office] SET Name=@Name WHERE OfficeId=@OfficeId;";
        private static readonly string Insert = "INSERT INTO [Office] (Name) VALUES (@Name);";
        private static readonly string Delete = "DELETE FROM [Office] WHERE OfficeId=@OfficeId;";
        private static readonly string SqlIdentity = "SELECT last_insert_rowid()";
        #endregion

        public async Task<Office> CreateAsync(Office entity, IDbConnection connection)
        {
            connection.Open();
            var parameters = new DynamicParameters();
            parameters.Add("Name", entity.Name);
            await connection.ExecuteAsync(Insert, parameters).ConfigureAwait(false);

            int? OfficeId = await connection.QueryFirstOrDefaultAsync<int>(SqlIdentity).ConfigureAwait(false);
            entity.OfficeId = OfficeId.Value;
            connection.Close();
            return entity;
        }

        public async Task<IEnumerable<Office>> CreateListOfEntitiesAsync(IEnumerable<Office> list, IDbConnection connection)
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                foreach (var c in list)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("Name", c.Name);
                    await connection.ExecuteAsync(Insert, parameters).ConfigureAwait(false);

                    int? OfficeId = await connection.QueryFirstOrDefaultAsync<int>(SqlIdentity).ConfigureAwait(false);
                    c.OfficeId = OfficeId.Value;
                }
                transaction.Commit();
            }
            connection.Close();
            return list;
        }

        public async Task DeleteAsync(Office entity, IDbConnection connection)
        {
            var parameters = new DynamicParameters();
            parameters.Add("OfficeId", entity.OfficeId);
            await connection.ExecuteAsync(Delete, parameters).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Office>> ReadAllAsync(IDbConnection connection)
        {
            return await connection.QueryAsync<Office>(Select).ConfigureAwait(false);
        }

        public async Task UpdateAsync(Office entity, IDbConnection connection)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Name", entity.Name);
            parameters.Add("OfficeId", entity.OfficeId);
            await connection.ExecuteAsync(Update, parameters).ConfigureAwait(false);
        }

        public async Task UpdateListOfEntitiesAsync(IEnumerable<Office> items, IDbConnection connection)
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                foreach (var entity in items)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("Name", entity.Name);
                    parameters.Add("OfficeId", entity.OfficeId);
                    await connection.ExecuteAsync(Update, parameters).ConfigureAwait(false);
                }
                transaction.Commit();
            }
            connection.Close();
        }
    }
}
