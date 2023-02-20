namespace RMS.DATA.BaseServices
{
    public interface IServiceFind<T, F> where T : class
    {
        Task<IEnumerable<T>> FindAsync(F f);
    }
}
