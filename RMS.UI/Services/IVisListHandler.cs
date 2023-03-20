using System.Collections.Generic;
using System.Data;
using RMS.DATA.Entities;

namespace RMS.UI.Services
{
    /// <summary>
    /// обработчик данных из визлиста
    /// </summary>
    public interface IVisListHandler
    {
        IEnumerable<Group> GetNewItems(DataTable dt, IEnumerable<Group> allItems);
        IEnumerable<Manager> GetNewItems(DataTable dt, IEnumerable<Manager> allItems);
        IEnumerable<Office> GetNewItems(DataTable dt, IEnumerable<Office> allItems);
        IEnumerable<Company> GetItems(DataTable dt, IEnumerable<Group> groups, IEnumerable<Manager> managers);
        IEnumerable<Account> GetItems(DataTable dt, IEnumerable<Office> offices);
        IEnumerable<Company> GetNewItems(IEnumerable<Company> currentItems, IEnumerable<Company> oldItems);
        IEnumerable<Account> GetNewItems(IEnumerable<Account> currentItems, IEnumerable<Account> oldItems);
        IEnumerable<Company> GetItemsToUpdate(IEnumerable<Company> currentItems, IEnumerable<Company> oldItems);
        IEnumerable<Account> GetItemsToUpdate(IEnumerable<Account> currentItems, IEnumerable<Account> oldItems);
        string CheckIntegrityOfCompanyData(DataTable dt);
    }
}
