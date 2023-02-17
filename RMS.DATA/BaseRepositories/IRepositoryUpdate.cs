using System.Data;

namespace RMS.DATA.BaseRepositories
{
    internal interface IRepositoryUpdate<T> where T : class
    {
        Task UpdateAsync(T entity, IDbConnection connection);
    }
}
