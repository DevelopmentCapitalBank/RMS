﻿using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using RMS.DATA;
using RMS.DATA.Entities;
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
        private string searchAccountText = "";
        private ObservableCollection<Account>? accounts;
        private Account? selectedAccount;
        private Company? company;
        private ObservableCollection<Office>? offices;
        private ICommand? searchAccount;
        private ICommand? removeAccount;
        private ICommand? saveAccount;
        #endregion

        #region Properties
        public int PageId { get; set; }
        public string Title { get; set; }
        public string SearchAccountText
        {
            get => searchAccountText;
            set
            {
                if (string.Compare(searchAccountText, value) == 0)
                {
                    return;
                }
                searchAccountText = value;
                OnPropertyChanged(nameof(SearchAccountText));
            }
        }
        public ObservableCollection<Account>? Accounts
        {
            get => accounts;
            set
            {
                if (value == accounts)
                {
                    return;
                }
                accounts = value;
                OnPropertyChanged(nameof(Accounts));
            }
        }
        public Account? SelectedAccount
        {
            get => selectedAccount;
            set
            {
                if (value == selectedAccount || value == null)
                {
                    return;
                }
                selectedAccount = value;
                LoadAcoountData(selectedAccount);
                OnPropertyChanged(nameof(SelectedAccount));
            }
        }
        public Company? Company
        {
            get => company;
            set
            {
                if (value == company)
                {
                    return;
                }
                company = value;
                OnPropertyChanged(nameof(Company));
            }
        }
        public ObservableCollection<Office>? Offices
        {
            get => offices;
            set
            {
                if (value == offices)
                {
                    return;
                }
                offices = value;
                OnPropertyChanged(nameof(Offices));
            }
        }
        #endregion

        #region Methods
        private async void LoadAcoountData(Account acc)
        {
            try
            {
                var offcs = await context.Offices.ReadAllAsync().ConfigureAwait(false);
                Offices = new ObservableCollection<Office>(offcs);

                Company = await context.Companies.ReadByIdAsync(acc.CompanyId).ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                await dialogService.ShowMsgOk($"Ошибка чтения данных по счету!\n{ex.Message}").ConfigureAwait(false);
            }
        }
        #endregion

        #region Commands
        public ICommand SearchAccount
        {
            get
            {
                if (searchAccount == null)
                {
                    searchAccount = new RelayCommand(ExecuteSearchAccount, CanExecuteSearchAccount);
                }
                return searchAccount;
            }
        }
        public async void ExecuteSearchAccount(object parameter)
        {
            try
            {
                var accs = await context.Accounts.FindAsync(SearchAccountText).ConfigureAwait(false);
                Accounts = new ObservableCollection<Account>(accs);
            }
            catch (Exception ex)
            {
                await dialogService.ShowMsgOk($"Ошибка поиска счета!\n{ex.Message}").ConfigureAwait(false);
            }
        }
        public bool CanExecuteSearchAccount(object parameter)
        {
            if (parameter == null)
            {
                return false;
            }
            if (parameter is string str)
            {
                return !string.IsNullOrEmpty(str);
            }
            return false;
        }

        public ICommand RemoveAccount
        {
            get
            {
                if (removeAccount == null)
                {
                    removeAccount = new RelayCommand(ExecuteRemoveAccount, CanExecuteRemoveAccount);
                }
                return removeAccount;
            }
        }
        public async void ExecuteRemoveAccount(object parameter)
        {
            try
            {
                bool isDelete = await dialogService.ShowMsgYesNo("Удалить счет?").ConfigureAwait(false);
                if (isDelete)
                {
                    await context.Accounts.DeleteAsync(SelectedAccount).ConfigureAwait(false);
                    SelectedAccount = new();
                    if (Accounts != null)
                    {
                        Accounts = new ObservableCollection<Account>();
                    }
                }
            }
            catch (Exception ex)
            {
                await dialogService.ShowMsgOk($"Ошибка удаления счета!\n{ex.Message}").ConfigureAwait(false);
            }
        }
        public bool CanExecuteRemoveAccount(object parameter)
        {
            return parameter != null;
        }

        public ICommand SaveAccount
        {
            get
            {
                if (saveAccount == null)
                {
                    saveAccount = new RelayCommand(ExecuteSaveAccount, CanExecuteSaveAccount);
                }
                return saveAccount;
            }
        }
        public async void ExecuteSaveAccount(object parameter)
        {
            try
            {
                await context.Accounts.UpdateAsync(SelectedAccount).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await dialogService.ShowMsgOk($"Ошибка сохранения данных по счету!\n{ex.Message}").ConfigureAwait(false);
            }
        }
        public bool CanExecuteSaveAccount(object parameter)
        {
            return parameter != null;
        }
        #endregion
    }
}
