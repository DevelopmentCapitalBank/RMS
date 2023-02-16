using System;
using RMS.UI.Commands;

namespace RMS.UI.ViewModels
{
    public class CompanyViewModel : IPageViewModel
    {
        public int PageId { get; set; }
        public string Title { get; set; }

        public event EventHandler<EventArgs<int>>? ViewChanged;

        public CompanyViewModel( int pageIndex = 0 )
        {
            PageId = pageIndex;
            Title = "Companies";
        }
    }
}
