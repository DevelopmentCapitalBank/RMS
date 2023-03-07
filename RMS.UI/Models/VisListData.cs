using System;

namespace RMS.UI.Models
{
    /// <summary>
    /// модель данных из визлиста
    /// </summary>
    public class VisListData
    {
        public int ClientId { get; set; } = 0;
        public string AccountNumber { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string GroupName { get; set; } = string.Empty;
        public bool IsAttraction { get; set; } = false;
        public string? ManagerName { get; set; }
        public string? OfficeName { get; set; }
        public DateTime? DateOpen { get; set; }
        public DateTime? DateClose { get; set; }
        public DateTime? DateTimeLastOperation { get; set; }
        public string Inn { get; set; } = string.Empty;
        public string? Comment { get; set; }
    }
}
