using System.Data;

namespace RMS.DATA.BaseRepositories
{
    internal interface IRepositoryCreateList<T> where T : class
    {
        Task CreateListOfEntitiesAsync(IEnumerable<T> list, IDbConnection connection);
    }
}
