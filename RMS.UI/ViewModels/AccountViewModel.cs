﻿using System;
using RMS.DATA;
using RMS.UI.Commands;
using RMS.UI.DialogBoxes;

namespace RMS.UI.ViewModels
{
    public class AccountViewModel : BaseViewModel, IPageViewModel
    {
        public event EventHandler<EventArgs<int>>? ViewChanged;

        public AccountViewModel(DbContext context, IDialogService dialogService, int PageId = 3)
        {
            this.PageId = PageId;
            this.context = context;
            this.dialogService = dialogService;
            Title = "Счета";
        }

        #region Fields
        private readonly DbContext context;
        private readonly IDialogService dialogService;
        #endregion

        #region Properties
        public int PageId { get; set; }
        public string Title { get; set; }
        #endregion

    }
}