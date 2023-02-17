namespace RMS.DATA.BaseServices
{
    public interface IServiceReadAll<T> where T : class
    {
        Task<IEnumerable<T>> ReadAllAsync();
    }
}
