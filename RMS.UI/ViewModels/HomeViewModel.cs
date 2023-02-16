using RMS.UI.Commands;
using System;

namespace RMS.UI.ViewModels
{
    public class HomeViewModel : IPageViewModel
    {
        public int PageId { get; set; }
        public string Title { get; set; }

        public event EventHandler<EventArgs<int>>? ViewChanged;

        public HomeViewModel(int pageIndex = 0)
        {
            PageId = pageIndex;
            Title = "View 1";
        }
    }
}
