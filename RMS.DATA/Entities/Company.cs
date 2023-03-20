namespace RMS.DATA.Entities
{
    public class Company
    {
        public int CompanyId { get; set; }
        public int ManagerId { get; set; } = 0;
        public int GroupId { get; set; } = 0;
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
        public override bool Equals(object? obj)
        {
            if(obj is Company c)
            {
                return c.CompanyId == CompanyId &&
                    c.ManagerId == ManagerId &&
                    c.GroupId == GroupId &&
                    c.IsAttraction == IsAttraction &&
                    string.Equals(c.Comment, Comment);
            }
            return false;
        }
        public override int GetHashCode() => CompanyId.GetHashCode();
    }
}
