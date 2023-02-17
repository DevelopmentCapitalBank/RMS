using RMS.DATA.BaseServices;
using RMS.DATA.Entities;
using RMS.DATA.Repositories;
using RMS.DATA.Services;

namespace RMS.DATA
{
    public class DbContext
    {
        public DbContext(DbConfig dbConfig)
        {
            Groups = new GroupService(dbConfig, new GroupRepository());
        }

        public IServiceMin<Group, int> Groups { get; private set; }
    }
}
