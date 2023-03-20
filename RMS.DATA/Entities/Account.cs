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
        public override bool Equals(object? obj)
        {
            if (obj is Account a)
            {
                return string.Equals(a.AccountNumber, AccountNumber) &&
                    a.CompanyId == CompanyId &&
                    a.OfficeId == OfficeId &&
                    a.DateOpen == DateOpen &&
                    a.DateClose == DateClose &&
                    a.DateTimeLastOperation == DateTimeLastOperation;
            }

            return false;
        }
        public override int GetHashCode() => AccountNumber.GetHashCode();
    }
}
