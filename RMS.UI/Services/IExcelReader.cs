using System.Data;

namespace RMS.UI.Services
{
    public interface IExcelReader
    {
        DataTable Read(string sheet, string path);
        string[] GetSheets(string path);
    }
}
