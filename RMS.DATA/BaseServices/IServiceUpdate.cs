namespace RMS.DATA.BaseServices
{
    public interface IServiceUpdate<T> where T : class
    {
        Task UpdateAsync(T entity);
    }
}
