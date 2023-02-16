using RMS.UI.Commands;
using System;

namespace RMS.UI.ViewModels
{
    public interface IPageViewModel
    {
        event EventHandler<EventArgs<int>>? ViewChanged;
        int PageId { get; set; }
        string Title { get; set; }
    }
}
