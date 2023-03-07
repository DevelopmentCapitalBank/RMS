using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using RMS.UI.Models;

namespace RMS.UI.Services
{
    public class VisList : IVisList
    {
        public IEnumerable<VisListData> Read(string path)
        {
            using FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using SpreadsheetDocument doc = SpreadsheetDocument.Open(fs, false);
            
            var workbookPart = doc.WorkbookPart; 
            var sstpart = workbookPart.GetPartsOfType<SharedStringTablePart>().First();
            var sst = sstpart.SharedStringTable;
            var worksheetPart = workbookPart.WorksheetParts.First();
            var sheet = worksheetPart.Worksheet;
            var rows = sheet.Descendants<Row>();
            List<VisListData> retults = new List<VisListData>();

            foreach (var row in rows)
            {
                if (row.RowIndex.Value != 1)
                {
                    retults.Add(FillVisListData(sst, row));
                }
                else
                {
                    if (!IsValidVisList(sst, row))
                    {
                        throw new Exception("Данный документ не является ВизЛистом!\nВозможно были переименованы/перенесены столбцы.");
                    };
                }
            }

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
    }
}
