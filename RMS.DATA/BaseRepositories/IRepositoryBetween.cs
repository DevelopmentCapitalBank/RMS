using System.Data;

namespace RMS.DATA.BaseRepositories
{
    internal interface IRepositoryBetween<T, V> where T : class
    {
        Task<IEnumerable<T>> ReadBetweenAsync(V v1, V v2, IDbConnection connection);
    }
}
