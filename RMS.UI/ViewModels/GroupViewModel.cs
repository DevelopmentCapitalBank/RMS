using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using RMS.DATA;
using RMS.DATA.Entities;
using RMS.UI.Commands;
using RMS.UI.DialogBoxes;

namespace RMS.UI.ViewModels
{
    public class GroupViewModel : BaseViewModel, IPageViewModel
    {
        public GroupViewModel(DbContext context, IDialogService dialogService, int pageIndex = 1)
        {
            PageId = pageIndex;
            Title = "Группы";
            this.context = context;
            this.dialogService = dialogService;
            OnLoad();
        }

        public event EventHandler<EventArgs<int>>? ViewChanged;

        #region Fields
        private readonly DbContext context;
        private readonly IDialogService dialogService;
        private ObservableCollection<Group>? groups;
        private ObservableCollection<Company>? companies;
        private Group? selectedGroup;
        private ICommand? createGroup;
        private ICommand? removeGroup;
        private ICommand? saveGroup;
        private ICommand? showCompanies;
        private ICommand? refreshGroup;
        #endregion

        #region Properties
        public int PageId { get; set; }
        public string Title { get; set; }
        public ObservableCollection<Group>? Groups
        {
            get => groups;
            set
            {
                if (groups == value)
                {
                    return;
                }
                groups = value;
                OnPropertyChanged(nameof(Groups));
            }
        }
        public ObservableCollection<Company>? Companies
        {
            get => companies;
            set
            {
                if (companies == value)
                {
                    return;
                }
                companies = value;
                OnPropertyChanged(nameof(Companies));
            }
        }
        public Group? SelectedGroup
        {
            get => selectedGroup;
            set
            {
                if (selectedGroup == value)
                {
                    return;
                }
                if (Companies != null)
                {
                    Companies.Clear();
                }
                selectedGroup = value;
                OnPropertyChanged(nameof(SelectedGroup));
            }
        }
        #endregion

        #region Methods
        private async void OnLoad()
        {
            try
            {
                var grps = await context.Groups.ReadAllAsync();
                Groups = new ObservableCollection<Group>(grps);
            }
            catch (Exception ex)
            {
                dialogService.ShowMsg($"Ошибка чтения списка всех групп!\n{ex.Message}");
            }
        }
        #endregion

        #region Commands
        public ICommand RefreshGroup
        {
            get
            {
                return refreshGroup ??= new RelayCommand(x => {
                    OnLoad();
                });
            }
        }

        public ICommand CreateGroup
        {
            get
            {
                return createGroup ??= new RelayCommand(async x => {
                    var newGroup = new Group { Name = "НОВАЯ ГРУППА", Comment = "Комментарий" };
                    await context.Groups.CreateAsync(newGroup);
                    OnLoad();
                });
            }
        }

        public ICommand RemoveGroup
        {
            get
            {
                if (removeGroup == null)
                {
                    removeGroup = new RelayCommand(ExecuteRemoveGroup, CanExecuteRemoveGroup);
                }
                return removeGroup;
            }
        }
        public async void ExecuteRemoveGroup(object parameter)
        {
            try
            {
                bool isYes = await dialogService.ShowMsgYesNo("Удалить группу? \nВсе компании закрепленные за данной группой\nбудут перенесены в группу ПУСТО.").ConfigureAwait(false);

                if (isYes)
                {
                    await context.Groups.DeleteAsync(SelectedGroup);
                    OnLoad();
                }
            }
            catch(Exception ex)
            {
                dialogService.ShowMsg($"Ошибка удаления группы!\n{ex.Message}");
            }
        }
        public bool CanExecuteRemoveGroup(object parameter)
        {
            if (parameter == null)
            {
                return false;
            }

            if (parameter is Group group)
            {
                return group.GroupId != 1;
            }
            return false;
        }

        public ICommand SaveGroup
        {
            get
            {
                if (saveGroup == null)
                {
                    saveGroup = new RelayCommand(ExecuteSaveGroup, CanExecuteSaveGroup);
                }
                return saveGroup;
            }
        }
        public async void ExecuteSaveGroup(object parameter)
        {
            try
            {
                await context.Groups.UpdateAsync(SelectedGroup);
                OnLoad();
            }
            catch (Exception ex)
            {
                dialogService.ShowMsg($"Ошибка сохранения изменений в группе!\n{ex.Message}");
            }
        }
        public bool CanExecuteSaveGroup(object parameter)
        {
            if (parameter == null)
            {
                return false;
            }

            if (parameter is Group group)
            {
                return group.GroupId != 1;
            }
            return false;
        }

        public ICommand ShowCompanies
        {
            get
            {
                if (showCompanies == null)
                {
                    showCompanies = new RelayCommand(ExecuteShowCompanies, CanExecuteShowCompanies);
                }
                return showCompanies;
            }
        }
        public async void ExecuteShowCompanies(object parameter)
        {
            try
            {
                var comps = await context.Companies.ReadListByIdAsync(SelectedGroup.GroupId);
                Companies = new ObservableCollection<Company>(comps);
            }
            catch (Exception ex)
            {
                dialogService.ShowMsg($"Ошибка чтения списка компаний в группе!\n{ex.Message}");
            }
        }
        public bool CanExecuteShowCompanies(object parameter)
        {
            if (parameter == null)
            {
                return false;
            }

            if (parameter is Group group)
            {
                return group.GroupId != 1;
            }
            return false;
        }
        #endregion        
    }
}
