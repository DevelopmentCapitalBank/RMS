using System.Data;
using System.Data.OleDb;

namespace RMS.DocumentProcessing.Reader
{
    public class ExcelReader : IExcelReader
    {
        public string[] GetSheets(string path)
        {
            string Extension = Path.GetExtension(path);
            string conStr = "";
            switch (Extension)
            {
                case ".xls":
                    conStr = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=No;IMEX=1""";
                    break;
                case ".xlsx":
                    conStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0;HDR=No;IMEX=1""";
                    break;
            }
            conStr = string.Format(conStr, path);
            try
            {
                using OleDbConnection connExcel = new(conStr);
                connExcel.Open();

                var dt = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (dt == null)
                {
                    return null;
                }

                string[] excelSheets = new string[dt.Rows.Count];
                int i = 0;

                foreach (DataRow row in dt.Rows)
                {
                    excelSheets[i] = row["TABLE_NAME"].ToString().Replace("$", "");
                    i++;
                }

                return excelSheets;
            }
            catch (Exception ex)
            {
                string[] errror = new string[] { "Error", ex.Message };
                return errror;
            }
        }

        public DataTable Read(string sheet, string path)
        {
            string Extension = Path.GetExtension(path);
            string conStr = "";
            switch (Extension)
            {
                case ".xls":
                    conStr = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""";
                    break;
                case ".xlsx":
                    conStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0;HDR=Yes;IMEX=1""";
                    break;
            }
            conStr = string.Format(conStr, path);
            try
            {
                using OleDbConnection connExcel = new(conStr);
                DataTable schemaTable = new();
                connExcel.Open();
                var MyCommand = new OleDbDataAdapter("Select * from [" + sheet + "$]", connExcel);
                var dt = connExcel.GetSchema("Tables"); ;
                MyCommand.TableMappings.Add("Table", "TestTable");
                var DtSet = new DataSet();
                MyCommand.Fill(DtSet);

                connExcel.Close();
                throw new Exception("ёбушки воробушки");
                return DtSet.Tables[0];
            }
            catch (Exception ex)
            {
                var errDataTable = new DataTable("Error");
                var errColumn = new DataColumn("Name", Type.GetType("System.String"));
                errDataTable.Columns.Add(errColumn);
                errDataTable.Rows.Add(new object[] { ex.Message });
                return errDataTable;
            }
        }
    }
}
