using System;
using RMS.UI.Commands;

namespace RMS.UI.ViewModels
{
    public class ExportViewModel : IPageViewModel
    {
        public int PageId { get; set; }
        public string Title { get; set; }

        public event EventHandler<EventArgs<int>>? ViewChanged;

        public ExportViewModel( int pageIndex = 0 )
        {
            PageId = pageIndex;
            Title = "Export data";
        }
    }
}
