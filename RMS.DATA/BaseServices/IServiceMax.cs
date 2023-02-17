namespace RMS.DATA.BaseServices
{
    public interface IServiceMax<T, V> : IServiceMin<T, V>, IServiceReadAll<T>, IServiceReadList<T, V>
        where T : class
    { }
}
