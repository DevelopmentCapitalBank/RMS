namespace RMS.DATA.BaseServices
{
    public interface IServiceCount<V>
    {
        Task<int> GetCountAsync(V value);
    }
}
