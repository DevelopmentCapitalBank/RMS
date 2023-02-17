namespace RMS.DATA.Entities
{
    public class Account
    {
        public int AccountId { get; set; } = 0;
        public int CompanyId { get; set; } = 0;
        public Company Company { get; set; } = new Company();
        public int OfficeId { get; set; } = 0;
        public Office Office { get; set; } = new Office();
        public Acquiring? Acquiring { get; set; }
        public DateTime DateOpen { get; set; } = new DateTime();
        public DateTime? DateClose { get; set; }
        public DateTime? DateTimeLastOperation { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public override string ToString()
        {
            return AccountNumber;
        }
    }
}
