using System.Data;

namespace RMS.DATA.BaseRepositories
{
    internal interface IRepositoryDelete<T> where T : class
    {
        Task DeleteAsync(T entity, IDbConnection connection);
    }
}
