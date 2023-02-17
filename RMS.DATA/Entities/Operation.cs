namespace RMS.DATA.Entities
{
    /// <summary>
    /// Операции 
    /// данные из выгрузки цфт по платежным операциям,
    /// выгружается ежемесячно
    /// </summary>
    public class Operation
    {
        /// <summary>
        /// дата выгрузки (на начало месяца)
        /// </summary>
        public DateTime DateOfUnloading { get; set; }
        public decimal Amount { get; set; } = decimal.Zero;
        public decimal AmountEquivalent { get; set; } = decimal.Zero;
        public string Purpose { get; set; } = string.Empty;
        public string Payer { get; set; } = string.Empty;
        public string Recipient { get; set; } = string.Empty;
        public DateTime DateOperation { get; set; } = new DateTime();
    }
}
