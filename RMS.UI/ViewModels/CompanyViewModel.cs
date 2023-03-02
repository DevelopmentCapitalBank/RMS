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
        public CompanyViewModel(DbContext context, IDialogService dialogService, int pageIndex = 0)
        {
            PageId = pageIndex;
            Title = "Companies";
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
        private Company? selectedCompany;
        private ObservableCollection<Account>? accounts;
        private ICommand? searchCompany;
        private ICommand? removeCompany;
        private ICommand? saveCompany;
        private ICommand? showAccounts;
        private ICommand? createCompany;
        private ICommand? insertCompany;
        private string visibilityCompanyIdTextBox = "Collapsed";
        #endregion

        #region Properties
        public int PageId { get; set; }
        public string Title { get; set; }
        public CompanyView Search
        {
            get { return search; }
            set
            {
                if (value == null)
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
                if (value == null)
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
                if (value == null)
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
                if (value == null)
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
                if (value == null)
                {
                    return;
                }
                managers = value;
                OnPropertyChanged(nameof(Managers));
            }
        }
        public Company? SelectedCompany
        {
            get { return selectedCompany; }
            set
            {
                if (value == null)
                {
                    return;
                }
                selectedCompany = value;
                OnPropertyChanged(nameof(SelectedCompany));
            }
        }
        public ObservableCollection<Account>? Accounts
        {
            get { return accounts; }
            set
            {
                if (value == null)
                {
                    return;
                }
                accounts = value;
                OnPropertyChanged(nameof(Accounts));
            }
        }
        public string VisibilityCompanyIdTextBox
        {
            get { return visibilityCompanyIdTextBox; }
            set
            {
                if (value == null)
                {
                    return;
                }
                visibilityCompanyIdTextBox = value;
                OnPropertyChanged(nameof(VisibilityCompanyIdTextBox));
            }
        }
        #endregion

        #region Methods
        private async void LoadCompanyData(int CompanyId)
        {
            var gs = await context.Groups.ReadAllAsync().ConfigureAwait(false);
            Groups = new ObservableCollection<Group>(gs);

            var ms = await context.Managers.ReadAllAsync().ConfigureAwait(false);
            Managers = new ObservableCollection<Manager>(ms);

            SelectedCompany = await context.Companies.ReadByIdAsync(CompanyId).ConfigureAwait(false);
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
            var comps = await context.ViewCompanies.FindAsync(Search);
            Companies = new ObservableCollection<CompanyView>(comps);
            Search = new CompanyView();
            SelectedCompany = new Company();
            SelectedCompanyView = new CompanyView();
            if (Accounts != null)
            {
                Accounts.Clear();
            }
            VisibilityCompanyIdTextBox = "Collapsed";
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
            await context.Companies.DeleteAsync(SelectedCompany);
            VisibilityCompanyIdTextBox = "Collapsed";
            Search = new CompanyView();
            SelectedCompany = new Company();
            SelectedCompanyView = new CompanyView();
            if (Accounts != null)
            {
                Accounts.Clear();
            }
            if (Companies != null)
            {
                Companies.Clear();
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
            await context.Companies.UpdateAsync(SelectedCompany);
            VisibilityCompanyIdTextBox = "Collapsed";
        }
        public bool CanExecuteSaveCompany(object parameter)
        {
            return parameter != null && VisibilityCompanyIdTextBox != "Visible";
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
            var ac = await context.Accounts.ReadListByIdAsync(SelectedCompany.CompanyId);
            Accounts = new ObservableCollection<Account>(ac);
            VisibilityCompanyIdTextBox = "Collapsed";
        }
        public bool CanExecuteShowAccounts(object parameter)
        {
            return parameter != null && VisibilityCompanyIdTextBox != "Visible";
        }
        
        public ICommand CreateCompany
        {
            get
            {
                return createCompany ??= new RelayCommand(async x => {
                    Search = new CompanyView();
                    if (Companies != null)
                    {
                        Companies.Clear();
                    }

                    SelectedCompany = new Company {
                        CompanyId = 77777,
                        Name = "ИМЯ КОМПАНИИ",
                        ManagerId = 1,
                        GroupId = 1,
                        IsActive = true,
                        IsAttraction = false,
                        Inn = "1234567890",
                        Comment = "Комментарий"
                    };

                    var gs = await context.Groups.ReadAllAsync().ConfigureAwait(false);
                    Groups = new ObservableCollection<Group>(gs);

                    var ms = await context.Managers.ReadAllAsync().ConfigureAwait(false);
                    Managers = new ObservableCollection<Manager>(ms);

                    VisibilityCompanyIdTextBox = "Visible";
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
            SelectedCompany = await context.Companies.CreateAsync(SelectedCompany);
            VisibilityCompanyIdTextBox = "Collapsed";
        }
        public bool CanExecuteInsertCompany(object parameter)
        {
            return parameter != null && VisibilityCompanyIdTextBox != "Collapsed";
        }
        #endregion
        //TODO реализовать обработку исключений с выводом сообщения пользователю
    }
}
