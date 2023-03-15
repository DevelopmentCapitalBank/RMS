using System.Data;
using Dapper;
using RMS.DATA.BaseRepositories;
using RMS.DATA.Entities;

namespace RMS.DATA.Repositories
{
    internal class CompanyRepository : IRepositoryExtended<Company, int, string>
    {
        #region SQL
        private static readonly string Select = "SELECT CompanyId, ManagerId, GroupId, Name, " +
            "IsActive, IsAttraction, Inn, Comment FROM [Company] WHERE CompanyId=@CompanyId;";
        private static readonly string SelectAll = "SELECT CompanyId, ManagerId, GroupId, Name, " +
            "IsActive, IsAttraction, Inn, Comment FROM [Company];";
        private static readonly string SelectByGroupId = "SELECT CompanyId, ManagerId, GroupId, Name, " +
            "IsActive ,IsAttraction, Inn, Comment FROM [Company] WHERE GroupId=@GroupId;";        
        private static readonly string SelectByName = "SELECT CompanyId, ManagerId, GroupId, Name, " +
            "IsActive ,IsAttraction, Inn, Comment " +
            "FROM [Company] " +
            "WHERE Name LIKE @f " +
            "LIMIT 50;";        
        private static readonly string Update = "UPDATE [Company] " +
            "SET ManagerId=@ManagerId, GroupId=@GroupId, Name=@Name, IsActive=@IsActive, IsAttraction=@IsAttraction, Inn=@Inn, Comment=@Comment " +
            "WHERE CompanyId=@CompanyId;";        
        private static readonly string Insert = "INSERT INTO [Company] " +
            "(CompanyId, ManagerId, GroupId, Name, IsActive, IsAttraction, Inn, Comment) " +
            "VALUES (@CompanyId, @ManagerId, @GroupId, @Name, @IsActive, @IsAttraction, @Inn, @Comment);";        
        private static readonly string Delete = "DELETE FROM [Company] WHERE CompanyId=@CompanyId;";
        #endregion

        public async Task<Company> CreateAsync(Company entity, IDbConnection connection)
        {
            connection.Open();
            var parameters = new DynamicParameters();
            parameters.Add("CompanyId", entity.CompanyId);
            parameters.Add("ManagerId", entity.ManagerId);
            parameters.Add("GroupId", entity.GroupId);
            parameters.Add("Name", entity.Name);
            parameters.Add("IsActive", entity.IsActive);
            parameters.Add("IsAttraction", entity.IsAttraction);
            parameters.Add("Inn", entity.Inn);
            parameters.Add("Comment", entity.Comment);
            await connection.ExecuteAsync(Insert, parameters).ConfigureAwait(false);
            connection.Close();
            return entity;
        }

        public async Task CreateListOfEntitiesAsync(IEnumerable<Company> list, IDbConnection connection)
        {
            connection.Open();
            foreach (var c in list)
            {
                var parameters = new DynamicParameters();
                parameters.Add("CompanyId", c.CompanyId);
                parameters.Add("ManagerId", c.ManagerId);
                parameters.Add("GroupId", c.GroupId);
                parameters.Add("Name", c.Name);
                parameters.Add("IsActive", c.IsActive);
                parameters.Add("IsAttraction", c.IsAttraction);
                parameters.Add("Inn", c.Inn);
                parameters.Add("Comment", c.Comment);
                await connection.ExecuteAsync(Insert, parameters).ConfigureAwait(false);
            }
            connection.Close();
        }

        public async Task DeleteAsync(Company entity, IDbConnection connection)
        {
            var parameters = new DynamicParameters();
            parameters.Add("CompanyId", entity.CompanyId);
            await connection.ExecuteAsync(Delete, parameters).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Company>> FindAsync(string f, IDbConnection connection)
        {
            return await connection.QueryAsync<Company>(SelectByName, new { f }).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Company>> ReadAllAsync(IDbConnection connection)
        {
            return await connection.QueryAsync<Company>(SelectAll).ConfigureAwait(false);
        }

        public async Task<Company> ReadByIdAsync(int id, IDbConnection connection)
        {
            var parameters = new DynamicParameters();
            parameters.Add("CompanyId", id);
            return await connection.QueryFirstOrDefaultAsync<Company>(Select, parameters).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Company>> ReadListByIdAsync(int id, IDbConnection connection)
        {
            var parameters = new DynamicParameters();
            parameters.Add("GroupId", id);
            return await connection.QueryAsync<Company>(SelectByGroupId, parameters).ConfigureAwait(false);
        }

        public async Task UpdateAsync(Company entity, IDbConnection connection)
        {
            var parameters = new DynamicParameters();
            parameters.Add("ManagerId", entity.ManagerId);
            parameters.Add("GroupId", entity.GroupId);
            parameters.Add("Name", entity.Name);
            parameters.Add("IsActive", entity.IsActive);
            parameters.Add("IsAttraction", entity.IsAttraction);
            parameters.Add("Inn", entity.Inn);
            parameters.Add("Comment", entity.Comment);
            parameters.Add("CompanyId", entity.CompanyId);
            await connection.ExecuteAsync(Update, parameters).ConfigureAwait(false);
        }

        public async Task UpdateListOfEntitiesAsync(IEnumerable<Company> items, IDbConnection connection)
        {
            connection.Open();
            foreach (var entity in items)
            {
                var parameters = new DynamicParameters();
                parameters.Add("ManagerId", entity.ManagerId);
                parameters.Add("GroupId", entity.GroupId);
                parameters.Add("Name", entity.Name);
                parameters.Add("IsActive", entity.IsActive);
                parameters.Add("IsAttraction", entity.IsAttraction);
                parameters.Add("Inn", entity.Inn);
                parameters.Add("Comment", entity.Comment);
                parameters.Add("CompanyId", entity.CompanyId);
                await connection.ExecuteAsync(Update, parameters).ConfigureAwait(false);
            }
            connection.Close();
        }
    }
}
