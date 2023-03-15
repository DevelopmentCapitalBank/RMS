using System.Data;
using Dapper;
using RMS.DATA.BaseRepositories;
using RMS.DATA.Entities;

namespace RMS.DATA.Repositories
{
    internal class ManagerRepository : IRepositoryStandart<Manager>
    {
        #region SQL
        private static readonly string Select = "SELECT ManagerId, Name FROM [Manager];";
        private static readonly string Update = "UPDATE [Manager] SET Name=@Name WHERE ManagerId=@ManagerId;";
        private static readonly string Insert = "INSERT INTO [Manager] (Name) VALUES (@Name);";
        private static readonly string Delete = "DELETE FROM [Manager] WHERE ManagerId=@ManagerId;";
        private static readonly string SqlIdentity = "SELECT last_insert_rowid()";
        #endregion

        public async Task<Manager> CreateAsync(Manager entity, IDbConnection connection)
        {
            connection.Open();
            var parameters = new DynamicParameters();
            parameters.Add("Name", entity.Name);
            await connection.ExecuteAsync(Insert, parameters).ConfigureAwait(false);

            int? ManagerId = await connection.QueryFirstOrDefaultAsync<int>(SqlIdentity).ConfigureAwait(false);
            entity.ManagerId = ManagerId.Value;
            connection.Close();
            return entity;
        }

        public async Task CreateListOfEntitiesAsync(IEnumerable<Manager> list, IDbConnection connection)
        {
            connection.Open();
            foreach (var c in list)
            {
                var parameters = new DynamicParameters();
                parameters.Add("Name", c.Name);
                await connection.ExecuteAsync(Insert, parameters).ConfigureAwait(false);
            }
            connection.Close();
        }

        public async Task DeleteAsync(Manager entity, IDbConnection connection)
        {
            var parameters = new DynamicParameters();
            parameters.Add("ManagerId", entity.ManagerId);
            await connection.ExecuteAsync(Delete, parameters).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Manager>> ReadAllAsync(IDbConnection connection)
        {
            return await connection.QueryAsync<Manager>(Select).ConfigureAwait(false);
        }

        public async Task UpdateAsync(Manager entity, IDbConnection connection)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Name", entity.Name);
            parameters.Add("ManagerId", entity.ManagerId);
            await connection.ExecuteAsync(Update, parameters).ConfigureAwait(false);
        }

        public async Task UpdateListOfEntitiesAsync(IEnumerable<Manager> items, IDbConnection connection)
        {
            connection.Open();
            foreach (var entity in items)
            {
                var parameters = new DynamicParameters();
                parameters.Add("Name", entity.Name);
                parameters.Add("ManagerId", entity.ManagerId);
                await connection.ExecuteAsync(Update, parameters).ConfigureAwait(false);
            }
            connection.Close();
        }
    }
}
