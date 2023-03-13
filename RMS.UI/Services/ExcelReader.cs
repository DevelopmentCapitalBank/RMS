using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using RMS.UI.Models;

namespace RMS.UI.Services
{
    public class ExcelReader : IExcelReader
    {

        public DataTable Read2(string path, string nameSheet)
        {
            using var cnn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                               path + @";Extended Properties=""Excel 12.0;HDR=No;IMEX=1""");
            cnn.Open();
            var lines = new List<VisListData>();
            try
            {
                var cmd = new OleDbCommand(string.Format("select * from [{0}$]", nameSheet), cnn);
                using (var dr = cmd.ExecuteReader())
                {
                    if (dr != null)
                    {
                        bool isFirtsRow = true;
                        while (dr.Read())
                        {
                            if (isFirtsRow)
                            {
                                bool isValidList = dr[0].ToString() == "клиент" ||
                                dr[1].ToString() == "счет" ||
                                dr[2].ToString() == "баланс" ||
                                dr[3].ToString() == "вал" ||
                                dr[4].ToString() == "Ключ" ||
                                dr[5].ToString() == "Номер" ||
                                dr[6].ToString() == "название" ||
                                dr[7].ToString() == "Группа" ||
                                dr[8].ToString() == "Привлечение" ||
                                dr[9].ToString() == "Рекомендация" ||
                                dr[10].ToString() == "Офис" ||
                                dr[11].ToString() == "дата открытия" ||
                                dr[12].ToString() == "дата закрытия" ||
                                dr[13].ToString() == "дата последней проводки" ||
                                dr[14].ToString() == "ИНН" ||
                                dr[15].ToString() == "Комментарий";
                                isFirtsRow = false;
                            }
                            else
                            {
                                VisListData vld = new();
                                vld.ClientId = int.Parse(dr[0].ToString());
                                lines.Add(vld);
                            }
                        }

                    }
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                cnn.Close();
            }
            return null ;
        }
        public IEnumerable<VisListData> Read2(string path)
        {
            using FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using SpreadsheetDocument doc = SpreadsheetDocument.Open(fs, false);

            WorkbookPart workbookPart = doc.WorkbookPart;
            WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();

            OpenXmlReader reader = OpenXmlReader.Create(worksheetPart);
            List<VisListData> retults = new List<VisListData>();
            string text;
            while (reader.Read())
            {
                if (reader.ElementType == typeof(CellValue))
                {
                    text = reader.GetText();
                    Debug.Print(text + " ");
                }
            }

            //var sstpart = workbookPart.GetPartsOfType<SharedStringTablePart>().First();
            //var sst = sstpart.SharedStringTable;
            //var worksheetPart = workbookPart.WorksheetParts.First();
            //var sheet = worksheetPart.Worksheet;
            //var rows = sheet.Descendants<Row>();
            //List<VisListData> retults = new List<VisListData>();

            //foreach (var row in rows)
            //{
            //    if (row.RowIndex.Value != 1)
            //    {
            //        retults.Add(FillVisListData(sst, row));
            //    }
            //    else
            //    {
            //        if (!IsValidVisList(sst, row))
            //        {
            //            throw new Exception("Данный документ не является ВизЛистом!\nВозможно были переименованы/перенесены столбцы.");
            //        };
            //    }
            //}

            return retults;
        }
        private static VisListData FillVisListData(SharedStringTable sst, Row row)
        {
            VisListData vld = new();

            string? str = GetValue(sst, row.Descendants<Cell>().ElementAt(0));
            if (str != null)
            {
                vld.ClientId = int.Parse(str);
            }
            str = GetValue(sst, row.Descendants<Cell>().ElementAt(1));
            if (str != null)
            {
                vld.AccountNumber = str;
            }
            str = GetValue(sst, row.Descendants<Cell>().ElementAt(6));
            if (str != null)
            {
                vld.CompanyName = str;
            }
            str = GetValue(sst, row.Descendants<Cell>().ElementAt(7));
            if (str != null)
            {
                vld.GroupName = str;
            }
            str = GetValue(sst, row.Descendants<Cell>().ElementAt(8));
            vld.IsAttraction = str != null;
            str = GetValue(sst, row.Descendants<Cell>().ElementAt(9));
            vld.ManagerName = str;
            str = GetValue(sst, row.Descendants<Cell>().ElementAt(10));
            vld.OfficeName = str;
            str = GetValue(sst, row.Descendants<Cell>().ElementAt(11));
            if (str != null)
            {
                vld.DateOpen = TryParseExcelDateTime(str);
            }
            str = GetValue(sst, row.Descendants<Cell>().ElementAt(12));
            if (str != null)
            {
                if (string.Compare(str, "...") != 0)
                {
                    vld.DateClose = TryParseExcelDateTime(str);
                }
            }
            str = GetValue(sst, row.Descendants<Cell>().ElementAt(13));
            if (str != null)
            {
                if (string.Compare(str, "...") != 0)
                {
                    vld.DateTimeLastOperation = TryParseExcelDateTime(str);
                }
            }
            str = GetValue(sst, row.Descendants<Cell>().ElementAt(14));
            if (str != null)
            {
                vld.Inn = str;
            }
            str = GetValue(sst, row.Descendants<Cell>().ElementAt(15));
            vld.Comment = str;

            return vld;
        }

        private static bool IsValidVisList(SharedStringTable sst, Row row)
        {
            return GetValue(sst, row.Descendants<Cell>().ElementAt(0)) == "клиент" ||
                GetValue(sst, row.Descendants<Cell>().ElementAt(1)) == "счет" ||
                GetValue(sst, row.Descendants<Cell>().ElementAt(2)) == "баланс" ||
                GetValue(sst, row.Descendants<Cell>().ElementAt(3)) == "вал" ||
                GetValue(sst, row.Descendants<Cell>().ElementAt(4)) == "Ключ" ||
                GetValue(sst, row.Descendants<Cell>().ElementAt(5)) == "Номер" ||
                GetValue(sst, row.Descendants<Cell>().ElementAt(6)) == "название" ||
                GetValue(sst, row.Descendants<Cell>().ElementAt(7)) == "Группа" ||
                GetValue(sst, row.Descendants<Cell>().ElementAt(8)) == "Привлечение" ||
                GetValue(sst, row.Descendants<Cell>().ElementAt(9)) == "Рекомендация" ||
                GetValue(sst, row.Descendants<Cell>().ElementAt(10)) == "Офис" ||
                GetValue(sst, row.Descendants<Cell>().ElementAt(11)) == "дата открытия" ||
                GetValue(sst, row.Descendants<Cell>().ElementAt(12)) == "дата закрытия" ||
                GetValue(sst, row.Descendants<Cell>().ElementAt(13)) == "дата последней проводки" ||
                GetValue(sst, row.Descendants<Cell>().ElementAt(14)) == "ИНН" ||
                GetValue(sst, row.Descendants<Cell>().ElementAt(15)) == "Комментарий";
        }

        private static DateTime? TryParseExcelDateTime(string excelDateTimeAsString)
        {
            try
            {
                if (!double.TryParse(excelDateTimeAsString, NumberStyles.Any, CultureInfo.InvariantCulture, out double oaDateAsDouble))
                {
                    return null;
                }

                return DateTime.FromOADate(oaDateAsDouble);
            }
            catch
            {
                return null;
            }
        }

        private static string? GetValue(SharedStringTable sst, Cell cell)
        {
            if (cell.CellValue == null)
            {
                return null;
            }

            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return sst.ChildElements[int.Parse(cell.CellValue.Text)].InnerText;
            }
            else
            {
                return cell.CellValue.InnerText;
            }
        }

        public DataTable Read(string sheet, string path)
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
                DataTable schemaTable = new();
                connExcel.Open();
                var MyCommand = new OleDbDataAdapter("Select * from [" + sheet + "$]", connExcel);
                var dt = connExcel.GetSchema("Tables"); ;
                MyCommand.TableMappings.Add("Table", "TestTable");
                var DtSet = new DataSet();
                MyCommand.Fill(DtSet);

                connExcel.Close();
                return DtSet.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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
    }
}
