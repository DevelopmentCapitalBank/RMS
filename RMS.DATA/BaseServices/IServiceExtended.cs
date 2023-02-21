namespace RMS.DATA.BaseServices
{
    public interface IServiceExtended<T, V, F> :
        IServiceCreate<T>, IServiceUpdate<T>, IServiceDelete<T>,
        IServiceReadList<T, V>, IServiceReadOneEntity<T, V>, IServiceFind<T, F>
        where T : class
    { }
}
