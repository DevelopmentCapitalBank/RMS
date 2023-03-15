using System;
using System.Data;
using System.Threading.Tasks;
using RMS.DATA;
using RMS.DocumentProcessing.Verification;

namespace RMS.UI.Services
{
    public class TransformData : ITransformData
    {
        public async Task Transform(TypeDocument type, DbContext context, DataTable dataTable)
        {
            switch (type)
            {
                case TypeDocument.VisList:
                    await TransformVisListAsync(context, dataTable);
                    break;
                case TypeDocument.Turnovers:
                    await TransformTurnoversAsync(context, dataTable);
                    break;
                case TypeDocument.Deposits:
                    await TransformTurnoversAsync(context, dataTable);
                    break;
                case TypeDocument.Operation:
                    await TransformOperationsAsync(context, dataTable);
                    break;
                case TypeDocument.Conversion:
                    await TransformConversionsAsync(context, dataTable);
                    break;
                default:
                    break;
            }
        }

        private async Task TransformVisListAsync(DbContext context, DataTable dataTable)
        {
            throw new NotImplementedException();
        }

        private async Task TransformTurnoversAsync(DbContext context, DataTable dataTable)
        {
            throw new NotImplementedException();
        }

        private async Task TransformOperationsAsync(DbContext context, DataTable dataTable)
        {
            throw new NotImplementedException();
        }

        private async Task TransformConversionsAsync(DbContext context, DataTable dataTable)
        {
            throw new NotImplementedException();
        }
    }
}
