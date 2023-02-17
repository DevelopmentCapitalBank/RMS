using System.Data;

namespace RMS.DATA.BaseRepositories
{
    internal interface IRepositoryReadOneEntity<T, V> where T : class
    {
        Task<T> ReadByIdAsync(V id, IDbConnection connection);
    }
}
