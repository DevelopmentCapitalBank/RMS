using System;
using System.Data;
using System.Threading.Tasks;
using RMS.DATA;
using RMS.DocumentProcessing.Verification;

namespace RMS.UI.Services
{
    public interface ITransformData
    {
        Task<string> Transform(TypeDocument type, DbContext context, DataTable dt, DateTime date);
    }
}
