namespace RMS.DATA.BaseServices
{
    public interface IServiceExtended<T, V, F> :
        IServiceCreate<T>, IServiceCreateList<T>, IServiceUpdate<T>, IServiceDelete<T>,
        IServiceReadList<T, V>, IServiceReadOneEntity<T, V>, IServiceFind<T, F>, IServiceReadAll<T>,
        IServiceUpdateList<T>
        where T : class
    { }
}
