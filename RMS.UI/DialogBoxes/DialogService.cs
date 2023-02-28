using System.Collections.Generic;
using MaterialDesignThemes.Wpf;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace RMS.UI.DialogBoxes
{
    public class DialogService : IDialogService
    {
        public string FilePath { get; set; } = string.Empty;

        public bool FolderPathDialog()
        {
            System.Windows.Forms.FolderBrowserDialog saveFileDialog = new System.Windows.Forms.FolderBrowserDialog();
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FilePath = saveFileDialog.SelectedPath;
                return true;
            }
            return false;
        }

        public bool OpenFileDialog(FilterFile filterFile)
        {
            string filt = "";
            switch (filterFile)
            {
                case FilterFile.All:
                    filt = "All files(*.*)| *.*";
                    break;
                case FilterFile.Excel:
                    filt = "Excel Files (*.xls, *.xlsx, *.xlsm, *.xlsb)| *.xls; *.xlsx; *.xlsm; *.xlsb";
                    break;
                case FilterFile.Word:
                    filt = "Word Files (*.doc, *.docx, *.docm)| *.doc; *.docx; *.docm";
                    break;
                case FilterFile.Txt:
                    filt = "Text Files (*.txt)|*.txt";
                    break;
                case FilterFile.Image:
                    filt = "Image Files (*.bmp, *.jpg, *.png)| *.bmp; *.jpg; *.png";
                    break;
            }
            OpenFileDialog openFileDialog = new OpenFileDialog {
                Filter = filt
            };
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                return true;
            }
            return false;
        }

        public List<string> OpenFilesDialog(FilterFile filterFile)
        {
            string filt = "";
            switch (filterFile)
            {
                case FilterFile.All:
                    filt = "All files(*.*)| *.*";
                    break;
                case FilterFile.Excel:
                    filt = "Excel Files (*.xls, *.xlsx, *.xlsm, *.xlsb)| *.xls; *.xlsx; *.xlsm; *.xlsb";
                    break;
                case FilterFile.Word:
                    filt = "Word Files (*.doc, *.docx, *.docm)| *.doc; *.docx; *.docm";
                    break;
                case FilterFile.Txt:
                    filt = "Text Files (*.txt)|*.txt";
                    break;
                case FilterFile.Image:
                    filt = "Image Files (*.bmp, *.jpg, *.png)| *.bmp; *.jpg; *.png";
                    break;
            }
            List<string> result = new List<string>();
            OpenFileDialog openFileDialog = new OpenFileDialog {
                Filter = filt,
                Multiselect = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string file in openFileDialog.FileNames)
                {
                    result.Add(file);
                }
                return result;
            }
            return result;
        }

        public bool SaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            //saveFileDialog.Filter = "Frs files(*.frs)|*.frs|All files(*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                FilePath = saveFileDialog.FileName;
                return true;
            }
            return false;
        }

        public async Task<bool> ShowMsgOk(string msg)
        {
            try
            {
                DialogViewModel dialog_vm = new DialogViewModel { Msg = msg };
                var dialog_v = new OkView {
                    DataContext = dialog_vm
                };
                var result = await DialogHost.Show(dialog_v, "RootDialog");
                return (bool)result;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ShowMsgOkCancel(string msg)
        {
            try
            {
                DialogViewModel dialog_vm = new DialogViewModel { Msg = msg };
                var dialog_v = new OkCancelView {
                    DataContext = dialog_vm
                };

                var result = await DialogHost.Show(dialog_v, "RootDialog");

                return (bool)result;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ShowMsgYesNo(string msg)
        {
            try
            {
                DialogViewModel dialog_vm = new DialogViewModel { Msg = msg };
                var dialog_v = new YesNoView {
                    DataContext = dialog_vm
                };

                var result = await DialogHost.Show(dialog_v, "RootDialog");

                return (bool)result;
            }
            catch
            {
                return false;
            }
        }
    }
}
