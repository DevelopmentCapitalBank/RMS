namespace RMS.DATA.BaseServices
{
    public interface IServiceMin<T, V> :
        IServiceCreate<T>, IServiceReadOneEntity<T, V>, IServiceUpdate<T>, IServiceDelete<T>
        where T : class
    { }
}
