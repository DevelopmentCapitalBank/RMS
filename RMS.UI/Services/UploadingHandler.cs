using System;
using System.Collections.Generic;
using System.Data;
using RMS.DATA.Entities;

namespace RMS.UI.Services
{
    public class UploadingHandler : IUploadingHandler
    {
        public IEnumerable<Conversion> GetConversions(DataTable dt, DateTime date)
        {
            List<Conversion> conversions = new();

            foreach(DataRow r in dt.Rows)
            {
                conversions.Add(new Conversion {
                    DateOperation = Convert.ToDateTime(r.ItemArray[1]),
                    TypeOfTransaction = r.ItemArray[2].ToString().Trim(),
                    ReceivesAmount = Convert.ToDecimal(r.ItemArray[5]),
                    ReceivedCurrency = Convert.ToString(r.ItemArray[6]),
                    GivesAmount = Convert.ToDecimal(r.ItemArray[8]),
                    GivesCurrency = Convert.ToString(r.ItemArray[9]),
                    RateCurrencyOfCrediting = Convert.ToDecimal(r.ItemArray[15]),
                    RateCurrencyOfDebiting = Convert.ToDecimal(r.ItemArray[16]),
                    ReceivesToAccount = Convert.ToString(r.ItemArray[17]),
                    GivesFromAccount = Convert.ToString(r.ItemArray[18])
                });
            }

            return conversions;
        }

        public IEnumerable<Operation> GetOperations(DataTable dt, DateTime date)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Remains> GetRemains(DataTable dt, DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
