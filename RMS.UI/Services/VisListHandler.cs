﻿using RMS.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace RMS.UI.Services
{
    public class VisListHandler : IVisListHandler
    {
        public string CheckIntegrityOfCompanyData(DataTable dt)
        {
            StringBuilder result = new();

            Dictionary<int, string[]> fields = new();

            foreach (DataRow r in dt.Rows)
            {
                int companyId = int.Parse(r.ItemArray[0].ToString().Trim());
                string groupName = r.ItemArray[7].ToString().Trim();
                string attraction = r.ItemArray[8].ToString().Trim();
                string managerName = r.ItemArray[9].ToString().Trim();
                if (fields.ContainsKey(companyId))
                {
                    string[] values = fields[companyId];
                    if (string.Compare(values[0], groupName) != 0)
                    {
                        result.Append($"Компания: {companyId}, неоднозначные данные по группе.\n");
                    }
                    if (string.Compare(values[1], attraction) != 0)
                    {
                        result.Append($"Компания: {companyId}, неоднозначные данные по привлечению.\n");
                    }
                    if (string.Compare(values[2], managerName) != 0)
                    {
                        result.Append($"Компания: {companyId}, неоднозначные данные по рекомендации.\n");
                    }
                }
                else
                {
                    fields.Add(companyId, new string[] { groupName, attraction, managerName });
                }
            }

            return result.ToString();
        }
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

        public IEnumerable<Company> GetItems(DataTable dt, IEnumerable<Group> groups, IEnumerable<Manager> managers)
        {
            Dictionary<int, Company> fields = new();

            foreach(DataRow r in dt.Rows)
            {
                int companyId = int.Parse(r.ItemArray[0].ToString().Trim());

                if (!fields.ContainsKey(companyId))
                {
                    var group = groups.FirstOrDefault(g => string.Equals(g.Name, r.ItemArray[7].ToString().Trim()));
                    var manager = managers.FirstOrDefault(m => string.Equals(m.Name, r.ItemArray[9].ToString().Trim()));

                    fields.Add(companyId, new Company {
                        CompanyId = companyId,
                        GroupId = group == null ? 1 : group.GroupId,
                        ManagerId = manager == null ? 1 : manager.ManagerId,
                        Name = r.ItemArray[6].ToString().Trim(),
                        IsAttraction = r.ItemArray[8].ToString().Trim().ToLower() == "да",
                        Inn = r.ItemArray[14].ToString().Trim(),
                        Comment = r.ItemArray[15].ToString().Trim()
                    });
                }
            }

            return fields.Values.ToList();
        }

        public IEnumerable<Account> GetItems(DataTable dt, IEnumerable<Office> offices)
        {
            Dictionary<string, Account> fields = new();

            foreach (DataRow r in dt.Rows)
            {
                var accountNumber = r.ItemArray[1].ToString().Trim();

                if (!fields.ContainsKey(accountNumber))
                {
                    var office = offices.FirstOrDefault(g => string.Equals(g.Name, r.ItemArray[10].ToString().Trim()));
                    var strDateOpen = r.ItemArray[11] == DBNull.Value ? null : r.ItemArray[11].ToString().Trim();
                    var strDateClose = r.ItemArray[12] == DBNull.Value ? null : r.ItemArray[12].ToString().Trim();
                    if (strDateClose != null && string.Equals(strDateClose, "..."))
                    {
                        strDateClose = null;
                    }
                    var strDateTimeLastOperation = r.ItemArray[13] == DBNull.Value ? null : r.ItemArray[13].ToString().Trim();
                    if (strDateTimeLastOperation != null && string.Equals(strDateTimeLastOperation, "..."))
                    {
                        strDateTimeLastOperation = null;
                    }

                    fields.Add(accountNumber, new Account {
                        CompanyId = int.Parse(r.ItemArray[0].ToString().Trim()),
                        OfficeId = office == null ? 1 : office.OfficeId,
                        AccountNumber = accountNumber,
                        DateOpen = strDateOpen == null ? null : Convert.ToDateTime(strDateOpen),
                        DateClose = strDateClose == null ? null : Convert.ToDateTime(strDateClose),
                        DateTimeLastOperation = strDateTimeLastOperation == null ? null : Convert.ToDateTime(strDateTimeLastOperation)
                    });
                }
            }

            return fields.Values.ToList();
        }

        public IEnumerable<Company> GetNewItems(IEnumerable<Company> currentItems, IEnumerable<Company> oldItems)
        {
            List<Company> newItems = new();

            foreach(var company in currentItems)
            {
                var c = oldItems.FirstOrDefault(com => com.CompanyId == company.CompanyId);
                if (c == null)
                {
                    newItems.Add(company);
                }
            }

            return newItems;
        }

        public IEnumerable<Account> GetNewItems(IEnumerable<Account> currentItems, IEnumerable<Account> oldItems)
        {
            List<Account> newItems = new();

            foreach (var acc in currentItems)
            {
                var a = oldItems.FirstOrDefault(ac => string.Equals(ac.AccountNumber,acc.AccountNumber));
                if (a == null)
                {
                    newItems.Add(acc);
                }
            }

            return newItems;
        }

        public IEnumerable<Company> GetItemsToUpdate(IEnumerable<Company> currentItems, IEnumerable<Company> oldItems)
        {
            List<Company> companies = new();

            foreach (var c in oldItems)
            {
                var cmp = currentItems.FirstOrDefault(cm => cm.CompanyId == c.CompanyId);
                if (cmp != null)
                {
                    if (!cmp.Equals(c))
                    {
                        companies.Add(cmp);
                    }
                }
            }

            return companies;
        }

        public IEnumerable<Account> GetItemsToUpdate(IEnumerable<Account> currentItems, IEnumerable<Account> oldItems)
        {
            List<Account> accounts = new();

            foreach (var a in oldItems)
            {
                var acc = currentItems.FirstOrDefault(ac => string.Equals(ac.AccountNumber, a.AccountNumber));
                if (acc != null)
                {
                    if (!acc.Equals(a))
                    {
                        acc.AccountId = a.AccountId;
                        accounts.Add(acc);
                    }
                }
            }

            return accounts;
        }
    }
}
