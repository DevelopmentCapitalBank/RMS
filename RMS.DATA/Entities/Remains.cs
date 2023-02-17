namespace RMS.DATA.Entities
{
    /// <summary>
    /// Остатки (либо по депозитным счетам , лобо по расчетным)
    /// данные из выгрузки цфт (средне арифметические остатки по счетам)
    /// выгружается ежемесячно
    /// </summary>
    public class Remains
    {
        /// <summary>
        /// дата выгрузки (на начало месяца)
        /// </summary>
        public DateTime DateOfUnloading { get; set; }
        public string Account { get; set; } = string.Empty;
        public decimal Debit { get; set; } = decimal.Zero;
        public decimal Credit { get; set; } = decimal.Zero;
        public decimal AverageBalance { get; set; } = decimal.Zero;
    }
}
