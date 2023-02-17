namespace RMS.DATA.Entities
{
    public class Company
    {
        public int CompanyId { get; set; }
        public int ManagerId { get; set; } = 0;
        public Manager Manager { get; set; } = new Manager();
        public int GroupId { get; set; } = 0;
        public Group Group { get; set; } = new Group();
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; } = false;
        public bool IsAttraction { get; set; } = false;
        public string Inn { get; set; } = string.Empty;
        public string? Comment { get; set; }
        public IEnumerable<Account>? Accounts { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
