namespace RMS.DATA.Entities
{
    public class Office
    {
        public int OfficeId { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public override string ToString()
        {
            return Name;
        }
    }
}
