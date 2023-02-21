namespace RMS.DATA.BaseRepositories
{
    internal interface IRepositoryStandard<T> : IRepositoryCreate<T>, IRepositoryReadAll<T>, IRepositoryUpdate<T>, IRepositoryDelete<T>
        where T : class
    { }
}
