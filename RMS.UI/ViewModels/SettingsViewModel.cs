using System;
using RMS.UI.Commands;

namespace RMS.UI.ViewModels
{
    public class SettingsViewModel : IPageViewModel
    {
        public int PageId { get;  set; }

        public string Title { get; set; }

        public event EventHandler<EventArgs<int>>? ViewChanged;
        public SettingsViewModel(int pageIndex = 6)
        {
            PageId = pageIndex;
            Title = "View 2";
        }
    }
}
