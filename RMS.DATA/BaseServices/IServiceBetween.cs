namespace RMS.DATA.BaseServices
{
    public interface IServiceBetweenBetween<T, V> where T : class
    {
        Task<IEnumerable<T>> ReadBetweenAsync(V v1, V v2);
    }
}
