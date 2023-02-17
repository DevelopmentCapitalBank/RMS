namespace RMS.DATA.Entities
{
    public class DateOp
    {
        public int DateId { get; set; } = 0;
        public DateTime DateOperation { get; set; } = new DateTime();
        public override string ToString()
        {
            return DateOperation.ToString();
        }
    }
}
