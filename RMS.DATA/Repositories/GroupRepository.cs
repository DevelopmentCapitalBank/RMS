using System.Data;
using Dapper;
using RMS.DATA.BaseRepositories;
using RMS.DATA.Entities;

namespace RMS.DATA.Repositories
{
    internal sealed class GroupRepository : IRepositoryStandart<Group>
    {
        #region SQL
        private static readonly string Update = "UPDATE [Group] SET Name=@Name, Comment=@Comment WHERE GroupId=@GroupId;";
        private static readonly string Insert = "INSERT INTO [Group] (Name, Comment) VALUES (@Name, @Comment);";
        private static readonly string Delete = "DELETE FROM [Group] WHERE GroupId=@GroupId;";
        private static readonly string Select = "SELECT GroupId, Name, Comment FROM [Group];";
        private static readonly string SqlIdentity = "SELECT last_insert_rowid()";
        #endregion

        public async Task<Group> CreateAsync(Group entity, IDbConnection connection)
        {
            connection.Open();
            var parameters = new DynamicParameters();
            parameters.Add("Name", entity.Name);
            parameters.Add("Comment", entity.Comment);
            await connection.ExecuteAsync(Insert, parameters).ConfigureAwait(false);

            int? GroupId = await connection.QueryFirstOrDefaultAsync<int>(SqlIdentity).ConfigureAwait(false);
            entity.GroupId = GroupId.Value;
            connection.Close();
            return entity;
        }

        public async Task<IEnumerable<Group>> CreateListOfEntitiesAsync(IEnumerable<Group> list, IDbConnection connection)
        {
            connection.Open(); 
            using (var transaction = connection.BeginTransaction())
            {
                foreach (var entity in list)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("Name", entity.Name);
                    parameters.Add("Comment", entity.Comment);
                    await connection.ExecuteAsync(Insert, parameters).ConfigureAwait(false);

                    int? GroupId = await connection.QueryFirstOrDefaultAsync<int>(SqlIdentity).ConfigureAwait(false);
                    entity.GroupId = GroupId.Value;
                }
                transaction.Commit();
            }
            connection.Close();
            return list;
        }

        public async Task DeleteAsync(Group entity, IDbConnection connection)
        {
            var parameters = new DynamicParameters();
            parameters.Add("GroupId", entity.GroupId);
            await connection.ExecuteAsync(Delete, parameters).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Group>> ReadAllAsync(IDbConnection connection)
        {
            return await connection.QueryAsync<Group>(Select).ConfigureAwait(false);
        }

        public async Task UpdateAsync(Group entity, IDbConnection connection)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Name", entity.Name);
            parameters.Add("Comment", entity.Comment);
            parameters.Add("GroupId", entity.GroupId);
            await connection.ExecuteAsync(Update, parameters).ConfigureAwait(false);
        }

        public async Task UpdateListOfEntitiesAsync(IEnumerable<Group> items, IDbConnection connection)
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                foreach (var entity in items)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("Name", entity.Name);
                    parameters.Add("Comment", entity.Comment);
                    parameters.Add("GroupId", entity.GroupId);
                    await connection.ExecuteAsync(Update, parameters).ConfigureAwait(false);
                }
                transaction.Commit();
            }
            connection.Close();
        }
    }
}
