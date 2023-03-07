namespace RMS.DATA.Entities
{
    public class Account
    {
        public int AccountId { get; set; } = 0;
        public int CompanyId { get; set; } = 0;
        public int OfficeId { get; set; } = 0;
        public DateTime? DateOpen { get; set; }
        public DateTime? DateClose { get; set; }
        public DateTime? DateTimeLastOperation { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public override string ToString()
        {
            return AccountNumber;
        }
    }
}
