﻿using System;
using System.Windows.Input;
using RMS.DATA;
using RMS.UI.Commands;
using RMS.UI.DialogBoxes;
using RMS.UI.Services;

namespace RMS.UI.ViewModels
{
    public class ExchangeRatesViewModel : BaseViewModel, IPageViewModel
    {
        public event EventHandler<EventArgs<int>>? ViewChanged;

        public ExchangeRatesViewModel(DbContext context, IDialogService dialogService, ICurrencyRateParser parser, int PageId = 7)
        {
            Title = "Курсы валют";
            this.PageId = PageId;
            this.dialogService = dialogService;
            this.parser = parser;
            this.context = context;
        }

        #region Fields
        private readonly DbContext context;
        private readonly IDialogService dialogService;
        private readonly ICurrencyRateParser parser;
        private ICommand? getCourse;
        #endregion

        #region Properties
        public int PageId { get; set; }
        public string Title { get; set; }

        #endregion

        #region Commands
        public ICommand GetCourse
        {
            get
            {
                return getCourse ??= new RelayCommand(async x => {
                    _ = await parser.GetCourseAsync(DateTime.Now, new DateTime());
                });
            }
        }
        #endregion

        #region Methods

        #endregion
    }
}
