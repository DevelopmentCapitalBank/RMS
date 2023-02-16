using System;
using RMS.UI.Commands;

namespace RMS.UI.ViewModels
{
    public class GroupViewModel : IPageViewModel
    {
        public int PageId { get; set; }
        public string Title { get; set; }

        public event EventHandler<EventArgs<int>>? ViewChanged;

        public GroupViewModel( int pageIndex = 0 )
        {
            PageId = pageIndex;
            Title = "Groups";
        }
    }
}
