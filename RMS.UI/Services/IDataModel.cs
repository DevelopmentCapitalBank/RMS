namespace RMS.UI.Services
{
    public interface IDataModel
    {
        string Data { get; set; }
        string? Reverse();
    }
}
