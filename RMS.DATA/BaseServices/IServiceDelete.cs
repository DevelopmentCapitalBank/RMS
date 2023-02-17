namespace RMS.DATA.BaseServices
{
    public interface IServiceDelete<T> where T : class
    {
        Task DeleteAsync(T entity);
    }
}
