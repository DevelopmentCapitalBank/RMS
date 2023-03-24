using System.Net;
using System.Xml;
using System.Xml.Linq;
using RMS.DATA;
using RMS.DATA.Entities;

namespace DebugConsole
{
    public class Program 
    {
        public static void Main(string[] args)
        {
            TestDb(new DateTime(2023, 1, 1), new DateTime(2023, 1, 31));
            Console.WriteLine("End.");
        }

        public static void TestDb(DateTime from, DateTime to)
        {
            var xdoc = new XmlDocument();
            xdoc.Load("http://www.cbr.ru/scripts/XML_daily.asp?date_req=" + from.ToString("dd/MM/yyyy")); //+
                                                                                                          //"&date_req2=" + to.ToString("dd/MM/yyyy"));
        }
    }
}

