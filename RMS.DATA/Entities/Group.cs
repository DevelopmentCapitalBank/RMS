namespace RMS.DATA.Entities
{
    public class Group
    {
        public int GroupId { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string? Comment { get; set; }
        public IEnumerable<Company>? Companies { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
