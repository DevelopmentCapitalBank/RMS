using RMS.DATA.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace RMS.UI.Services
{
    public class VisListHandler : IVisListHandler 
    {
        public IEnumerable<Group> GetNewItems(DataTable dt, IEnumerable<Group> allItems)
        {
            List<Group> results = new();
            foreach (DataRow row in dt.Rows)
            {
                string groupName = row.ItemArray[0].ToString().Trim();
                if (!string.IsNullOrEmpty(groupName))
                {
                    var g = allItems.FirstOrDefault(i => string.Compare(i.Name, groupName) == 0);
                    if (g == null)
                    {
                        results.Add(new Group { Name = groupName, });
                    }
                }
            }
            return results;
        }

        public IEnumerable<Manager> GetNewItems(DataTable dt, IEnumerable<Manager> allItems)
        {
            List<Manager> results = new();
            foreach (DataRow row in dt.Rows)
            {
                string managerName = row.ItemArray[0].ToString().Trim();
                if (!string.IsNullOrEmpty(managerName))
                {
                    var g = allItems.FirstOrDefault(i => string.Compare(i.Name, managerName) == 0);
                    if (g == null)
                    {
                        results.Add(new Manager { Name = managerName, });
                    }
                }
            }
            return results;
        }

        public IEnumerable<Office> GetNewItems(DataTable dt, IEnumerable<Office> allItems)
        {
            List<Office> results = new();
            foreach (DataRow row in dt.Rows)
            {
                string officeName = row.ItemArray[0].ToString().Trim();
                if (!string.IsNullOrEmpty(officeName))
                {
                    var g = allItems.FirstOrDefault(i => string.Compare(i.Name, officeName) == 0);
                    if (g == null)
                    {
                        results.Add(new Office { Name = officeName, });
                    }
                }
            }
            return results;
        }

        public IEnumerable<Company> GetNewItems(DataTable dt, IEnumerable<Company> allItems, IEnumerable<Group> groups, IEnumerable<Manager> managers)
        {
            throw new System.NotImplementedException();
        }
        public IEnumerable<Account> GetNewItems(DataTable dt, IEnumerable<Account> allItems, IEnumerable<Office> offices)
        {
            throw new System.NotImplementedException();
        }
        public IEnumerable<Company> GetItemsToUpdate(DataTable dt, IEnumerable<Company> allItems)
        {
            throw new System.NotImplementedException();
        }
        public IEnumerable<Account> GetItemsToUpdate(DataTable dt, IEnumerable<Account> allItems)
        {
            throw new System.NotImplementedException();
        }
        public string CheckIntegrityOfCompanyData(DataTable dt)
        {
            throw new System.NotImplementedException();
        }
    }
}
