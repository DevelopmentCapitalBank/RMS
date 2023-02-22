using Microsoft.Data.Sqlite;
using RMS.DATA.BaseRepositories;
using RMS.DATA.BaseServices;
using RMS.DATA.Views;

namespace RMS.DATA.Services
{
    internal class CompanyViewService : IServiceFind<CompanyView, CompanyView>
    {
        private readonly IRepositoryFind<CompanyView, CompanyView> repo;
        private readonly DbConfig dbConfig;
        public CompanyViewService(DbConfig dbConfig,
            IRepositoryFind<CompanyView, CompanyView> repo)
        {
            this.dbConfig = dbConfig;
            this.repo = repo;
        }
        public async Task<IEnumerable<CompanyView>> FindAsync(CompanyView f)
        {
            using var connection = new SqliteConnection(dbConfig.Name);
            return await repo.FindAsync(f, connection).ConfigureAwait(false);
        }
    }
}
