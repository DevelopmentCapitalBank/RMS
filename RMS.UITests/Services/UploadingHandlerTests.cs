using Microsoft.VisualStudio.TestTools.UnitTesting;
using RMS.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace RMS.UI.Services.Tests
{
    [TestClass()]
    public class UploadingHandlerTests
    {
        [TestMethod()]
        public void GetConversions__return_empty()
        {
            var dt = new DataTable();
            dt.Columns.Add(new DataColumn("Номер", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Дата", Type.GetType("System.DateTime")));
            dt.Columns.Add(new DataColumn("Вид сделки", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Cтатус", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Контрагент", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Банк получает сумму", Type.GetType("System.Double")));
            dt.Columns.Add(new DataColumn("Вал", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Дата зачисления", Type.GetType("System.DateTime")));
            dt.Columns.Add(new DataColumn("Банк отдает сумму", Type.GetType("System.Double")));
            dt.Columns.Add(new DataColumn("Вал1", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Дата списания", Type.GetType("System.DateTime")));
            dt.Columns.Add(new DataColumn("Курс остатка", Type.GetType("System.Double")));
            dt.Columns.Add(new DataColumn("Док", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Сальдо переоценки", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Состояние документов", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Курс ЦБ (Валюта зачисления)", Type.GetType("System.Double")));
            dt.Columns.Add(new DataColumn("Курс ЦБ (Валюта списания)", Type.GetType("System.Double")));
            dt.Columns.Add(new DataColumn("Банк получает на счет", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Банк отдает со счета", Type.GetType("System.String")));

            UploadingHandler handler = new();

            var actual = (List<Conversion>)handler.GetConversions(dt, new DateTime(2022, 1, 1));

            Assert.AreEqual(0, actual.Count);
        }
        [TestMethod()]
        public void GetConversions__return_item()
        {
            var dt = new DataTable();
            dt.Columns.Add(new DataColumn("Номер", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Дата", Type.GetType("System.DateTime")));
            dt.Columns.Add(new DataColumn("Вид сделки", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Cтатус", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Контрагент", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Банк получает сумму", Type.GetType("System.Double")));
            dt.Columns.Add(new DataColumn("Вал", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Дата зачисления", Type.GetType("System.DateTime")));
            dt.Columns.Add(new DataColumn("Банк отдает сумму", Type.GetType("System.Double")));
            dt.Columns.Add(new DataColumn("Вал1", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Дата списания", Type.GetType("System.DateTime")));
            dt.Columns.Add(new DataColumn("Курс остатка", Type.GetType("System.Double")));
            dt.Columns.Add(new DataColumn("Док", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Сальдо переоценки", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Состояние документов", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Курс ЦБ (Валюта зачисления)", Type.GetType("System.Double")));
            dt.Columns.Add(new DataColumn("Курс ЦБ (Валюта списания)", Type.GetType("System.Double")));
            dt.Columns.Add(new DataColumn("Банк получает на счет", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Банк отдает со счета", Type.GetType("System.String")));

            dt.Rows.Add(new object[] { "1",  new DateTime(2023, 2, 22) , "ЮЛ. Покупка валюты по курсу Банка", "Закрыт",  "\"МегаМет\"ООО",
                18060776.82, "RUB", new DateTime(2023, 2, 22), 224218.21, "EUR",
                new DateTime(2023, 2, 22), 0, "{4}", DBNull.Value, "Проведены", 
                1, 79.7588, "40702810400010010973", "40702978300010010973" });

            UploadingHandler handler = new();

            var actual = (List<Conversion>)handler.GetConversions(dt, new DateTime(2022, 1, 1));

            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(new DateTime(2023, 2, 22), actual[0].DateOperation);
            Assert.AreEqual("ЮЛ. Покупка валюты по курсу Банка", actual[0].TypeOfTransaction);
            Assert.AreEqual(18060776.82M, actual[0].ReceivesAmount, 0.001M);
            Assert.AreEqual("RUB", actual[0].ReceivedCurrency);
            Assert.AreEqual(224218.21M, actual[0].GivesAmount, 0.001M);
            Assert.AreEqual("EUR", actual[0].GivesCurrency);
            Assert.AreEqual(1M, actual[0].RateCurrencyOfCrediting, 0.0001M);
            Assert.AreEqual(79.7588M, actual[0].RateCurrencyOfDebiting, 0.0001M);
            Assert.AreEqual("40702810400010010973", actual[0].ReceivesToAccount);
            Assert.AreEqual("40702978300010010973", actual[0].GivesFromAccount);
        }
    }
}