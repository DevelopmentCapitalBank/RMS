namespace RMS.DATA.BaseServices
{
    /// <summary>
    /// сервис для работы с выгрузками из цфт
    /// </summary>
    /// <typeparam name="T">Сущность</typeparam>
    /// <typeparam name="V">Условие отбора</typeparam>
    /// <typeparam name="C">Условие для удаления</typeparam>
    public interface IServiceUnload<T, V, C> : IServiceBetween<T, V>, IServiceDeleteByCondition<C>, IServiceCreateList<T>
        where T : class
    {  }
}
