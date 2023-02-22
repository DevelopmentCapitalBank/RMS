using System.Data;
using Dapper;
using RMS.DATA.BaseRepositories;
using RMS.DATA.Views;

namespace RMS.DATA.Repositories
{
    internal class CompanyViewRepository : IRepositoryFind<CompanyView, CompanyView>
    {
        #region SQL
        private static readonly string sql1 = "SELECT * FROM [CompanyView] WHERE CompanyId=@CompanyId;";
        private static readonly string sql2 = "SELECT * FROM [CompanyView] WHERE CompanyName LIKE @CompanyName LIMIT 30;";
        private static readonly string sql3 = "SELECT * FROM [CompanyView] WHERE Inn LIKE @Inn LIMIT 30;";
        private static readonly string sql4 = "SELECT * FROM [CompanyView] WHERE Inn LIKE @Inn AND CompanyName LIKE @CompanyName LIMIT 30;";
        private static readonly string sql5 = "SELECT * FROM [CompanyView] LIMIT 30;";
        #endregion
        public async Task<IEnumerable<CompanyView>> FindAsync(CompanyView f, IDbConnection connection)
        {
            var parameters = new DynamicParameters();

            if (f.CompanyId > 0)
            {
                parameters.Add("CompanyId", f.CompanyId);
                return await connection.QueryAsync<CompanyView>(sql1, parameters).ConfigureAwait(false);
            }
            if (!string.IsNullOrEmpty(f.CompanyName) && string.IsNullOrEmpty(f.Inn))
            {
                parameters.Add("CompanyName", f.CompanyName);
                return await connection.QueryAsync<CompanyView>(sql2, parameters).ConfigureAwait(false);
            }
            else if (string.IsNullOrEmpty(f.CompanyName) && !string.IsNullOrEmpty(f.Inn))
            {
                parameters.Add("Inn", f.Inn);
                return await connection.QueryAsync<CompanyView>(sql3, parameters).ConfigureAwait(false);
            }
            else if (!string.IsNullOrEmpty(f.CompanyName) && !string.IsNullOrEmpty(f.Inn))
            {
                parameters.Add("Inn", f.Inn);
                parameters.Add("CompanyName", f.CompanyName);
                return await connection.QueryAsync<CompanyView>(sql4, parameters).ConfigureAwait(false);
            }
            else
            {
                return await connection.QueryAsync<CompanyView>(sql5).ConfigureAwait(false);
            }
        }
    }
}
