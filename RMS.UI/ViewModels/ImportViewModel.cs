using System;
using System.Data;
using System.Windows.Input;
using RMS.UI.Commands;
using RMS.UI.DialogBoxes;
using RMS.UI.Services;

namespace RMS.UI.ViewModels
{
    public class ImportViewModel : IPageViewModel
    {
        public event EventHandler<EventArgs<int>>? ViewChanged;
        public ImportViewModel(IDialogService dialogService, IVisList visList, int pageIndex = 4)
        {
            PageId = pageIndex;
            Title = "Import data";
            this.visList = visList;
            this.dialogService = dialogService;
        }

        #region Fields
        private readonly IVisList visList;
        private readonly IDialogService dialogService;
        private ICommand? importVislist;
        #endregion

        #region Properties
        public int PageId { get; set; }
        public string Title { get; set; }
        #endregion

        #region Methods

        #endregion

        #region Commands
        /// <summary>команда импотра данных из виз листа</summary> 
        public ICommand ImportVislist
        {
            get
            {
                return importVislist ??
                    (importVislist = new RelayCommand(obj => {
                        bool isChoice = dialogService.OpenFileDialog(FilterFile.Excel);
                        if (isChoice)
                        {
                            string path = dialogService.FilePath;
                            string[] sl = visList.GetSheets(path);
                            DataTable dt = visList.Read(sl[1], path);
                        }
                    }));
            }
        }
        #endregion
    }
}
