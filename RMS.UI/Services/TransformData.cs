using System;
using System.Data;
using System.Diagnostics;
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

        public async Task<string> Transform(TypeDocument type, DbContext context, DataTable dataTable)
        {
            switch (type)
            {
                case TypeDocument.VisList:
                    return await TransformVisListAsync(context, dataTable);
                    break;
                case TypeDocument.Turnovers:
                    return await TransformTurnoversAsync(context, dataTable);
                    break;
                case TypeDocument.Deposits:
                    return await TransformDepositsAsync(context, dataTable);
                    break;
                case TypeDocument.Operation:
                    return await TransformOperationsAsync(context, dataTable);
                    break;
                case TypeDocument.Conversion:
                    return await TransformConversionsAsync(context, dataTable);
                    break;
                default:
                    return string.Empty;
                    break;
            }
        }

        private async Task<string> TransformVisListAsync(DbContext context, DataTable dataTable)
        {
            string d = handler.CheckIntegrityOfCompanyData(dataTable);
            if (string.IsNullOrEmpty(d))
            {
                var groups = await context.Groups.ReadAllAsync().ConfigureAwait(false);
                var newGroups = handler.GetNewItems(new DataView(dataTable).ToTable(true, "Группа"), groups);
                newGroups = await context.Groups.CreateListOfEntitiesAsync(newGroups).ConfigureAwait(false);
                var allGroups = groups.Union(newGroups);

                var managers = await context.Managers.ReadAllAsync().ConfigureAwait(false);
                var newManagers = handler.GetNewItems(new DataView(dataTable).ToTable(true, "Рекомендация"), managers);
                newManagers = await context.Managers.CreateListOfEntitiesAsync(newManagers).ConfigureAwait(false);
                var allManagers = managers.Union(newManagers);

                var offices = await context.Offices.ReadAllAsync().ConfigureAwait(false);
                var newOffices = handler.GetNewItems(new DataView(dataTable).ToTable(true, "Офис"), offices);
                newOffices = await context.Offices.CreateListOfEntitiesAsync(newOffices).ConfigureAwait(false);
                var allOffices = offices.Union(newOffices);

                var companies = await context.Companies.ReadAllAsync().ConfigureAwait(false);
                var currentCompanies = handler.GetItems(dataTable, allGroups, allManagers);
                var newCompanies = handler.GetNewItems(currentCompanies, companies);
                _ = await context.Companies.CreateListOfEntitiesAsync(newCompanies).ConfigureAwait(false);
                var companiesToUpdate = handler.GetItemsToUpdate(currentCompanies, companies);
                await context.Companies.UpdateListOfEntitiesAsync(companiesToUpdate).ConfigureAwait(false);

                var accounts = await context.Accounts.ReadAllAsync().ConfigureAwait(false);
                var currentAccounts = handler.GetItems(dataTable, allOffices);
                var newAccounts = handler.GetNewItems(currentAccounts, accounts);
                _ = await context.Accounts.CreateListOfEntitiesAsync(newAccounts).ConfigureAwait(false);
                var accountsToUpdate = handler.GetItemsToUpdate(currentAccounts, accounts);
                await context.Accounts.UpdateListOfEntitiesAsync(accountsToUpdate).ConfigureAwait(false);
            }
            return d;
        }

        private async Task<string> TransformTurnoversAsync(DbContext context, DataTable dataTable)
        {
            throw new NotImplementedException();
        }

        private async Task<string> TransformDepositsAsync(DbContext context, DataTable dataTable)
        {
            throw new NotImplementedException();
        }

        private async Task<string> TransformOperationsAsync(DbContext context, DataTable dataTable)
        {
            throw new NotImplementedException();
        }

        private async Task<string> TransformConversionsAsync(DbContext context, DataTable dataTable)
        {
            throw new NotImplementedException();
        }
    }
}
