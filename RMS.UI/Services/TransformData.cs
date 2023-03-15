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
                    await TransformDepositsAsync(context, dataTable);
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
            var groups = await context.Groups.ReadAllAsync().ConfigureAwait(false);
            var managers = await context.Managers.ReadAllAsync().ConfigureAwait(false);
            var companies = await context.Companies.ReadAllAsync().ConfigureAwait(false);
            var accounts = await context.Accounts.ReadAllAsync().ConfigureAwait(false);
        }

        private async Task TransformTurnoversAsync(DbContext context, DataTable dataTable)
        {
            throw new NotImplementedException();
        }

        private async Task TransformDepositsAsync(DbContext context, DataTable dataTable)
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
