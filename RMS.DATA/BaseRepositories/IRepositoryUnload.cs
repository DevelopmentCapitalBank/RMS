namespace RMS.DATA.BaseRepositories
{
    /// <summary>
    /// репозитрорий для работы с выгрузками из цфт
    /// </summary>
    /// <typeparam name="T">Сущность</typeparam>
    /// <typeparam name="V">Условие отбора</typeparam>
    /// <typeparam name="C">Условие для удаления</typeparam>
    internal interface IRepositoryUnload<T, V, C> : IRepositoryBetween<T, V>, IRepositoryDeleteByCondition<C>, IRepositoryCreateList<T>
        where T : class
    { }
}
