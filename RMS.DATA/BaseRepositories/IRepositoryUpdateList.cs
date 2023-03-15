using System.Data;

namespace RMS.DATA.BaseRepositories
{
    internal interface IRepositoryUpdateList<T> where T : class
    {
        Task UpdateListOfEntitiesAsync(IEnumerable<T> items, IDbConnection connection);
    }
}
