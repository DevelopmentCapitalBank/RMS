namespace RMS.DATA.Entities
{
    public class Currency
    {
        public string Iso { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public override string ToString()
        {
            return Iso + " | " + Code;
        }
    }
}
