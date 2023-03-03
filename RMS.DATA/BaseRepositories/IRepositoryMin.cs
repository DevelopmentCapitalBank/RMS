namespace RMS.DATA.BaseRepositories
{
    /// <summary>
    /// минимальный репозиторий для работы с сущностями
    /// </summary>
    /// <typeparam name="T">класс сущности</typeparam>
    internal interface IRepositoryMin<T, V> : IRepositoryCreate<T>, IRepositoryReadOneEntity<T, V>, IRepositoryUpdate<T>, IRepositoryDelete<T>
        where T : class
    { }
}
