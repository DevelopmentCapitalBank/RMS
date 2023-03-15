namespace RMS.DATA.BaseServices
{
    public interface IServiceStandart<T> :
        IServiceCreate<T>, IServiceCreateList<T>, IServiceReadAll<T>, IServiceUpdate<T>, IServiceDelete<T>, IServiceUpdateList<T>
        where T : class
    { }
}
