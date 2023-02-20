using System.Data;

namespace RMS.DATA.BaseRepositories
{
    internal interface IRepositoryFind<T, F> where T : class
    {
        Task<IEnumerable<T>> FindAsync(F f, IDbConnection connection);
    }
}
