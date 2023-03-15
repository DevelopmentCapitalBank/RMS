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

            DataView view = new DataView(dataTable);
            DataTable distinctValues = view.ToTable(true, "Группа");
            foreach (DataRow row in distinctValues.Rows)
            {
                string groupName = row.ItemArray[0].ToString().Trim();
                if (!string.IsNullOrEmpty(groupName))
                {
                    var g = groups.FirstOrDefault(i => string.Compare(i.Name, groupName) == 0);
                    //if (g == null)
                    //{
                    //    await context.Groups.CreateAsync(new Group { Name = groupName }).ConfigureAwait(false);
                    //}
                }
            }

            view = new DataView(dataTable);
            distinctValues = view.ToTable(true, "Рекомендация");
            foreach (DataRow row in distinctValues.Rows)
            {
                string managerName = row.ItemArray[0].ToString().Trim();
                if (!string.IsNullOrEmpty(managerName))
                {
                    var m = managers.FirstOrDefault(i => string.Compare(i.Name, managerName) == 0);
                    //if (m == null)
                    //{
                    //    await context.Managers.CreateAsync(new Manager { Name = managerName }).ConfigureAwait(false);
                    //}
                }
            }

            view = new DataView(dataTable);
            distinctValues = view.ToTable(true, "Офис");
            foreach (DataRow row in distinctValues.Rows)
            {
                string officeName = row.ItemArray[0].ToString().Trim();
                if (!string.IsNullOrEmpty(officeName))
                {
                    var g = offices.FirstOrDefault(i => string.Compare(i.Name, officeName) == 0);
                    //if (g == null)
                    //{
                    //    await context.Offices.CreateAsync(new Office { Name = officeName }).ConfigureAwait(false);
                    //}
                }
            }
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
