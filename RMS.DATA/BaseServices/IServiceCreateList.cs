namespace RMS.DATA.BaseServices
{
    public interface IServiceCreateList<T> where T : class
    {
        Task CreateListOfEntitiesAsync(IEnumerable<T> list);
    }
}
