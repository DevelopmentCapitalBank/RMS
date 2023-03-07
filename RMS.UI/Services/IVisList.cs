using System.Collections.Generic;
using RMS.UI.Models;

namespace RMS.UI.Services
{
    public interface IVisList
    {
        IEnumerable<VisListData> Read(string path);
    }
}
