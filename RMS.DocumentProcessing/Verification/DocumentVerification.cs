using System.Data;

namespace RMS.DocumentProcessing.Verification
{
    public class DocumentVerification : IDocumentVerification
    {
        public bool IsVerified(TypeDocument document, DataTable dataTable)
        {
            bool isVerified = false;
            switch (document)
            {
                case TypeDocument.VisList:
                    isVerified = IsVerifiedVisList(dataTable);
                    break;
                case TypeDocument.Turnovers:
                    isVerified = IsVerifiedTurnovers(dataTable);
                    break;
                case TypeDocument.Deposits:
                    isVerified = IsVerifiedTurnovers(dataTable);
                    break;
                case TypeDocument.Operation:
                    isVerified = IsVerifiedOperations(dataTable);
                    break;
                case TypeDocument.Conversion:
                    isVerified = IsVerifiedConversions(dataTable);
                    break;
                default:
                    break;
            }
            return isVerified;
        }

        private bool IsVerifiedConversions(DataTable dataTable)
        {
            if (string.Compare(dataTable.Columns[0].ColumnName, "Номер") != 0) return false;
            if (string.Compare(dataTable.Columns[1].ColumnName, "Дата") != 0) return false;
            if (string.Compare(dataTable.Columns[2].ColumnName, "Вид сделки") != 0) return false;
            if (string.Compare(dataTable.Columns[3].ColumnName, "Cтатус") != 0) return false;
            if (string.Compare(dataTable.Columns[4].ColumnName, "Контрагент") != 0) return false;
            if (string.Compare(dataTable.Columns[5].ColumnName, "Банк получает сумму") != 0) return false;
            if (string.Compare(dataTable.Columns[6].ColumnName, "Вал") != 0) return false;
            if (string.Compare(dataTable.Columns[7].ColumnName, "Дата зачисления") != 0) return false;
            if (string.Compare(dataTable.Columns[8].ColumnName, "Банк отдает сумму") != 0) return false;
            if (string.Compare(dataTable.Columns[9].ColumnName, "Вал1") != 0) return false;
            if (string.Compare(dataTable.Columns[10].ColumnName, "Дата списания") != 0) return false;
            if (string.Compare(dataTable.Columns[11].ColumnName, "Курс остатка") != 0) return false;
            if (string.Compare(dataTable.Columns[12].ColumnName, "Док") != 0) return false;
            if (string.Compare(dataTable.Columns[13].ColumnName, "Сальдо переоценки") != 0) return false;
            if (string.Compare(dataTable.Columns[14].ColumnName, "Состояние документов") != 0) return false;
            if (string.Compare(dataTable.Columns[15].ColumnName, "Курс ЦБ (Валюта зачисления)") != 0) return false;
            if (string.Compare(dataTable.Columns[16].ColumnName, "Курс ЦБ (Валюта списания)") != 0) return false;
            if (string.Compare(dataTable.Columns[17].ColumnName, "Банк получает на счет") != 0) return false;
            if (string.Compare(dataTable.Columns[18].ColumnName, "Банк отдает со счета") != 0) return false;
            return true;
        }

        private bool IsVerifiedOperations(DataTable dataTable)
        {
            if (string.Compare(dataTable.Columns[0].ColumnName, "Дата") != 0) return false;
            if (string.Compare(dataTable.Columns[1].ColumnName, "Документ") != 0) return false;
            if (string.Compare(dataTable.Columns[2].ColumnName, "Номер") != 0) return false;
            if (string.Compare(dataTable.Columns[3].ColumnName, "Сумма") != 0) return false;
            if (string.Compare(dataTable.Columns[4].ColumnName, "Сумма эквивалента") != 0) return false;
            if (string.Compare(dataTable.Columns[5].ColumnName, "Вал") != 0) return false;
            if (string.Compare(dataTable.Columns[6].ColumnName, "Счет Дебет") != 0) return false;
            if (string.Compare(dataTable.Columns[7].ColumnName, "Счет Кредит") != 0) return false;
            if (string.Compare(dataTable.Columns[8].ColumnName, "Состояние") != 0) return false;
            if (string.Compare(dataTable.Columns[9].ColumnName, "Дата проводки") != 0) return false;
            if (string.Compare(dataTable.Columns[10].ColumnName, "Назначение платежа") != 0) return false;
            if (string.Compare(dataTable.Columns[11].ColumnName, "БИК Банка корресп#") != 0) return false;
            if (string.Compare(dataTable.Columns[12].ColumnName, "Дата создания") != 0) return false;
            if (string.Compare(dataTable.Columns[13].ColumnName, "Счет плательщика") != 0) return false;
            if (string.Compare(dataTable.Columns[14].ColumnName, "Счет получателя") != 0) return false;
            if (string.Compare(dataTable.Columns[15].ColumnName, "Плательщик") != 0) return false;
            if (string.Compare(dataTable.Columns[16].ColumnName, "Получатель") != 0) return false;
            return true;
        }

        private bool IsVerifiedTurnovers(DataTable dataTable)
        {
            if (string.Compare(dataTable.Columns[0].ColumnName, "№ счета") != 0) return false;
            if (string.Compare(dataTable.Columns[1].ColumnName, "Валюта") != 0) return false;
            if (string.Compare(dataTable.Columns[2].ColumnName, "Клиент") != 0) return false;
            if (string.Compare(dataTable.Columns[3].ColumnName, "Входящее сальдо") != 0) return false;
            if (string.Compare(dataTable.Columns[4].ColumnName, "Оборот Дебет") != 0) return false;
            if (string.Compare(dataTable.Columns[5].ColumnName, "Оборот Кредит") != 0) return false;
            if (string.Compare(dataTable.Columns[6].ColumnName, "Исходящее сальдо") != 0) return false;
            if (string.Compare(dataTable.Columns[7].ColumnName, "Средний остаток") != 0) return false;
            if (string.Compare(dataTable.Columns[8].ColumnName, "Средний остаток НП") != 0) return false;
            if (string.Compare(dataTable.Columns[9].ColumnName, "Подразделение") != 0) return false;
            return true;
        }

        private bool IsVerifiedVisList(DataTable dataTable)
        {
            if (string.Compare(dataTable.Columns[0].ColumnName, "клиент") != 0) return false;
            if (string.Compare(dataTable.Columns[1].ColumnName, "счет") != 0) return false;
            if (string.Compare(dataTable.Columns[2].ColumnName, "баланс") != 0) return false;
            if (string.Compare(dataTable.Columns[3].ColumnName, "вал") != 0) return false;
            if (string.Compare(dataTable.Columns[4].ColumnName, "Ключ") != 0) return false;
            if (string.Compare(dataTable.Columns[5].ColumnName, "Номер") != 0) return false;
            if (string.Compare(dataTable.Columns[6].ColumnName, "название") != 0) return false;
            if (string.Compare(dataTable.Columns[7].ColumnName, "Группа") != 0) return false;
            if (string.Compare(dataTable.Columns[8].ColumnName, "Привлечение") != 0) return false;
            if (string.Compare(dataTable.Columns[9].ColumnName, "Рекомендация") != 0) return false;
            if (string.Compare(dataTable.Columns[10].ColumnName, "Офис") != 0) return false;
            if (string.Compare(dataTable.Columns[11].ColumnName, "дата открытия") != 0) return false;
            if (string.Compare(dataTable.Columns[12].ColumnName, "дата закрытия") != 0) return false;
            if (string.Compare(dataTable.Columns[13].ColumnName, "дата последней проводки") != 0) return false;
            if (string.Compare(dataTable.Columns[14].ColumnName, "ИНН") != 0) return false;
            if (string.Compare(dataTable.Columns[15].ColumnName, "Комментарий") != 0) return false;
            return true;
        }
    }
}
