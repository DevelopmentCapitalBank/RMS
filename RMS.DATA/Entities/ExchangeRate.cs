namespace RMS.DATA.Entities
{
    public class ExchangeRate
    {
        public string Iso { get; set; } = string.Empty;
        public Currency Currency { get; set; } = new Currency();
        public int DateId { get; set; } = 0;
        public DateOp Date { get; set; } = new DateOp();
        public decimal Rate { get; set; } = decimal.Zero;
        public override string ToString()
        {
            return Rate.ToString();
        }
    }
}
