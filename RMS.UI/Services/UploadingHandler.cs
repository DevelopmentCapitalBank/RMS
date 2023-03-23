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
            List<Operation> operations = new();

            foreach (DataRow r in dt.Rows)
            {
                if (r.ItemArray[8].ToString().ToLower().Trim() == "проведен")
                {
                    operations.Add(new Operation {
                        DateOfUnloading = date,
                        Amount = Convert.ToDecimal(r.ItemArray[3]),
                        AmountEquivalent = r.ItemArray[4] == DBNull.Value ? 0M : Convert.ToDecimal(r.ItemArray[4]),
                        DateOperation = Convert.ToDateTime(r.ItemArray[9]),
                        Purpose = Convert.ToString(r.ItemArray[10]),
                        Payer = Convert.ToString(r.ItemArray[13]),
                        Recipient = Convert.ToString(r.ItemArray[14])
                    });
                }
            }

            return operations;
        }

        public IEnumerable<Remains> GetRemains(DataTable dt, DateTime date)
        {
            List<Remains> remains = new();

            foreach (DataRow r in dt.Rows)
            {
                remains.Add(new Remains {
                    DateOfUnloading= date,
                    Account = Convert.ToString(r.ItemArray[0]),
                    Debit = Convert.ToDecimal(r.ItemArray[4]),
                    Credit = Convert.ToDecimal(r.ItemArray[5]),
                    AverageBalance = Convert.ToDecimal(r.ItemArray[7])
                });
            }

            return remains;
        }
    }
}
