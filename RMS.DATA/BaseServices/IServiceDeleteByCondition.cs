namespace RMS.DATA.BaseServices
{
    public interface IServiceDeleteByCondition<C>
    {
        Task DeleteByConditionAsync(C condition);
    }
}
