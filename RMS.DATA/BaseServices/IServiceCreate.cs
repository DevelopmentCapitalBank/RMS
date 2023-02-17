namespace RMS.DATA.BaseServices
{
    public interface IServiceCreate<T> where T : class
    {
        Task<T> CreateAsync(T entity);
    }
}
