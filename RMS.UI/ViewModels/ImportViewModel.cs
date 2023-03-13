using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Input;
using RMS.UI.Commands;
using RMS.UI.DialogBoxes;
using RMS.UI.Services;

namespace RMS.UI.ViewModels
{
    public class ImportViewModel : BaseViewModel, IPageViewModel
    {
        public event EventHandler<EventArgs<int>>? ViewChanged;
        public ImportViewModel(IDialogService dialogService, IExcelReader reader, int pageIndex = 4)
        {
            PageId = pageIndex;
            Title = "Import data";
            this.reader = reader;
            this.dialogService = dialogService;
            TypesOfUnloading = new Dictionary<int, string> {
                { 1, "ВизЛист" },
                { 2, "Обороты" },
                { 3, "Депозиты" },
                { 4, "Платежки" },
                { 5, "Конверсия ДБО" }
            };
            typeKey = 1;
        }

        #region Fields
        private readonly IExcelReader reader;
        private readonly IDialogService dialogService;
        private int typeKey;
        private string path = "";
        private ObservableCollection<string> sheets = new ObservableCollection<string>();
        private string? selectedSheet;
        private string output = "";
        private ICommand? importData;
        private ICommand? searchFile;
        #endregion

        #region Properties
        public int PageId { get; set; }
        public string Title { get; set; }
        public Dictionary<int, string> TypesOfUnloading { get; set; }
        public int TypeKey
        {
            get => typeKey;
            set
            {
                if (value == typeKey)
                {
                    return;
                }
                typeKey = value;
                Path = string.Empty;
                Sheets = new ObservableCollection<string>();
                SelectedSheet = string.Empty;
                OnPropertyChanged(nameof(TypeKey));
            }
        }
        public string Path
        {
            get => path;
            set
            {
                if (string.Compare(value, path) == 0)
                {
                    return;
                }
                path = value;
                OnPropertyChanged(nameof(Path));
            }
        }
        public ObservableCollection<string> Sheets
        {
            get => sheets;
            set
            {
                if (sheets == value)
                {
                    return;
                }
                sheets = value;
                OnPropertyChanged(nameof(Sheets));
            }
        }
        public string? SelectedSheet
        {
            get => selectedSheet;
            set
            {
                if (string.Compare(value, selectedSheet) == 0)
                {
                    return;
                }
                selectedSheet = value;
                OnPropertyChanged(nameof(SelectedSheet));
            }
        }
        public string Output
        {
            get => output;
            set
            {
                if (string.Compare(value, output) == 0)
                {
                    return;
                }
                output = value;
                OnPropertyChanged(nameof(Output));
            }
        }
        #endregion

        #region Methods
        private async Task ImportVisList(DataTable dt)
        {

        }
        private async Task ImportRemains(DataTable dt)
        {

        }
        private async Task ImportDeposits(DataTable dt)
        {

        }
        private async Task ImportOperations(DataTable dt)
        {

        }
        private async Task ImportConversions(DataTable dt)
        {

        }
        #endregion

        #region Commands

        /// <summary>команда поиска файла</summary> 
        public ICommand SearchFile
        {
            get
            {
                return searchFile ??
                    (searchFile = new RelayCommand(obj => {
                        bool isChoice = dialogService.OpenFileDialog(FilterFile.Excel);
                        if (isChoice)
                        {
                            Path = dialogService.FilePath;
                            var sl = reader.GetSheets(Path);
                            if (string.Compare(sl[0], "Error") == 0)
                            {
                                Output = sl[1] + Output;
                            }
                            else
                            {
                                Sheets = new ObservableCollection<string>(sl);
                                Output = TypesOfUnloading[TypeKey] + ": " + path + "\n" + Output;
                            }
                        }
                    }));
            }
        }
        
        /// <summary>команда импотра данных из файла в базу аднных</summary> 
        public ICommand ImportData
        {
            get
            {
                if (importData == null)
                {
                    importData = new RelayCommand(ExecuteImportData, CanExecuteImportData);
                }
                return importData;
            }
        }
        public async void ExecuteImportData(object parameter)
        {
            try
            {
                var dt = await Task.Run(() => reader.Read(SelectedSheet, Path)).ConfigureAwait(false);

                switch (TypeKey)
                {
                    case 1:
                        await ImportVisList(dt).ConfigureAwait(false);
                        break;
                    case 2:
                        await ImportRemains(dt).ConfigureAwait(false);
                        break;
                    case 3:
                        await ImportDeposits(dt).ConfigureAwait(false);
                        break;
                    case 4:
                        await ImportOperations(dt).ConfigureAwait(false);
                        break;
                    case 5:
                        await ImportConversions(dt).ConfigureAwait(false);
                        break;
                    default:
                        break;
                }
            }
            catch(Exception ex)
            {
                await dialogService.ShowMsgOk($"Ошибка импорта данных!\n{ ex.Message }").ConfigureAwait(false);
            }
        }
        public bool CanExecuteImportData(object parameter)
        {
            if (parameter == null)
            {
                return false;
            }
            if (parameter is string s)
            {
                return !string.IsNullOrEmpty(s);
            }
            return true;
        }
        #endregion
    }
}
