using System.Data;

namespace RMS.DATA.BaseRepositories
{
    internal interface IRepositoryReadList<T, V> where T : class
    {
        Task<IEnumerable<T>> ReadListByIdAsync(V id, IDbConnection connection);
    }
}
