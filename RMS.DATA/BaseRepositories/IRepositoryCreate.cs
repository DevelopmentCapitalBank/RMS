using System.Data;

namespace RMS.DATA.BaseRepositories
{
    internal interface IRepositoryCreate<T> where T : class
    {
        Task<T> CreateAsync(T entity, IDbConnection connection);
    }
}
