using System;
using System.Collections.Generic;
using System.Data;
using RMS.DATA.Entities;

namespace RMS.UI.Services
{
    public interface IUploadingHandler
    {
        IEnumerable<Remains> GetRemains(DataTable dt, DateTime date);
        IEnumerable<Operation> GetOperations(DataTable dt, DateTime date);
        IEnumerable<Conversion> GetConversions(DataTable dt, DateTime date);
    }
}
