namespace RMS.DATA.Entities
{
    public class Acquiring
    {
        public int AccountId { get; set; } = 0;
        public decimal Comission { get; set; } = decimal.Zero;
        public decimal Tarif { get; set; } = decimal.Zero;
        public decimal WriteOffOther { get; set; } = decimal.Zero;
        public bool IsActive { get; set; } = true;
    }
}
