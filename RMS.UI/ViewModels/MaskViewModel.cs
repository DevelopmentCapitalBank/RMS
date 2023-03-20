using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.DATA;
using RMS.UI.Commands;
using RMS.UI.DialogBoxes;

namespace RMS.UI.ViewModels
{
    public class MaskViewModel : BaseViewModel, IPageViewModel
    {
        public event EventHandler<EventArgs<int>>? ViewChanged;

        public MaskViewModel(IDialogService dialogService, DbContext context, int pageIndex = 4)
        {
            this.dialogService = dialogService;
            this.context = context;
            Title = "Маски";
            PageId = 3;
        }

        #region Fields
        private readonly IDialogService dialogService;
        private readonly DbContext context;
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
