namespace RMS.DATA.BaseServices
{
    public interface IServiceCreateList<T> where T : class
    {
        Task<IEnumerable<T>> CreateListOfEntitiesAsync(IEnumerable<T> list);
    }
}
