using System;

namespace RMS.UI.Services
{
    public class DataModel : IDataModel
    {
        public string Data { get; set; }

        public string? Reverse()
        {
            char[] charArray = Data.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public DataModel() { Data = ""; }

        public DataModel(string data) { Data = data; }
    }
}
