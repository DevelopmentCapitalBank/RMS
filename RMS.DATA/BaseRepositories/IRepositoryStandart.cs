namespace RMS.DATA.BaseRepositories
{
    /// <summary>
    /// стандартный репозиторий для работы с сущностями
    /// </summary>
    /// <typeparam name="T">класс сущности</typeparam>
    internal interface IRepositoryStandart<T> : IRepositoryCreate<T>, IRepositoryCreateList<T>, IRepositoryReadAll<T>, 
        IRepositoryUpdate<T>, IRepositoryDelete<T>, IRepositoryUpdateList<T>
        where T : class
    { }
}
