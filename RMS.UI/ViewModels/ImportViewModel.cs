using System;
using RMS.UI.Commands;

namespace RMS.UI.ViewModels
{
    public class ImportViewModel : IPageViewModel
    {
        public int PageId { get; set; }
        public string Title { get; set; }

        public event EventHandler<EventArgs<int>>? ViewChanged;

        public ImportViewModel( int pageIndex = 4 )
        {
            PageId = pageIndex;
            Title = "Import data";
        }
    }
}
