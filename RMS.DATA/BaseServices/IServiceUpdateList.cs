namespace RMS.DATA.BaseServices
{
    public interface IServiceUpdateList<T> where T : class
    {
        Task UpdateListOfEntitiesAsync(IEnumerable<T> items);
    }
}
