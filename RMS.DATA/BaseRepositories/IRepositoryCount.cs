using System.Data;

namespace RMS.DATA.BaseRepositories
{
    /// <summary>
    /// репозиторий для запроса кол-ва записей
    /// </summary>
    /// <typeparam name="V">условие для отбора</typeparam>
    internal interface IRepositoryCount<V>
    {
        Task<int> GetCountAsync(V value, IDbConnection connection);
    }
}
