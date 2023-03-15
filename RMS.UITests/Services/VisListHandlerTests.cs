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
    }
}