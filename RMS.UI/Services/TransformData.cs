using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using RMS.DATA;
using RMS.DocumentProcessing.Verification;

namespace RMS.UI.Services
{
    public class TransformData : ITransformData
    {
        private readonly IVisListHandler visListHandler;
        private readonly IUploadingHandler uploadingHandler;
        public TransformData(IVisListHandler visListHandler, IUploadingHandler uploadingHandler)
        {
            this.visListHandler = visListHandler;
            this.uploadingHandler = uploadingHandler;
        }

        public async Task<string> Transform(TypeDocument type, DbContext context, DataTable dataTable, DateTime date)
        {
            switch (type)
            {
                case TypeDocument.VisList:
                    return await TransformVisListAsync(context, dataTable);
                    break;
                case TypeDocument.Turnovers:
                    return await TransformTurnoversAsync(context, dataTable, date);
                    break;
                case TypeDocument.Deposits:
                    return await TransformDepositsAsync(context, dataTable, date);
                    break;
                case TypeDocument.Operation:
                    return await TransformOperationsAsync(context, dataTable, date);
                    break;
                case TypeDocument.Conversion:
                    return await TransformConversionsAsync(context, dataTable, date);
                    break;
                default:
                    return string.Empty;
                    break;
            }
        }

        private async Task<string> TransformVisListAsync(DbContext context, DataTable dataTable)
        {
            string d = visListHandler.CheckIntegrityOfCompanyData(dataTable);
            if (string.IsNullOrEmpty(d))
            {
                var groups = await context.Groups.ReadAllAsync().ConfigureAwait(false);
                var newGroups = visListHandler.GetNewItems(new DataView(dataTable).ToTable(true, "Группа"), groups);
                newGroups = await context.Groups.CreateListOfEntitiesAsync(newGroups).ConfigureAwait(false);
                var allGroups = groups.Union(newGroups);

                var managers = await context.Managers.ReadAllAsync().ConfigureAwait(false);
                var newManagers = visListHandler.GetNewItems(new DataView(dataTable).ToTable(true, "Рекомендация"), managers);
                newManagers = await context.Managers.CreateListOfEntitiesAsync(newManagers).ConfigureAwait(false);
                var allManagers = managers.Union(newManagers);

                var offices = await context.Offices.ReadAllAsync().ConfigureAwait(false);
                var newOffices = visListHandler.GetNewItems(new DataView(dataTable).ToTable(true, "Офис"), offices);
                newOffices = await context.Offices.CreateListOfEntitiesAsync(newOffices).ConfigureAwait(false);
                var allOffices = offices.Union(newOffices);

                var companies = await context.Companies.ReadAllAsync().ConfigureAwait(false);
                var currentCompanies = visListHandler.GetItems(dataTable, allGroups, allManagers);
                var newCompanies = visListHandler.GetNewItems(currentCompanies, companies);
                _ = await context.Companies.CreateListOfEntitiesAsync(newCompanies).ConfigureAwait(false);
                var companiesToUpdate = visListHandler.GetItemsToUpdate(currentCompanies, companies);
                await context.Companies.UpdateListOfEntitiesAsync(companiesToUpdate).ConfigureAwait(false);

                var accounts = await context.Accounts.ReadAllAsync().ConfigureAwait(false);
                var currentAccounts = visListHandler.GetItems(dataTable, allOffices);
                var newAccounts = visListHandler.GetNewItems(currentAccounts, accounts);
                _ = await context.Accounts.CreateListOfEntitiesAsync(newAccounts).ConfigureAwait(false);
                var accountsToUpdate = visListHandler.GetItemsToUpdate(currentAccounts, accounts);
                await context.Accounts.UpdateListOfEntitiesAsync(accountsToUpdate).ConfigureAwait(false);
            }
            return d;
        }

        private async Task<string> TransformTurnoversAsync(DbContext context, DataTable dataTable, DateTime date)
        {
            throw new NotImplementedException();
        }

        private async Task<string> TransformDepositsAsync(DbContext context, DataTable dataTable, DateTime date)
        {
            throw new NotImplementedException();
        }

        private async Task<string> TransformOperationsAsync(DbContext context, DataTable dataTable, DateTime date)
        {
            throw new NotImplementedException();
        }

        private async Task<string> TransformConversionsAsync(DbContext context, DataTable dataTable, DateTime date)
        {
            int countRow = await context.Conversions.GetCountAsync(date);
            if (countRow == 0)
            {
                var newOperations = uploadingHandler.GetConversions(dataTable, date);
                _ = await context.Conversions.CreateListOfEntitiesAsync(newOperations).ConfigureAwait(false);
                return string.Empty;
            }
            else
            {
                return "Данные конверсии ДБО за этот период уже есть в базе данных!\n";
            }
        }
    }
}
