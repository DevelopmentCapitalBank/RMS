namespace RMS.DATA.BaseRepositories
{
    /// <summary>
    /// стандартный репозиторий для работы с сущностями
    /// </summary>
    /// <typeparam name="T">класс сущности</typeparam>
    internal interface IRepositoryStandart<T> : IRepositoryCreate<T>, IRepositoryReadAll<T>, IRepositoryUpdate<T>, IRepositoryDelete<T>
        where T : class
    { }
}
