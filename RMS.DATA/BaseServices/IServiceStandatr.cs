namespace RMS.DATA.BaseServices
{
    public interface IServiceStandatr<T> :
        IServiceCreate<T>, IServiceReadAll<T>, IServiceUpdate<T>, IServiceDelete<T>
        where T : class
    { }
}
