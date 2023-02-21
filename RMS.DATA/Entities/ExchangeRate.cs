namespace RMS.DATA.Entities
{
    public class ExchangeRate
    {
        public string Iso { get; set; } = string.Empty;
        public int DateId { get; set; } = 0;
        public decimal Rate { get; set; } = decimal.Zero;
        public override string ToString()
        {
            return Rate.ToString();
        }
    }
}
