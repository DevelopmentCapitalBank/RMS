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
        private ICommand? showMasks;
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
                if (selectedMaskType == value)
                {
                    return;
                }
                selectedMaskType = value;
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
                    //
                });
            }
        }

        public ICommand InsertMaskType
        {
            get
            {
                if (insertMaskType == null)
                {
                    insertMaskType = new RelayCommand(ExecuteInsertMaskType, CanExecuteInsertMaskType);
                }
                return insertMaskType;
            }
        }
        public void ExecuteInsertMaskType(object parameter)
        {
            //TO DO
        }
        public bool CanExecuteInsertMaskType(object parameter)
        {
            if (parameter == null)
            {
                return false;
            }
            return true;
        }

        public ICommand CancelInsertMaskType
        {
            get
            {
                return cancelInsertMaskType ??= new RelayCommand(x => {
                    //
                });
            }
        }

        public ICommand RemoveMaskType
        {
            get
            {
                if (removeMaskType == null)
                {
                    removeMaskType = new RelayCommand(ExecuteRemoveMaskType, CanExecuteRemoveMaskType);
                }
                return removeMaskType;
            }
        }
        public void ExecuteRemoveMaskType(object parameter)
        {
            //TO DO
        }
        public bool CanExecuteRemoveMaskType(object parameter)
        {
            if (parameter == null)
            {
                return false;
            }
            return true;
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
        public void ExecuteSaveMaskType(object parameter)
        {
            //TO DO
        }
        public bool CanExecuteSaveMaskType(object parameter)
        {
            if (parameter == null)
            {
                return false;
            }
            return true;
        }

        public ICommand ShowMasks
        {
            get
            {
                if (showMasks == null)
                {
                    showMasks = new RelayCommand(ExecuteShowMasks, CanExecuteShowMasks);
                }
                return showMasks;
            }
        }
        public void ExecuteShowMasks(object parameter)
        {
            //TO DO
        }
        public bool CanExecuteShowMasks(object parameter)
        {
            if (parameter == null)
            {
                return false;
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
            //TO DO
        }
        public bool CanExecuteShowPopupCreateMask(object parameter)
        {
            if (parameter == null)
            {
                return false;
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
        public void ExecuteInsertMask(object parameter)
        {
            //TO DO
        }
        public bool CanExecuteInsertMask(object parameter)
        {
            if (parameter == null)
            {
                return false;
            }
            return true;
        }

        public ICommand CancelInsertMask
        {
            get
            {
                return cancelInsertMask ??= new RelayCommand(x => {
                    //
                });
            }
        }

        public ICommand RemoveMask
        {
            get
            {
                if (removeMask == null)
                {
                    removeMask = new RelayCommand(ExecuteRemoveMask, CanExecuteRemoveMask);
                }
                return removeMask;
            }
        }
        public void ExecuteRemoveMask(object parameter)
        {
            //TO DO
        }
        public bool CanExecuteRemoveMask(object parameter)
        {
            if (parameter == null)
            {
                return false;
            }
            return true;
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
        #endregion
    }
}
