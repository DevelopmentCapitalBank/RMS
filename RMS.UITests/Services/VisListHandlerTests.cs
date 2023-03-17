using System;
using System.Collections.Generic;
using System.Data;
using RMS.DATA.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RMS.UI.Services.Tests
{
    [TestClass()]
    public class VisListHandlerTests
    {
        [TestMethod()]
        public void GetNewItemsGroup__datatable_empty__allItems_empty__return_empty()
        {
            DataTable dt = new();
            dt.Columns.Add(new DataColumn("name", Type.GetType("System.String")));

            var allItems = new List<Group>();
            VisListHandler handler = new();
            
            var actual = (List<Group>)handler.GetNewItems(dt, allItems);

            Assert.AreEqual(0, actual.Count);
        }
        [TestMethod()]
        public void GetNewItemsGroup__datatable_empty__allItems_oneItem__return_empty()
        {
            DataTable dt = new();
            dt.Columns.Add(new DataColumn("name", Type.GetType("System.String")));
            dt.Rows.Add(new object[] { "" });

            var allItems = new List<Group>();
            VisListHandler handler = new();

            var actual = (List<Group>)handler.GetNewItems(dt, allItems);

            Assert.AreEqual(0, actual.Count);
        }
        [TestMethod]
        public void GetNewItemsGroup__datatable_2items__allItems_empty__return_2()
        {
            DataTable dt = new();
            dt.Columns.Add(new DataColumn("name", Type.GetType("System.String")));
            dt.Rows.Add(new object[] { "B" });
            dt.Rows.Add(new object[] { "N" });

            var allItems = new List<Group>();
            VisListHandler handler = new();

            var actual = (List<Group>)handler.GetNewItems(dt, allItems);

            Assert.AreEqual(2, actual.Count);
        }
        [TestMethod]
        public void GetNewItemsGroup__datatable_2items__allItems_1item__retun1()
        {
            DataTable dt = new();
            dt.Columns.Add(new DataColumn("name", Type.GetType("System.String")));
            dt.Rows.Add(new object[] { "B" });
            dt.Rows.Add(new object[] { "N" });

            var allItems = new List<Group> {
                new Group { Name = "B" }
            };
            VisListHandler handler = new();

            var actual = (List<Group>)handler.GetNewItems(dt, allItems);

            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual("N", actual[0].Name);
        }


        [TestMethod()]
        public void GetNewItemsOffice__datatable_empty__allItems_empty__return_empty()
        {
            DataTable dt = new();
            dt.Columns.Add(new DataColumn("name", Type.GetType("System.String")));

            var allItems = new List<Office>();
            VisListHandler handler = new();

            var actual = (List<Office>)handler.GetNewItems(dt, allItems);

            Assert.AreEqual(0, actual.Count);
        }
        [TestMethod()]
        public void GetNewItemsOffice__datatable_empty__allItems_oneItem__return_empty()
        {
            DataTable dt = new();
            dt.Columns.Add(new DataColumn("name", Type.GetType("System.String")));
            dt.Rows.Add(new object[] { "" });

            var allItems = new List<Office>();
            VisListHandler handler = new();

            var actual = (List<Office>)handler.GetNewItems(dt, allItems);

            Assert.AreEqual(0, actual.Count);
        }
        [TestMethod]
        public void GetNewItemsOffice__datatable_2items__allItems_empty__return_2()
        {
            DataTable dt = new();
            dt.Columns.Add(new DataColumn("name", Type.GetType("System.String")));
            dt.Rows.Add(new object[] { "B" });
            dt.Rows.Add(new object[] { "N" });

            var allItems = new List<Office>();
            VisListHandler handler = new();

            var actual = (List<Office>)handler.GetNewItems(dt, allItems);

            Assert.AreEqual(2, actual.Count);
        }
        [TestMethod]
        public void GetNewItemsOffice__datatable_2items__allItems_1item__retun1()
        {
            DataTable dt = new();
            dt.Columns.Add(new DataColumn("name", Type.GetType("System.String")));
            dt.Rows.Add(new object[] { "B" });
            dt.Rows.Add(new object[] { "N" });

            var allItems = new List<Office> {
                new Office { Name = "B" }
            };
            VisListHandler handler = new();

            var actual = (List<Office>)handler.GetNewItems(dt, allItems);

            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual("N", actual[0].Name);
        }


        [TestMethod()]
        public void GetNewItemsManager__datatable_empty__allItems_empty__return_empty()
        {
            DataTable dt = new();
            dt.Columns.Add(new DataColumn("name", Type.GetType("System.String")));

            var allItems = new List<Manager>();
            VisListHandler handler = new();

            var actual = (List<Manager>)handler.GetNewItems(dt, allItems);

            Assert.AreEqual(0, actual.Count);
        }
        [TestMethod()]
        public void GetNewItemsManager__datatable_empty__allItems_oneItem__return_empty()
        {
            DataTable dt = new();
            dt.Columns.Add(new DataColumn("name", Type.GetType("System.String")));
            dt.Rows.Add(new object[] { "" });

            var allItems = new List<Manager>();
            VisListHandler handler = new();

            var actual = (List<Manager>)handler.GetNewItems(dt, allItems);

            Assert.AreEqual(0, actual.Count);
        }
        [TestMethod]
        public void GetNewItemsManager__datatable_2items__allItems_empty__return_2()
        {
            DataTable dt = new();
            dt.Columns.Add(new DataColumn("name", Type.GetType("System.String")));
            dt.Rows.Add(new object[] { "B" });
            dt.Rows.Add(new object[] { "N" });

            var allItems = new List<Manager>();
            VisListHandler handler = new();

            var actual = (List<Manager>)handler.GetNewItems(dt, allItems);

            Assert.AreEqual(2, actual.Count);
        }
        [TestMethod]
        public void GetNewItemsManager__datatable_2items__allItems_1item__retun1()
        {
            DataTable dt = new();
            dt.Columns.Add(new DataColumn("name", Type.GetType("System.String")));
            dt.Rows.Add(new object[] { "B" });
            dt.Rows.Add(new object[] { "N" });

            var allItems = new List<Manager> {
                new Manager { Name = "B" }
            };
            VisListHandler handler = new();

            var actual = (List<Manager>)handler.GetNewItems(dt, allItems);

            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual("N", actual[0].Name);
        }

        [TestMethod]
        public void GetItemsCompanies__datatable_2items__return_company()
        {
            DataTable dt = new();

            dt.Columns.Add(new DataColumn("клиент", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("счет", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("баланс", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("вал", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Ключ", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Номер", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("название", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Группа", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Привлечение", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Рекомендация", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Офис", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("дата открытия", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("дата закрытия", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("дата последней проводки", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("ИНН", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Комментарий", Type.GetType("System.String")));
            dt.Rows.Add(new object[] { "1", "123", "1", "2", "3", "4", "JS", "ND", "", "Jack", "00-2", "2023-11-11", "...", "2023-12-11 11:23:35", "444", "Comm"});
            dt.Rows.Add(new object[] { "1", "124", "1", "2", "3", "4", "JS", "ND", "", "Jack", "00-2", "2023-10-21", "...", "2023-02-01 12:03:55", "444", "Comm" });
            
            var groups = new List<Group> { new Group { Name = "ND", GroupId = 4 } };
            var managers = new List<Manager> { new Manager { Name = "Jack", ManagerId = 6 } };

            VisListHandler handler = new();
            var actual = (List<Company>)handler.GetItems(dt, groups, managers);
            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(1, actual[0].CompanyId);
            Assert.AreEqual(4, actual[0].GroupId);
            Assert.AreEqual(6, actual[0].ManagerId);
            Assert.AreEqual("JS", actual[0].Name);
            Assert.IsFalse(actual[0].IsAttraction);
            Assert.AreEqual("444", actual[0].Inn);
        }
        [TestMethod]
        public void GetItemsCompanies__datatable_3items__return_2company()
        {
            DataTable dt = new();

            dt.Columns.Add(new DataColumn("клиент", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("счет", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("баланс", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("вал", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Ключ", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Номер", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("название", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Группа", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Привлечение", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Рекомендация", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Офис", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("дата открытия", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("дата закрытия", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("дата последней проводки", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("ИНН", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Комментарий", Type.GetType("System.String")));
            dt.Rows.Add(new object[] { "1", "123", "1", "2", "3", "4", "JS", "ND", "", "Jack", "00-2", "2023-11-11", "...", "2023-12-11 11:23:35", "444", "Comm" });
            dt.Rows.Add(new object[] { "1", "124", "1", "2", "3", "4", "JS", "ND", "", "Jack", "00-2", "2023-10-21", "...", "2023-02-01 12:03:55", "444", "Comm" });
            dt.Rows.Add(new object[] { "2", "432", "1", "2", "3", "4", "HH", "Lola", "Да", "Maik", "00-3", "2022-10-21", "...", "2022-12-11 13:03:55", "555", "Comm" });

            var groups = new List<Group> { new Group { Name = "ND", GroupId = 4 } };
            var managers = new List<Manager> { new Manager { Name = "Jack", ManagerId = 6 } };

            VisListHandler handler = new();
            var actual = (List<Company>)handler.GetItems(dt, groups, managers);
            Assert.AreEqual(2, actual.Count);
            Assert.AreEqual(1, actual[0].CompanyId);
            Assert.AreEqual(4, actual[0].GroupId);
            Assert.AreEqual(6, actual[0].ManagerId);
            Assert.AreEqual("JS", actual[0].Name);
            Assert.IsFalse(actual[0].IsAttraction);
            Assert.AreEqual("444", actual[0].Inn);

            Assert.AreEqual(2, actual[1].CompanyId);
            Assert.AreEqual(1, actual[1].GroupId);
            Assert.AreEqual(1, actual[1].ManagerId);
            Assert.AreEqual("HH", actual[1].Name);
            Assert.IsTrue(actual[1].IsAttraction);
            Assert.AreEqual("555", actual[1].Inn);
        }

        [TestMethod]
        public void GetItemsAccounts__datatable_1item__return_account()
        {
            DataTable dt = new();

            dt.Columns.Add(new DataColumn("клиент", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("счет", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("баланс", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("вал", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Ключ", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Номер", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("название", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Группа", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Привлечение", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Рекомендация", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Офис", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("дата открытия", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("дата закрытия", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("дата последней проводки", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("ИНН", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Комментарий", Type.GetType("System.String")));

            dt.Rows.Add(new object[] { "1", "4080381010000298898", "1", "2", "3", "4", "JS", "ND", "", "Jack", "00-2", "2023-11-11", "...", "2023-12-11 11:23:35", "444", "Comm" });

            var offices = new List<Office> { new Office { Name = "00-2", OfficeId = 4 } };

            VisListHandler handler = new();
            var actual = (List<Account>)handler.GetItems(dt, offices);

            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(1, actual[0].CompanyId);
            Assert.AreEqual("4080381010000298898", actual[0].AccountNumber);
            Assert.AreEqual(new DateTime(2023, 11, 11), actual[0].DateOpen);
            Assert.IsNull(actual[0].DateClose);
            Assert.AreEqual(new DateTime(2023, 12, 11, 11, 23, 35), actual[0].DateTimeLastOperation);
        }
        [TestMethod]
        public void GetItemsAccounts__datatable_2item__return_accounts()
        {
            DataTable dt = new();

            dt.Columns.Add(new DataColumn("клиент", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("счет", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("баланс", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("вал", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Ключ", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Номер", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("название", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Группа", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Привлечение", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Рекомендация", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Офис", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("дата открытия", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("дата закрытия", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("дата последней проводки", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("ИНН", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Комментарий", Type.GetType("System.String")));

            dt.Rows.Add(new object[] { "1", "4080381010000298898", "1", "2", "3", "4", "JS", "ND", "", "Jack", "00-2", "11/11/2023", "...", "11/12/2023 11:23:35", "444", "Comm" });
            dt.Rows.Add(new object[] { "1", "4080381010000254398", "1", "2", "3", "4", "JS", "ND", "", "Jack", "00-3", null, "...", "...", "555", "Comm" });

            var offices = new List<Office> { new Office { Name = "00-2", OfficeId = 4 } };

            VisListHandler handler = new();
            var actual = (List<Account>)handler.GetItems(dt, offices);

            Assert.AreEqual(2, actual.Count);

            Assert.AreEqual(1, actual[0].CompanyId);
            Assert.AreEqual("4080381010000298898", actual[0].AccountNumber);
            Assert.AreEqual(new DateTime(2023, 11, 11), actual[0].DateOpen);
            Assert.IsNull(actual[0].DateClose);
            Assert.AreEqual(new DateTime(2023, 12, 11, 11, 23, 35), actual[0].DateTimeLastOperation);

            Assert.AreEqual(1, actual[1].CompanyId);
            Assert.AreEqual("4080381010000254398", actual[1].AccountNumber);
            Assert.IsNull(actual[1].DateOpen);
            Assert.IsNull(actual[1].DateClose);
            Assert.IsNull(actual[1].DateTimeLastOperation);
        }

        [TestMethod]
        public void CheckIntegrityOfCompanyData__datatable_not_error__return_empty()
        {
            var dt = new DataTable();

            dt.Columns.Add(new DataColumn("клиент", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("счет", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("баланс", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("вал", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Ключ", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Номер", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("название", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Группа", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Привлечение", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Рекомендация", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Офис", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("дата открытия", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("дата закрытия", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("дата последней проводки", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("ИНН", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Комментарий", Type.GetType("System.String")));

            dt.Rows.Add(new object[] { "1", "123", "1", "2", "3", "4", "JS", "ND", "", "Jack", "00-2", "2023-11-11", "...", "2023-12-11 11:23:35", "444", "Comm" });
            dt.Rows.Add(new object[] { "1", "124", "1", "2", "3", "4", "JS", "ND", "", "Jack", "00-2", "2023-10-21", "...", "2023-02-01 12:03:55", "444", "Comm" });

            dt.Rows.Add(new object[] { "2", "125", "1", "2", "3", "4", "GH", "MN", "Yes", "Gary", "00-2", "2023-11-11", "...", "2023-12-11 11:23:35", "555", "Comm" });
            dt.Rows.Add(new object[] { "2", "126", "1", "2", "3", "4", "GH", "MN", "Yes", "Gary", "00-2", "2023-10-21", "...", "2023-02-01 12:03:55", "555", "Comm" });

            VisListHandler handler = new();

            var actual = handler.CheckIntegrityOfCompanyData(dt);
            Assert.IsTrue(string.IsNullOrEmpty(actual));
        }
        [TestMethod]
        public void CheckIntegrityOfCompanyData__datatable_error_groupName__return_str()
        {
            var dt = new DataTable();

            dt.Columns.Add(new DataColumn("клиент", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("счет", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("баланс", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("вал", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Ключ", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Номер", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("название", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Группа", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Привлечение", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Рекомендация", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Офис", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("дата открытия", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("дата закрытия", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("дата последней проводки", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("ИНН", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Комментарий", Type.GetType("System.String")));

            dt.Rows.Add(new object[] { "1", "123", "1", "2", "3", "4", "JS", "ND", "", "Jack", "00-2", "2023-11-11", "...", "2023-12-11 11:23:35", "444", "Comm" });
            dt.Rows.Add(new object[] { "1", "124", "1", "2", "3", "4", "JS", "MN", "", "Jack", "00-2", "2023-10-21", "...", "2023-02-01 12:03:55", "444", "Comm" });

            dt.Rows.Add(new object[] { "2", "125", "1", "2", "3", "4", "GH", "MN", "Yes", "Gary", "00-2", "2023-11-11", "...", "2023-12-11 11:23:35", "555", "Comm" });
            dt.Rows.Add(new object[] { "2", "126", "1", "2", "3", "4", "GH", "MN", "Yes", "Gary", "00-2", "2023-10-21", "...", "2023-02-01 12:03:55", "555", "Comm" });

            VisListHandler handler = new();

            var actual = handler.CheckIntegrityOfCompanyData(dt);
            Assert.AreEqual("Компания: 1, неоднозначные данные по группе.\n", actual);
        }
        [TestMethod]
        public void CheckIntegrityOfCompanyData__datatable_error_groupName_attraction__return_str()
        {
            var dt = new DataTable();

            dt.Columns.Add(new DataColumn("клиент", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("счет", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("баланс", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("вал", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Ключ", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Номер", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("название", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Группа", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Привлечение", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Рекомендация", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Офис", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("дата открытия", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("дата закрытия", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("дата последней проводки", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("ИНН", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Комментарий", Type.GetType("System.String")));

            dt.Rows.Add(new object[] { "1", "123", "1", "2", "3", "4", "JS", "ND", "", "Jack", "00-2", "2023-11-11", "...", "2023-12-11 11:23:35", "444", "Comm" });
            dt.Rows.Add(new object[] { "1", "124", "1", "2", "3", "4", "JS", "MN", "Yes", "Jack", "00-2", "2023-10-21", "...", "2023-02-01 12:03:55", "444", "Comm" });

            dt.Rows.Add(new object[] { "2", "125", "1", "2", "3", "4", "GH", "MN", "Yes", "Gary", "00-2", "2023-11-11", "...", "2023-12-11 11:23:35", "555", "Comm" });
            dt.Rows.Add(new object[] { "2", "126", "1", "2", "3", "4", "GH", "MN", "Yes", "Gary", "00-2", "2023-10-21", "...", "2023-02-01 12:03:55", "555", "Comm" });

            VisListHandler handler = new();

            var actual = handler.CheckIntegrityOfCompanyData(dt);
            Assert.AreEqual("Компания: 1, неоднозначные данные по группе.\nКомпания: 1, неоднозначные данные по привлечению.\n", actual);
        }
        [TestMethod]
        public void CheckIntegrityOfCompanyData__datatable_error_groupName_attraction_managerName__return_str()
        {
            var dt = new DataTable();

            dt.Columns.Add(new DataColumn("клиент", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("счет", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("баланс", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("вал", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Ключ", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Номер", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("название", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Группа", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Привлечение", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Рекомендация", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Офис", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("дата открытия", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("дата закрытия", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("дата последней проводки", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("ИНН", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Комментарий", Type.GetType("System.String")));

            dt.Rows.Add(new object[] { "1", "123", "1", "2", "3", "4", "JS", "ND", "", "Jack", "00-2", "2023-11-11", "...", "2023-12-11 11:23:35", "444", "Comm" });
            dt.Rows.Add(new object[] { "1", "124", "1", "2", "3", "4", "JS", "MN", "Yes", "Gary", "00-2", "2023-10-21", "...", "2023-02-01 12:03:55", "444", "Comm" });

            dt.Rows.Add(new object[] { "2", "125", "1", "2", "3", "4", "GH", "MN", "Yes", "Gary", "00-2", "2023-11-11", "...", "2023-12-11 11:23:35", "555", "Comm" });
            dt.Rows.Add(new object[] { "2", "126", "1", "2", "3", "4", "GH", "MN", "Yes", "Gary", "00-2", "2023-10-21", "...", "2023-02-01 12:03:55", "555", "Comm" });

            VisListHandler handler = new();

            var actual = handler.CheckIntegrityOfCompanyData(dt);
            Assert.AreEqual("Компания: 1, неоднозначные данные по группе.\n" +
                "Компания: 1, неоднозначные данные по привлечению.\n" +
                "Компания: 1, неоднозначные данные по рекомендации.\n", actual);
        }
        [TestMethod]
        public void CheckIntegrityOfCompanyData__datatable_error_2company__return_str()
        {
            var dt = new DataTable();

            dt.Columns.Add(new DataColumn("клиент", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("счет", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("баланс", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("вал", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Ключ", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Номер", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("название", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Группа", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Привлечение", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Рекомендация", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Офис", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("дата открытия", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("дата закрытия", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("дата последней проводки", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("ИНН", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Комментарий", Type.GetType("System.String")));

            dt.Rows.Add(new object[] { "1", "123", "1", "2", "3", "4", "JS", "ND", "", "Jack", "00-2", "2023-11-11", "...", "2023-12-11 11:23:35", "444", "Comm" });
            dt.Rows.Add(new object[] { "1", "124", "1", "2", "3", "4", "JS", "MN", "Yes", "Gary", "00-2", "2023-10-21", "...", "2023-02-01 12:03:55", "444", "Comm" });

            dt.Rows.Add(new object[] { "2", "125", "1", "2", "3", "4", "GH", "MN", "Yes", "Gary", "00-2", "2023-11-11", "...", "2023-12-11 11:23:35", "555", "Comm" });
            dt.Rows.Add(new object[] { "2", "126", "1", "2", "3", "4", "GH", "ND", "Yes", "Gary", "00-2", "2023-10-21", "...", "2023-02-01 12:03:55", "555", "Comm" });

            VisListHandler handler = new();

            var actual = handler.CheckIntegrityOfCompanyData(dt);
            Assert.AreEqual("Компания: 1, неоднозначные данные по группе.\n" +
                "Компания: 1, неоднозначные данные по привлечению.\n" +
                "Компания: 1, неоднозначные данные по рекомендации.\n" +
                "Компания: 2, неоднозначные данные по группе.\n", actual);
        }
    }
}