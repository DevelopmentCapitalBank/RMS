using System.Collections.Generic;
using System.Data;

namespace RMS.UI.Services
{
    /// <summary>
    /// обработчик данных из визлиста
    /// </summary>
    /// <typeparam name="T">сущность/набор сущностей которую необходимо извлечь</typeparam>
    public interface IVisListHandler<T> where T : class
    {
        IEnumerable<T> GetNewItems(DataTable dt, IEnumerable<T> allItems);
    }
}
