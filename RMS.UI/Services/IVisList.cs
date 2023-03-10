using System.Data;

namespace RMS.UI.Services
{
    public interface IVisList
    {
        DataTable Read(string sheet, string path);
        string[] GetSheets(string path);
    }
}
