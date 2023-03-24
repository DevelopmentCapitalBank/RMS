using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;

namespace RMS.UI.Services
{
    public class CurrencyRateParser : ICurrencyRateParser
    {
        public async Task<Dictionary<DateTime, double[]>> GetCourseAsync(DateTime from, DateTime to)
        {
            try
            {
                string CourseEUR = "";
                string CourseUSD = "";
                var xdoc = new XmlDocument();
                await Task.Run(() => xdoc.Load("http://www.cbr.ru/scripts/XML_daily.asp?date_req1=" + from.ToString("dd/MM/yyyy") +
                    "&date_req2=" + to.ToString("dd/MM/yyyy"))).ConfigureAwait(false);
                foreach (XmlNode node in xdoc.SelectNodes("//Valute"))
                {
                    switch (node.SelectSingleNode("CharCode").InnerText)
                    {
                        case "EUR":
                            CourseEUR = node.SelectSingleNode("Value").InnerText;
                            break;
                        case "USD":
                            CourseUSD = node.SelectSingleNode("Value").InnerText;
                            break;
                        default:
                            break;
                    }
                }
                //switch (currency)
                //{
                //    case Currency.usd:
                //        return ParseToDouble(CourseUSD);
                //    case Currency.eur:
                //        return ParseToDouble(CourseEUR);
                //    default:
                //        break;
                //}
                return new Dictionary<DateTime, double[]>();
            }
            catch
            {
                return new Dictionary<DateTime, double[]>();
            }
        }
        private static double ParseToDouble(string value)
        {
            value = value.Trim();
            if (!double.TryParse(value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.GetCultureInfo("ru-RU"), out double result))
            {
                if (!double.TryParse(value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.GetCultureInfo("en-US"), out result))
                {
                    return double.NaN;
                }
            }
            return result;
        }
    }
}
