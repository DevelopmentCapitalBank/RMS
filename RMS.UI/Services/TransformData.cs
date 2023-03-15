using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using RMS.DATA;
using RMS.DATA.Entities;
using RMS.DocumentProcessing.Verification;

namespace RMS.UI.Services
{
    public class TransformData : ITransformData 
    {
        private readonly IVisListHandler handler;
        public TransformData(IVisListHandler handler)
        {
            this.handler = handler;
        }

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
            var offices = await context.Offices.ReadAllAsync().ConfigureAwait(false);
            var companies = await context.Companies.ReadAllAsync().ConfigureAwait(false);
            var accounts = await context.Accounts.ReadAllAsync().ConfigureAwait(false);

            var newGroups = handler.GetNewItems(new DataView(dataTable).ToTable(true, "Группа"), groups);
            var newManagers = handler.GetNewItems(new DataView(dataTable).ToTable(true, "Рекомендация"), managers);
            var newOffices = handler.GetNewItems(new DataView(dataTable).ToTable(true, "Офис"), offices);
            var newCompanies = handler.GetNewItems(dataTable, companies);
            var newAccounts = handler.GetNewItems(dataTable, accounts);

            await context.Groups.CreateListOfEntitiesAsync(newGroups).ConfigureAwait(false);
            await context.Managers.CreateListOfEntitiesAsync(newManagers).ConfigureAwait(false);
            await context.Offices.CreateListOfEntitiesAsync(newOffices).ConfigureAwait(false);
            await context.Companies.CreateListOfEntitiesAsync(newCompanies).ConfigureAwait(false);
            await context.Accounts.CreateListOfEntitiesAsync(newAccounts).ConfigureAwait(false);

            var companiesToUpdate = handler.GetItemsToUpdate(dataTable, companies);
            var accountsToUpdate = handler.GetItemsToUpdate(dataTable, accounts);

            await context.Companies.UpdateListOfEntitiesAsync(newCompanies).ConfigureAwait(false);
            await context.Accounts.UpdateListOfEntitiesAsync(newAccounts).ConfigureAwait(false);
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
