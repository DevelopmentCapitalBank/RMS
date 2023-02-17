namespace RMS.DATA.BaseServices
{
    public interface IServiceAvg<T, V> : IServiceMin<T, V>, IServiceReadAll<T> 
        where T : class
    { }
}
