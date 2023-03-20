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
            throw new NotImplementedException();
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
