using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RMS.UI.Services
{
    /// <summary>
    /// сервис парсинга курсов валют с цб
    /// </summary>
    public interface ICurrencyRateParser
    {
        /// <summary>
        /// возвращает словарь курсов валют за некоторый диапазон дат :
        /// 
        /// Key         Value (double[])
        /// Дата        USD     EUR     CNY
        /// 2023-01-01  78.23   82.12   12.22 
        /// 
        /// </summary>
        /// <param name="from">дата от</param>
        /// <param name="to">дата до</param>
        /// <returns></returns>
        Task<Dictionary<DateTime, double[]>> GetCourseAsync(DateTime from, DateTime to);
    }
}
