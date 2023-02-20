namespace RMS.DATA.BaseServices
{
    public interface IServiceBetween<T, V> where T : class
    {
        Task<IEnumerable<T>> ReadBetweenAsync(V v1, V v2);
    }
}
