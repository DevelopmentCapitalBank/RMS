using System.Data;

namespace RMS.DATA.BaseRepositories
{
    internal interface IRepositoryReadAll<T> where T : class
    {
        Task<IEnumerable<T>> ReadAllAsync(IDbConnection connection);
    }
}
