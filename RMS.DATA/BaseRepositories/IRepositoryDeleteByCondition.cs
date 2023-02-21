using System.Data;

namespace RMS.DATA.BaseRepositories
{
    internal interface IRepositoryDeleteByCondition<C>
    {
        Task DeleteByConditionAsync(C condition, IDbConnection connection);
    }
}
