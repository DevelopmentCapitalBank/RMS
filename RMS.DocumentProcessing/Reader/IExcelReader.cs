using System.Data;

namespace RMS.DocumentProcessing.Reader
{
    public interface IExcelReader
    {
        DataTable Read(string sheet, string path);
        string[] GetSheets(string path);
    }
}
