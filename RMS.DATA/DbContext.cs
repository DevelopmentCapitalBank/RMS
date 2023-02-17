using Dapper;
using Microsoft.Data.Sqlite;
using RMS.DATA.BaseServices;
using RMS.DATA.Entities;
using RMS.DATA.Repositories;
using RMS.DATA.Services;

namespace RMS.DATA
{
    public class DbContext
    {
        private readonly DbConfig dbConfig;
        public DbContext(DbConfig dbConfig)
        {
            this.dbConfig = dbConfig;
            Groups = new GroupService(dbConfig, new GroupRepository());
        }

        public IServiceMin<Group, int> Groups { get; private set; }

        public async Task Setup()
        {
            try
            { 
                using var connection = new SqliteConnection(dbConfig.Name);

                var table = await connection.QueryFirstOrDefaultAsync<string>("SELECT name FROM sqlite_master WHERE type='table' AND name = 'Group';").ConfigureAwait(false);
                if (!string.IsNullOrEmpty(table) && table == "Group")
                    return;

                connection.Execute("CREATE TABLE [Group] (" +
                                        "GroupId INTEGER       NOT NULL," +
                                        "Name    VARCHAR(100) NOT NULL," +
                                        "Comment  TEXT," +
                                    "PRIMARY KEY(GroupId AUTOINCREMENT)); ");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
