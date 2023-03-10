using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Input;
using RMS.DocumentProcessing.Reader;
using RMS.DocumentProcessing.Verification;
using RMS.UI.Commands;
using RMS.UI.DialogBoxes;

namespace RMS.UI.ViewModels
{
    public class ImportViewModel : BaseViewModel, IPageViewModel
    {
        public event EventHandler<EventArgs<int>>? ViewChanged;
        public ImportViewModel(IDialogService dialogService, IExcelReader reader,
            IDocumentVerification verification, int pageIndex = 4)
        {
            PageId = pageIndex;
            Title = "Import data";
            this.reader = reader;
            this.dialogService = dialogService;
            this.verification = verification;
            TypesOfUnloading = new Dictionary<TypeDocument, string> {
                { TypeDocument.VisList, "ВизЛист" },
                { TypeDocument.Turnovers, "Обороты" },
                { TypeDocument.Deposits, "Депозиты" },
                { TypeDocument.Operation, "Платежки" },
                { TypeDocument.Conversion, "Конверсия ДБО" }
            };
            typeKey = TypeDocument.VisList;
        }

        #region Fields
        private readonly IExcelReader reader;
        private readonly IDialogService dialogService;
        private readonly IDocumentVerification verification;
        private TypeDocument typeKey;
        private string path = "";
        private ObservableCollection<string> sheets = new();
        private string? selectedSheet;
        private string output = "";
        private ICommand? importData;
        private ICommand? searchFile;
        #endregion

        #region Properties
        public int PageId { get; set; }
        public string Title { get; set; }
        public Dictionary<TypeDocument, string> TypesOfUnloading { get; set; }
        public TypeDocument TypeKey
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
            bool isVerified = verification.IsVerified(TypeDocument.VisList, dt);
            dialogService.ShowMsg(isVerified.ToString());
            /*
             * проверить на соответствие данной книге
             * трансформировать данные в сущности в бд
             * проверить и занести данные в бд
             *
            bool isOriginal = vislist.IsOriginal(dt);
            if (isOriginal)
            {

            }*/
        }
        private async Task ImportTurnovers(DataTable dt)
        {
            bool isVerified = verification.IsVerified(TypeDocument.Turnovers, dt);
            dialogService.ShowMsg(isVerified.ToString());
        }
        private async Task ImportDeposits(DataTable dt)
        {
            bool isVerified = verification.IsVerified(TypeDocument.Deposits, dt);
            dialogService.ShowMsg(isVerified.ToString());
        }
        private async Task ImportOperations(DataTable dt)
        {
            bool isVerified = verification.IsVerified(TypeDocument.Operation, dt);
            dialogService.ShowMsg(isVerified.ToString());
        }
        private async Task ImportConversions(DataTable dt)
        {
            bool isVerified = verification.IsVerified(TypeDocument.Conversion, dt);
            dialogService.ShowMsg(isVerified.ToString());
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

                if (dt.TableName == "Error")
                {
                    throw new Exception(dt.Rows[0].ItemArray[0].ToString());
                }

                switch (TypeKey)
                {
                    case TypeDocument.VisList:
                        await ImportVisList(dt).ConfigureAwait(false);
                        break;
                    case TypeDocument.Turnovers:
                        await ImportTurnovers(dt).ConfigureAwait(false);
                        break;
                    case TypeDocument.Deposits:
                        await ImportDeposits(dt).ConfigureAwait(false);
                        break;
                    case TypeDocument.Operation:
                        await ImportOperations(dt).ConfigureAwait(false);
                        break;
                    case TypeDocument.Conversion:
                        await ImportConversions(dt).ConfigureAwait(false);
                        break;
                    default:
                        break;
                }
            }
            catch(Exception ex)
            {
                dialogService.ShowMsg($"Ошибка импорта данных!\n{ ex.Message }");
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
