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
        IEnumerable<Company> GetNewItems(DataTable dt, IEnumerable<Company> allItems);
        IEnumerable<Account> GetNewItems(DataTable dt, IEnumerable<Account> allItems);
        IEnumerable<Company> GetItemsToUpdate(DataTable dt, IEnumerable<Company> allItems);
        IEnumerable<Account> GetItemsToUpdate(DataTable dt, IEnumerable<Account> allItems);
    }
}
