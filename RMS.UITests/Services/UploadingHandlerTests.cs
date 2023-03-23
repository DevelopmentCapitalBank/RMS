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

        [TestMethod()]
        public void GetOperations__return_empty()
        {
            var dt = new DataTable();
            dt.Columns.Add(new DataColumn("Дата", Type.GetType("System.DateTime")));
            dt.Columns.Add(new DataColumn("Документ", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Номер", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Сумма", Type.GetType("System.Double")));
            dt.Columns.Add(new DataColumn("Сумма эквивалента", Type.GetType("System.Double")));
            dt.Columns.Add(new DataColumn("Вал", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Счет Дебет", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Счет Кредит", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Состояние", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Дата проводки", Type.GetType("System.DateTime")));
            dt.Columns.Add(new DataColumn("Назначение платежа", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("БИК Банка корресп.", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Дата создания", Type.GetType("System.DateTime")));
            dt.Columns.Add(new DataColumn("Счет плательщика", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Счет получателя", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Плательщик", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Получатель", Type.GetType("System.String")));

            UploadingHandler handler = new();

            var actual = (List<Operation>)handler.GetOperations(dt, new DateTime(2022, 1, 1));

            Assert.AreEqual(0, actual.Count);
        }
        [TestMethod()]
        public void GetOperations__condition_not_suitable__return_empty()
        {
            var dt = new DataTable();
            dt.Columns.Add(new DataColumn("Дата", Type.GetType("System.DateTime")));
            dt.Columns.Add(new DataColumn("Документ", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Номер", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Сумма", Type.GetType("System.Double")));
            dt.Columns.Add(new DataColumn("Сумма эквивалента", Type.GetType("System.Double")));
            dt.Columns.Add(new DataColumn("Вал", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Счет Дебет", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Счет Кредит", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Состояние", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Дата проводки", Type.GetType("System.DateTime")));
            dt.Columns.Add(new DataColumn("Назначение платежа", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("БИК Банка корресп.", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Дата создания", Type.GetType("System.DateTime")));
            dt.Columns.Add(new DataColumn("Счет плательщика", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Счет получателя", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Плательщик", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Получатель", Type.GetType("System.String")));

            dt.Rows.Add(new object[] { new DateTime(2022, 2, 28), "Банк.ор.", "25661", 500.00, DBNull.Value, 
                "RUB", "40802810300010004750", "45814810300010001249", "Оплачен", new DateTime(2023, 2, 13), 
                "Комиссия за ведение счета согласно договора РКО N 750 - р за период с '01/02/2022' по '28/02/2022'.НДС не облагается", DBNull.Value,
                new DateTime(2022, 2, 28), "40802810300010004750", "45814810300010001249", "Какулин Дмитрий Александрович Индивидуальный предприниматель" , "АО Банк \"Развитие-Столица\"" });

            UploadingHandler handler = new();

            var actual = (List<Operation>)handler.GetOperations(dt, new DateTime(2022, 1, 1));

            Assert.AreEqual(0, actual.Count);
        }
        [TestMethod()]
        public void GetOperations__return_item()
        {
            var dt = new DataTable();
            dt.Columns.Add(new DataColumn("Дата", Type.GetType("System.DateTime")));
            dt.Columns.Add(new DataColumn("Документ", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Номер", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Сумма", Type.GetType("System.Double")));
            dt.Columns.Add(new DataColumn("Сумма эквивалента", Type.GetType("System.Double")));
            dt.Columns.Add(new DataColumn("Вал", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Счет Дебет", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Счет Кредит", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Состояние", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Дата проводки", Type.GetType("System.DateTime")));
            dt.Columns.Add(new DataColumn("Назначение платежа", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("БИК Банка корресп.", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Дата создания", Type.GetType("System.DateTime")));
            dt.Columns.Add(new DataColumn("Счет плательщика", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Счет получателя", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Плательщик", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Получатель", Type.GetType("System.String")));

            dt.Rows.Add(new object[] { new DateTime(2022, 2, 28), "Банк.ор.", "25661", 500.00, DBNull.Value,
                "RUB", "40802810300010004750", "45814810300010000259", "Проведен", new DateTime(2023, 2, 13),
                "Комиссия за ведение счета согласно договора РКО N 750-р за период с '01/03/2022' по '31/03/2022'. НДС не облагается", DBNull.Value,
                new DateTime(2022, 2, 28), "40802810300010004750", "45814810300010000259", "Какулин Дмитрий Александрович Индивидуальный предприниматель" , "АО Банк \"Развитие-Столица\"" });

            UploadingHandler handler = new();

            var actual = (List<Operation>)handler.GetOperations(dt, new DateTime(2022, 1, 1));

            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(500M, actual[0].Amount, 0.0001M);
            Assert.AreEqual(0M, actual[0].AmountEquivalent, 0.0001M);
            Assert.AreEqual(new DateTime(2023, 2, 13), actual[0].DateOperation);
            Assert.AreEqual("Комиссия за ведение счета согласно договора РКО N 750-р за период с '01/03/2022' по '31/03/2022'. НДС не облагается", actual[0].Purpose);
            Assert.AreEqual("40802810300010004750", actual[0].Payer);
            Assert.AreEqual("45814810300010000259", actual[0].Recipient);
        }

        [TestMethod()]
        public void GetRemaings__return_empty()
        {
            var dt = new DataTable();
            dt.Columns.Add(new DataColumn("№ счета", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Валюта", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Клиент", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Входящее сальдо", Type.GetType("System.Double")));
            dt.Columns.Add(new DataColumn("Оборот Дебет", Type.GetType("System.Double")));
            dt.Columns.Add(new DataColumn("Оборот Кредит", Type.GetType("System.Double")));
            dt.Columns.Add(new DataColumn("Исходящее сальдо", Type.GetType("System.Double")));
            dt.Columns.Add(new DataColumn("Средний остаток", Type.GetType("System.Double")));
            dt.Columns.Add(new DataColumn("Средний остаток НП", Type.GetType("System.Double")));
            dt.Columns.Add(new DataColumn("Подразделение", Type.GetType("System.String")));

            UploadingHandler handler = new();

            var actual = (List<Remains>)handler.GetRemains(dt, new DateTime(2022, 1, 1));

            Assert.AreEqual(0, actual.Count);
        }
        [TestMethod()]
        public void GetRemains__return_empty()
        {
            var dt = new DataTable();
            dt.Columns.Add(new DataColumn("№ счета", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Валюта", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Клиент", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Входящее сальдо", Type.GetType("System.Double")));
            dt.Columns.Add(new DataColumn("Оборот Дебет", Type.GetType("System.Double")));
            dt.Columns.Add(new DataColumn("Оборот Кредит", Type.GetType("System.Double")));
            dt.Columns.Add(new DataColumn("Исходящее сальдо", Type.GetType("System.Double")));
            dt.Columns.Add(new DataColumn("Средний остаток", Type.GetType("System.Double")));
            dt.Columns.Add(new DataColumn("Средний остаток НП", Type.GetType("System.Double")));
            dt.Columns.Add(new DataColumn("Подразделение", Type.GetType("System.String")));

            dt.Rows.Add(new object[] { "40702810400010010973", "RUB", "\"МегаМет\"ООО",
                15000.31, 12030.22, 125001.23, 14289.19, 10020.43, 10020.43, "000-1" });

            UploadingHandler handler = new();

            var actual = (List<Remains>)handler.GetRemains(dt, new DateTime(2022, 1, 1));

            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(new DateTime(2022, 1, 1), actual[0].DateOfUnloading);
            Assert.AreEqual("40702810400010010973", actual[0].Account);
            Assert.AreEqual(12030.22M, actual[0].Debit, 0.0001M);
            Assert.AreEqual(125001.23M, actual[0].Credit, 0.0001M);
            Assert.AreEqual(10020.43M, actual[0].AverageBalance, 0.0001M);
        }
    }
}