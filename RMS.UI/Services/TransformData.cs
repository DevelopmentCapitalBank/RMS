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
                DateTime dold = DateTime.Now;
                var groups = await context.Groups.ReadAllAsync().ConfigureAwait(false);
                var managers = await context.Managers.ReadAllAsync().ConfigureAwait(false);
                var offices = await context.Offices.ReadAllAsync().ConfigureAwait(false);
                var companies = await context.Companies.ReadAllAsync().ConfigureAwait(false);
                var accounts = await context.Accounts.ReadAllAsync().ConfigureAwait(false);

                var newGroups = handler.GetNewItems(new DataView(dataTable).ToTable(true, "Группа"), groups);
                var newManagers = handler.GetNewItems(new DataView(dataTable).ToTable(true, "Рекомендация"), managers);
                var newOffices = handler.GetNewItems(new DataView(dataTable).ToTable(true, "Офис"), offices);

                newGroups = await context.Groups.CreateListOfEntitiesAsync(newGroups).ConfigureAwait(false);
                var allGroups = groups.Union(newGroups);
                newManagers = await context.Managers.CreateListOfEntitiesAsync(newManagers).ConfigureAwait(false);
                var allManagers = managers.Union(newManagers);
                newOffices = await context.Offices.CreateListOfEntitiesAsync(newOffices).ConfigureAwait(false);
                var allOffices = offices.Union(newOffices);

                var newCompanies = handler.GetNewItems(dataTable, companies, allGroups, allManagers);
                newCompanies = await context.Companies.CreateListOfEntitiesAsync(newCompanies).ConfigureAwait(false);
                var allCompanies = companies.Union(newCompanies);

                var newAccounts = handler.GetNewItems(dataTable, accounts, allOffices);
                newAccounts = await context.Accounts.CreateListOfEntitiesAsync(newAccounts).ConfigureAwait(false);
                var allAccounts = accounts.Union(newAccounts);

                //TODO Подумать над реализацией события для выполнения нескольких комманд к бд за одну транзакцию(соединение)
                //Dependency injection in class lib

                var companiesToUpdate = handler.GetItemsToUpdate(dataTable, allCompanies);
                var accountsToUpdate = handler.GetItemsToUpdate(dataTable, allAccounts);

                await context.Companies.UpdateListOfEntitiesAsync(companiesToUpdate).ConfigureAwait(false);
                await context.Accounts.UpdateListOfEntitiesAsync(accountsToUpdate).ConfigureAwait(false);

                TimeSpan sp = DateTime.Now - dold;
                Debug.Print("millesec-" + sp.TotalMilliseconds.ToString() + " | sec-" + sp.TotalSeconds.ToString());
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
