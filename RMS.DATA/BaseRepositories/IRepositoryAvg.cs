namespace RMS.DATA.BaseRepositories
{
    internal interface IRepositoryAvg<T, V> : IRepositoryMin<T, V>, IRepositoryReadAll<T> 
        where T : class
    { }
}
