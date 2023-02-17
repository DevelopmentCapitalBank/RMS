namespace RMS.DATA.Entities
{
    /// <summary>
    /// Конверсия ДБО 
    /// данные из выгрузки цфт по конверсии,
    /// выгружается ежемесячно
    /// </summary>
    public class Conversion
    {
        /// <summary>дата операции по дбо</summary>
        public DateTime DateOperation { get; set; } = new DateTime();
        /// <summary>Вид сделки</summary>
        public string TypeOfTransaction { get; set; } = string.Empty;
        /// <summary>Банк получает сумму</summary>
        public decimal ReceivesAmount { get; set; } = decimal.Zero;
        /// <summary>Валюта получения</summary>
        public string ReceivedCurrency { get; set; } = string.Empty;
        /// <summary>Банк отдает сумму</summary>
        public decimal GivesAmount { get; set; } = decimal.Zero;
        /// <summary>Валюта, в которой банк отдает сумму</summary>
        public string GivesCurrency { get; set; } = string.Empty;
        /// <summary>Курс ЦБ (Валюта зачисления)</summary>
        public decimal RateCurrencyOfCrediting { get; set; } = decimal.Zero;
        /// <summary>Курс ЦБ (Валюта списания)</summary>
        public decimal RateCurrencyOfDebiting { get; set; } = decimal.Zero;
        /// <summary>Банк получает на счет</summary>
        public string ReceivesToAccount { get; set; } = string.Empty;
        /// <summary>Банк отдает со счета</summary>
        public string GivesFromAccount { get; set; } = string.Empty;

    }
}
