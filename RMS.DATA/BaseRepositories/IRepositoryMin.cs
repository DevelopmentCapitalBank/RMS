namespace RMS.DATA.BaseRepositories
{
    internal interface IRepositoryMin<T, V>
        : IRepositoryCreate<T>, IRepositoryReadOneEntity<T, V>, IRepositoryUpdate<T>, IRepositoryDelete<T>
        where T : class
    { }
}
