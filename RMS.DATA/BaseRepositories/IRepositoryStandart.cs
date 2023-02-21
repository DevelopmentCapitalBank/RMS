namespace RMS.DATA.BaseRepositories
{
    internal interface IRepositoryStandart<T> : IRepositoryCreate<T>, IRepositoryReadAll<T>, IRepositoryUpdate<T>, IRepositoryDelete<T>
        where T : class
    { }
}
