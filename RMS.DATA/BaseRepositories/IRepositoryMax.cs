namespace RMS.DATA.BaseRepositories
{
    internal interface IRepositoryMax<T, V> : IRepositoryMin<T, V>, IRepositoryReadAll<T>, IRepositoryReadList<T, V>
        where T : class
    { }
}
