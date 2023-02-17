namespace RMS.DATA.BaseServices
{
    public interface IServiceReadOneEntity<T, V> where T : class
    {
        Task<T> ReadByIdAsync(V id);
    }
}
