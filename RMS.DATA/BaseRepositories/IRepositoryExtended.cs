namespace RMS.DATA.BaseRepositories
{
    /// <summary>
    /// расширенный репозиторий для работы с сущностями
    /// </summary>
    /// <typeparam name="T">класс сущности</typeparam>
    /// <typeparam name="V">идентификатор для условия отбора</typeparam>
    /// <typeparam name="F">данные для поиска</typeparam>
    internal interface IRepositoryExtended<T, V, F>
        : IRepositoryCreate<T>, IRepositoryUpdate<T>, IRepositoryDelete<T>,
        IRepositoryReadList<T, V>, IRepositoryReadOneEntity<T, V>, IRepositoryFind<T, F>, IRepositoryReadAll<T>
        where T : class
    { }
}
