using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using RMS.DATA;
using RMS.DATA.Entities;
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
            Title = "Маски для поиска и анализа выгрузок";
            PageId = 3;
            OnLoad();
        }

        #region Fields
        private readonly IDialogService dialogService;
        private readonly DbContext context;
        private ObservableCollection<MaskType> maskTypes = new();
        private MaskType? selectedMaskType;
        private bool isShowCreateMaskType = false;
        private MaskType? newMaskType;
        private ObservableCollection<Mask>? masks;
        private Mask? selectedMask;
        private bool isShowCreateMask = false;
        private Mask? newMask;
        private ICommand? showPopupCreateMaskType;
        private ICommand? insertMaskType;
        private ICommand? cancelInsertMaskType;
        private ICommand? removeMaskType;
        private ICommand? saveMaskType;
        private ICommand? showPopupCreateMask;
        private ICommand? insertMask;
        private ICommand? cancelInsertMask;
        private ICommand? removeMask;
        #endregion

        #region Properties
        public int PageId { get; set; }
        public string Title { get; set; }
        public ObservableCollection<MaskType> MaskTypes
        {
            get => maskTypes;
            set
            {
                if (maskTypes == value)
                {
                    return;
                }
                maskTypes = value;
                OnPropertyChanged(nameof(MaskTypes));
            }
        }
        public MaskType? SelectedMaskType
        {
            get => selectedMaskType;
            set
            {
                if (selectedMaskType == value || value == null)
                {
                    return;
                }
                selectedMaskType = value;
                LoadMasks(selectedMaskType.MaskTypeId);
                OnPropertyChanged(nameof(SelectedMaskType));
            }
        }
        public bool IsShowCreateMaskType
        {
            get => isShowCreateMaskType;
            set
            {
                if (isShowCreateMaskType == value)
                {
                    return;
                }
                isShowCreateMaskType = value;
                OnPropertyChanged(nameof(IsShowCreateMaskType));
            }
        }
        public MaskType? NewMaskType
        {
            get => newMaskType;
            set
            {
                if (newMaskType == value)
                {
                    return;
                }
                newMaskType = value;
                OnPropertyChanged(nameof(NewMaskType));
            }
        }
        public ObservableCollection<Mask>? Masks
        {
            get => masks;
            set
            {
                if (masks == value)
                {
                    return;
                }
                masks = value;
                OnPropertyChanged(nameof(Masks));
            }
        }
        public Mask? SelectedMask
        {
            get => selectedMask;
            set
            {
                if (selectedMask == value)
                {
                    return;
                }
                selectedMask = value;
                OnPropertyChanged(nameof(SelectedMask));
            }
        }
        public bool IsShowCreateMask
        {
            get => isShowCreateMask;
            set
            {
                if (isShowCreateMask == value)
                {
                    return;
                }
                isShowCreateMask = value;
                OnPropertyChanged(nameof(IsShowCreateMask));
            }
        }
        public Mask? NewMask
        {
            get => newMask;
            set
            {
                if (newMask == value)
                {
                    return;
                }
                newMask = value;
                OnPropertyChanged(nameof(NewMask));
            }
        }
        #endregion

        #region Commands
        public ICommand ShowPopupCreateMaskType
        {
            get
            {
                return showPopupCreateMaskType ??= new RelayCommand(x => {
                    IsShowCreateMaskType = true;
                    NewMaskType = new();
                });
            }
        }

        public ICommand InsertMaskType
        {
            get
            {
                if (insertMaskType == null)
                {
                    insertMaskType = new RelayCommand(ExecuteInsertMaskType, CanExecute);
                }
                return insertMaskType;
            }
        }
        public async void ExecuteInsertMaskType(object parameter)
        {
            try
            {
                var mskt = await context.MaskTypes.CreateAsync(NewMaskType).ConfigureAwait(false);
                MaskTypes.Add(mskt);
                IsShowCreateMaskType = false;
            }
            catch (Exception ex)
            {
                dialogService.ShowMsg($"Ошибка создания вида масок.\n{ ex.Message }");
            }
        }

        public ICommand CancelInsertMaskType
        {
            get
            {
                return cancelInsertMaskType ??= new RelayCommand(x => {
                    IsShowCreateMaskType = false;
                    NewMaskType = null;
                });
            }
        }

        public ICommand RemoveMaskType
        {
            get
            {
                if (removeMaskType == null)
                {
                    removeMaskType = new RelayCommand(ExecuteRemoveMaskType, CanExecute);
                }
                return removeMaskType;
            }
        }
        public async void ExecuteRemoveMaskType(object parameter)
        {
            try
            {
                bool isDelete = await dialogService.ShowMsgYesNo("Удалить выбранный вид масок безвозвратно?").ConfigureAwait(false);
                if (isDelete)
                {
                    await context.MaskTypes.DeleteAsync(SelectedMaskType).ConfigureAwait(false);
                    SelectedMaskType = new();
                    SelectedMask = null;
                    OnLoad();
                }
            }
            catch (Exception ex)
            {
                dialogService?.ShowMsg($"Ошибка удаления вида масок.\n{ ex.Message }");
            }
        }

        public ICommand SaveMaskType
        {
            get
            {
                if (saveMaskType == null)
                {
                    saveMaskType = new RelayCommand(ExecuteSaveMaskType, CanExecuteSaveMaskType);
                }
                return saveMaskType;
            }
        }
        public async void ExecuteSaveMaskType(object parameter)
        {
            try
            {
                await context.MaskTypes.UpdateAsync(SelectedMaskType).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                dialogService.ShowMsg($"Ошибка сохранения изменений в виде масок.\n { ex.Message }");
            }
        }
        public bool CanExecuteSaveMaskType(object parameter)
        {
            if (parameter == null)
            {
                return false;
            }
            if (parameter is MaskType m)
            {
                return !string.IsNullOrEmpty(m.Name);
            }
            return true;
        }

        public ICommand ShowPopupCreateMask
        {
            get
            {
                if (showPopupCreateMask == null)
                {
                    showPopupCreateMask = new RelayCommand(ExecuteShowPopupCreateMask, CanExecuteShowPopupCreateMask);
                }
                return showPopupCreateMask;
            }
        }
        public void ExecuteShowPopupCreateMask(object parameter)
        {
            IsShowCreateMask = true;
            NewMask = new Mask {
                MaskTypeId = SelectedMaskType.MaskTypeId
            };
        }
        public bool CanExecuteShowPopupCreateMask(object parameter)
        {
            if (parameter == null)
            { 
                return false;
            }

            if (parameter is MaskType m)
            {
                return m.MaskTypeId > 0;
            }

            return true;
        }

        public ICommand InsertMask
        {
            get
            {
                if (insertMask == null)
                {
                    insertMask = new RelayCommand(ExecuteInsertMask, CanExecuteInsertMask);
                }
                return insertMask;
            }
        }
        public async void ExecuteInsertMask(object parameter)
        {
            try
            {
                var nm = await context.Masks.CreateAsync(NewMask).ConfigureAwait(false);
                if (Masks != null)
                {
                    Masks.Add(nm);
                }
                IsShowCreateMask = false;
            }
            catch (Exception ex)
            {
                dialogService.ShowMsg($"Ошибка создания маски!\n{ ex.Message }");
            }
        }
        public bool CanExecuteInsertMask(object parameter)
        {
            if (parameter == null)
            {
                return false;
            }
            if (parameter is Mask ms)
            {
                return !string.IsNullOrEmpty(ms.Content) && ms.SequenceNumber > 0;
            }
            return true;
        }

        public ICommand CancelInsertMask
        {
            get
            {
                return cancelInsertMask ??= new RelayCommand(x => {
                    IsShowCreateMask = false;
                    NewMask = null;
                });
            }
        }

        public ICommand RemoveMask
        {
            get
            {
                if (removeMask == null)
                {
                    removeMask = new RelayCommand(ExecuteRemoveMask, CanExecute);
                }
                return removeMask;
            }
        }
        public async void ExecuteRemoveMask(object parameter)
        {
            try
            {
                bool isDelete = await dialogService.ShowMsgYesNo("Удалить выбранную маску безвозвратно?").ConfigureAwait(false);
                if (isDelete)
                {
                    await context.Masks.DeleteAsync(SelectedMask).ConfigureAwait(false);
                    LoadMasks(SelectedMaskType.MaskTypeId);
                }
            }
            catch (Exception ex)
            {
                dialogService.ShowMsg($"Ошибка удаления маски!\n { ex.Message }");
            }
        }
        #endregion

        #region Methods
        private async void OnLoad()
        {
            try
            {
                var msk = await context.MaskTypes.ReadAllAsync();
                MaskTypes = new ObservableCollection<MaskType>(msk);
            }
            catch (Exception ex)
            {
                dialogService.ShowMsg($"Ошибка чтения списка всех групп!\n{ex.Message}");
            }
        }
        private async void LoadMasks(int MaskTypeId)
        {
            try
            {
                var ms = await context.Masks.ReadListByIdAsync(MaskTypeId).ConfigureAwait(false);
                Masks = new ObservableCollection<Mask>(ms);
            }
            catch (Exception ex)
            {
                dialogService.ShowMsg($"Ошибка чтения масок!\n{ex.Message}");
            }
        }
        public bool CanExecute(object parameter)
        {
            return parameter != null;
        }
        #endregion
    }
}
