using System;
using RMS.DATA;
using RMS.UI.Commands;
using RMS.UI.DialogBoxes;

namespace RMS.UI.ViewModels
{
    public class SqlViewModel : BaseViewModel, IPageViewModel
    {
        public event EventHandler<EventArgs<int>>? ViewChanged;

        public SqlViewModel(DbContext context, IDialogService dialogService, int PageId = 8)
        {
            Title = "SQL";
            this.PageId = PageId;
            this.dialogService = dialogService;
            this.context = context;
        }

        #region Fields
        private readonly DbContext context;
        private readonly IDialogService dialogService;
        #endregion

        #region Properties
        public int PageId { get; set; }
        public string Title { get; set; }
        #endregion

        #region Commands

        #endregion

        #region Methods

        #endregion
    }
}
