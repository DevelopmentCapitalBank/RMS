namespace RMS.DATA.BaseServices
{
    public interface IServiceReadList<T, V> where T : class
    {
        Task<IEnumerable<T>> ReadListByIdAsync(V id);
    }
}
