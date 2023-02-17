namespace RMS.DATA.Entities
{
    public class Manager
    {
        public int ManagerId { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public override string ToString()
        {
            return Name;
        }
    }
}
