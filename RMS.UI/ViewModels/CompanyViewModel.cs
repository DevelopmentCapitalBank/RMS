using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using RMS.DATA;
using RMS.DATA.Entities;
using RMS.DATA.Views;
using RMS.UI.Commands;
using RMS.UI.DialogBoxes;

namespace RMS.UI.ViewModels
{
    public class CompanyViewModel : BaseViewModel, IPageViewModel
    {
        public CompanyViewModel(DbContext context, IDialogService dialogService, int pageIndex = 2)
        {
            PageId = pageIndex;
            Title = "Компании";
            this.context = context;
            this.dialogService = dialogService;
        }

        public event EventHandler<EventArgs<int>>? ViewChanged;

        #region Fields
        private readonly DbContext context;
        private readonly IDialogService dialogService;
        private CompanyView search = new();
        private CompanyView selectedCompanyView = new();
        private ObservableCollection<CompanyView>? companies;
        private ObservableCollection<Group>? groups;
        private ObservableCollection<Manager>? managers;
        private ObservableCollection<Office>? offices;
        private Company? selectedCompany;
        private Company? newCompany;
        private ObservableCollection<Account>? accounts;
        private Account? selectedAccount;
        private Account? newAccount;
        private ICommand? searchCompany;
        private ICommand? removeCompany;
        private ICommand? saveCompany;
        private ICommand? showAccounts;
        private ICommand? showPopupCreateCompany;
        private ICommand? insertCompany;
        private ICommand? cancelInsertCompany;
        private ICommand? showPopupCreateAccount;
        private ICommand? insertAccount;
        private ICommand? removeAccount;
        private ICommand? cancelInsertAccount;
        private bool isShowCreateAccount = false;
        private bool isShowCreateCompany = false;
        #endregion

        #region Properties
        public int PageId { get; set; }
        public string Title { get; set; }
        public CompanyView Search
        {
            get { return search; }
            set
            {
                if (value == search)
                {
                    return;
                }
                search = value;
                OnPropertyChanged(nameof(Search));
            }
        }
        public CompanyView SelectedCompanyView
        {
            get { return selectedCompanyView; }
            set
            {
                if (value == selectedCompanyView || value == null)
                {
                    return;
                }
                selectedCompanyView = value;
                LoadCompanyData(selectedCompanyView.CompanyId);
                OnPropertyChanged(nameof(SelectedCompanyView));
            }
        }
        public ObservableCollection<CompanyView>? Companies
        {
            get { return companies; }
            set
            {
                if (value == companies)
                {
                    return;
                }
                companies = value;
                OnPropertyChanged(nameof(Companies));
            }
        }
        public ObservableCollection<Group>? Groups
        {
            get { return groups; }
            set
            {
                if (value == groups)
                {
                    return;
                }
                groups = value;
                OnPropertyChanged(nameof(Groups));
            }
        }
        public ObservableCollection<Manager>? Managers
        {
            get { return managers; }
            set
            {
                if (value == managers)
                {
                    return;
                }
                managers = value;
                OnPropertyChanged(nameof(Managers));
            }
        }
        public ObservableCollection<Office>? Offices
        {
            get { return offices; }
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
        public Company? SelectedCompany
        {
            get { return selectedCompany; }
            set
            {
                if (value == selectedCompany)
                {
                    return;
                }
                selectedCompany = value;
                OnPropertyChanged(nameof(SelectedCompany));
            }
        }
        public Company? NewCompany
        {
            get { return newCompany; }
            set
            {
                if (value == newCompany)
                {
                    return;
                }
                newCompany = value;
                OnPropertyChanged(nameof(NewCompany));
            }
        }
        public ObservableCollection<Account>? Accounts
        {
            get { return accounts; }
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
            get { return selectedAccount; }
            set
            {
                if (value == selectedAccount)
                {
                    return;
                }
                selectedAccount = value;
                OnPropertyChanged(nameof(SelectedAccount));
            }
        }
        public Account? NewAccount
        {
            get { return newAccount; }
            set
            {
                if (value == newAccount)
                {
                    return;
                }
                newAccount = value;
                OnPropertyChanged(nameof(NewAccount));
            }
        }
        public bool IsShowCreateAccount
        {
            get => isShowCreateAccount;
            set
            {
                if (IsShowCreateAccount == value)
                {
                    return;
                }
                isShowCreateAccount = value;
                OnPropertyChanged(nameof(IsShowCreateAccount));
            }
        }
        public bool IsShowCreateCompany
        {
            get => isShowCreateCompany;
            set
            {
                if (isShowCreateCompany == value)
                {
                    return;
                }
                isShowCreateCompany = value;
                OnPropertyChanged(nameof(IsShowCreateCompany));
            }
        }
        #endregion

        #region Methods
        private async void LoadCompanyData(int CompanyId)
        {
            try
            {
                if (Accounts != null)
                {
                    Accounts.Clear();
                }
                SelectedAccount = new();

                var gs = await context.Groups.ReadAllAsync().ConfigureAwait(false);
                Groups = new ObservableCollection<Group>(gs);

                var ms = await context.Managers.ReadAllAsync().ConfigureAwait(false);
                Managers = new ObservableCollection<Manager>(ms);

                SelectedCompany = await context.Companies.ReadByIdAsync(CompanyId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await dialogService.ShowMsgOk($"Ошибка чтения данных компании!\n{ex.Message}").ConfigureAwait(false);
            }
        }
        #endregion

        #region Commands        
        public ICommand SearchCompany
        {
            get
            {
                if (searchCompany == null)
                {
                    searchCompany = new RelayCommand(ExecuteSearchCompany, CanExecuteSearchCompany);
                }
                return searchCompany;
            }
        }
        public async void ExecuteSearchCompany(object parameter)
        {
            try
            {
                var comps = await context.ViewCompanies.FindAsync(Search);
                Companies = new ObservableCollection<CompanyView>(comps);
                Search = new CompanyView();
            }
            catch (Exception ex)
            {
                Companies = new ObservableCollection<CompanyView>();
                await dialogService.ShowMsgOk($"Ошибка поиска компании!\n{ex.Message}").ConfigureAwait(false);
            }
            finally
            {
                SelectedCompany = new();
                SelectedCompanyView = new();
                SelectedAccount = new();
                if (Accounts != null)
                {
                    Accounts.Clear();
                }
            }
        }
        public bool CanExecuteSearchCompany(object parameter)
        {
            if (parameter == null)
            {
                return false;
            }

            if (parameter is CompanyView c)
            {
                return c.CompanyId > 0 || !string.IsNullOrEmpty(c.CompanyName) || !string.IsNullOrEmpty(c.Inn);
            }
            return false;
        }

        public ICommand RemoveCompany
        {
            get
            {
                if (removeCompany == null)
                {
                    removeCompany = new RelayCommand(ExecuteRemoveCompany, CanExecuteRemoveCompany);
                }
                return removeCompany;
            }
        }
        public async void ExecuteRemoveCompany(object parameter)
        {
            try
            {
                bool isDelete = await dialogService.ShowMsgYesNo("Удалить выбранную компанию?\nВсе счета связанные с этой\nкомпанией также будут удалены.").ConfigureAwait(false);
                if (isDelete)
                {
                    await context.Companies.DeleteAsync(SelectedCompany);
                }
            }
            catch (Exception ex)
            {
                await dialogService.ShowMsgOk($"Ошибка удаления компании!\n{ex.Message}").ConfigureAwait(false);
            }
            finally
            {
                Search = new();
                SelectedCompany = new();
                SelectedCompanyView = new();
                SelectedAccount = new();
                if (Accounts != null)
                {
                    Accounts.Clear();
                }
                if (Companies != null)
                {
                    Companies.Clear();
                }
            }
        }
        public bool CanExecuteRemoveCompany(object parameter)
        {
            return parameter != null;
        }

        public ICommand SaveCompany
        {
            get
            {
                if (saveCompany == null)
                {
                    saveCompany = new RelayCommand(ExecuteSaveCompany, CanExecuteSaveCompany);
                }
                return saveCompany;
            }
        }
        public async void ExecuteSaveCompany(object parameter)
        {
            try
            {
                await context.Companies.UpdateAsync(SelectedCompany);
            }
            catch (Exception ex)
            {
                await dialogService.ShowMsgOk($"Ошибка сохранения данных компании!\n{ex.Message}").ConfigureAwait(false);
            }
        }
        public bool CanExecuteSaveCompany(object parameter)
        {
            return parameter != null;
        }

        public ICommand ShowAccounts
        {
            get
            {
                if (showAccounts == null)
                {
                    showAccounts = new RelayCommand(ExecuteShowAccounts, CanExecuteShowAccounts);
                }
                return showAccounts;
            }
        }
        public async void ExecuteShowAccounts(object parameter)
        {
            try
            {
                var ac = await context.Accounts.ReadListByIdAsync(SelectedCompany.CompanyId);
                Accounts = new ObservableCollection<Account>(ac);
            }
            catch (Exception ex)
            {
                Accounts = new ObservableCollection<Account>();
                await dialogService.ShowMsgOk($"Ошибка чтения счетов в компании!\n{ex.Message}").ConfigureAwait(false);
            }
            finally
            {
                SelectedAccount = new();
            }
        }
        public bool CanExecuteShowAccounts(object parameter)
        {
            return parameter != null;
        }
        
        public ICommand ShowPopupCreateCompany
        {
            get
            {
                return showPopupCreateCompany ??= new RelayCommand(async x => {
                    try
                    {
                        IsShowCreateCompany = true;
                        IsShowCreateAccount = false;

                        NewCompany = new Company {
                            ManagerId = 1,
                            GroupId = 1,
                            IsActive = true,
                            IsAttraction = false
                        };

                        if (Groups == null)
                        {
                            var gs = await context.Groups.ReadAllAsync().ConfigureAwait(false);
                            Groups = new ObservableCollection<Group>(gs);
                        }
                        if (Managers == null)
                        {
                            var ms = await context.Managers.ReadAllAsync().ConfigureAwait(false);
                            Managers = new ObservableCollection<Manager>(ms);
                        }
                    }
                    catch (Exception ex)
                    {
                        await dialogService.ShowMsgOk($"Ошибка создания пустой записи компании!\n{ex.Message}").ConfigureAwait(false);
                    }
                });
            }
        }

        public ICommand InsertCompany
        {
            get
            {
                if (insertCompany == null)
                {
                    insertCompany = new RelayCommand(ExecuteInsertCompany, CanExecuteInsertCompany);
                }
                return insertCompany;
            }
        }
        public async void ExecuteInsertCompany(object parameter)
        {
            try
            {
                SelectedCompany = await context.Companies.CreateAsync(NewCompany);
                Search = new();
                SelectedAccount = new();
                if (Accounts != null)
                {
                    Accounts.Clear();
                }
                if (Companies != null)
                {
                    Companies.Clear();
                }
            }
            catch (Exception ex)
            {
                await dialogService.ShowMsgOk($"Ошибка при создании новой компании!\n{ex.Message}").ConfigureAwait(false);
            }
            finally
            {
                IsShowCreateCompany = false;
                IsShowCreateAccount = false;
            }
        }
        public bool CanExecuteInsertCompany(object parameter)
        {
            if (parameter is Company c)
            {
                return c.Inn.Length >= 6 && c.Inn.Length <= 12 
                    && c.Name.Length >= 3 && c.Name.Length <= 255
                    && c.CompanyId > 0;
            }
            return false;
        }

        public ICommand CancelInsertCompany
        {
            get
            {
                return cancelInsertCompany ??= new RelayCommand(x => {
                    IsShowCreateCompany = false;
                    IsShowCreateAccount = false;
                    NewCompany = new();
                });
            }
        }

        public ICommand ShowPopupCreateAccount
        {
            get
            {
                if (showPopupCreateAccount == null)
                {
                    showPopupCreateAccount = new RelayCommand(ExecuteShowPopupCreateAccount, CanExecuteShowPopupCreateAccount);
                }
                return showPopupCreateAccount;
            }
        }
        public async void ExecuteShowPopupCreateAccount(object parameter)
        {
            try
            {
                var offcs = await context.Offices.ReadAllAsync().ConfigureAwait(false);
                Offices = new ObservableCollection<Office>(offcs);
                IsShowCreateCompany = false;
                IsShowCreateAccount = true;
                NewAccount = new Account
                {
                    CompanyId = selectedCompany.CompanyId,
                    OfficeId = 1,
                    DateOpen = DateTime.Now
                };
            }
            catch (Exception ex)
            {
                await dialogService.ShowMsgOk($"Ошибка создания пустой записи счета!\n{ex.Message}").ConfigureAwait(false);
            }
        }
        public bool CanExecuteShowPopupCreateAccount(object parameter)
        {
            return parameter != null;
        }

        public ICommand InsertAccount
        {
            get
            {
                if (insertAccount == null)
                {
                    insertAccount = new RelayCommand(ExecuteInsertAccount, CanExecuteInsertAccount);
                }
                return insertAccount;
            }
        }
        public async void ExecuteInsertAccount(object parameter)
        {
            try
            {
                NewAccount = await context.Accounts.CreateAsync(NewAccount).ConfigureAwait(false);
                if (Accounts == null)
                {
                    Accounts = new ObservableCollection<Account>();
                }
                Accounts.Add(NewAccount);
            }
            catch (Exception ex)
            {
                await dialogService.ShowMsgOk($"Ошибка добавления нового счета!\n{ex.Message}").ConfigureAwait(false);
            }
            finally
            {
                IsShowCreateCompany = false;
                IsShowCreateAccount = false;
            }
        }
        public bool CanExecuteInsertAccount(object parameter)
        {
            if (parameter == null)
            {
                return false; 
            }
            if (parameter is Account ac)
            {
                if (ac.AccountNumber.Length == 20)
                {
                    return true;
                }
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
                bool isDeleteAccount = await dialogService.ShowMsgYesNo("Удалить счет?").ConfigureAwait(false);
                if (isDeleteAccount)
                {
                    await context.Accounts.DeleteAsync(SelectedAccount).ConfigureAwait(false);
                    SelectedAccount = new();
                    Accounts = new ObservableCollection<Account>();
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

        public ICommand CancelInsertAccount
        {
            get
            {
                return cancelInsertAccount ??= new RelayCommand(async x => {
                    try
                    {
                        IsShowCreateCompany = false;
                        IsShowCreateAccount = false;
                        NewAccount = new();
                    }
                    catch (Exception ex)
                    {
                        await dialogService.ShowMsgOk($"Ошибка!\n{ex.Message}").ConfigureAwait(false);
                    }
                });
            }
        }
        #endregion
    }
}
