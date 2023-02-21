namespace RMS.DATA.BaseRepositories
{
    internal interface IRepositoryExtended<T, V, F>
        : IRepositoryCreate<T>, IRepositoryUpdate<T>, IRepositoryDelete<T>,
        IRepositoryReadList<T, V>, IRepositoryReadOneEntity<T, V>, IRepositoryFind<T, F>
        where T : class
    { }
}
