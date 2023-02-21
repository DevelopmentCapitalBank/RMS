namespace RMS.DATA.BaseServices
{
    public interface IServiceStandart<T> :
        IServiceCreate<T>, IServiceReadAll<T>, IServiceUpdate<T>, IServiceDelete<T>
        where T : class
    { }
}
