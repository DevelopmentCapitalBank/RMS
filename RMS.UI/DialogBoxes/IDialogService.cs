using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.UI.DialogBoxes
{
    public enum FilterFile
    {
        Word,
        Excel,
        Image,
        Txt,
        All
    }
    public interface IDialogService
    {

        /// <summary>
        /// Вывод диалогового окна Ok
        /// </summary>
        /// <param name="msg">Сообщение пользователю</param>
        /// <returns></returns>
        Task<bool> ShowMsgOk(string msg);
        /// <summary>
        /// Вывод диалогового окна Ок Отмена
        /// </summary>
        /// <param name="msg">Сообщение пользователю</param>
        /// <returns></returns>
        Task<bool> ShowMsgOkCancel(string msg);
        /// <summary>
        /// Вывод диалогового окна Да Нет
        /// </summary>
        /// <param name="msg">Сообщение пользователю</param>
        /// <returns></returns>
        Task<bool> ShowMsgYesNo(string msg);
        /// <summary>
        /// Путь к файлу
        /// </summary>
        string FilePath { get; set; }
        /// <summary>
        /// Окно для выбора файла
        /// </summary>
        /// <param name="filterFile">фильтр по посику файлов</param>
        /// <returns></returns>
        bool OpenFileDialog(FilterFile filterFile);
        /// <summary>
        /// Окно для множественного выбора файлов
        /// </summary>
        /// <param name="filterFile">ильтр по посику файлов</param>
        /// <returns></returns>
        List<string> OpenFilesDialog(FilterFile filterFile);
        /// <summary>
        /// Окно для сохранения файла
        /// </summary>
        /// <returns></returns>
        bool SaveFileDialog();
        /// <summary>
        /// Окно для выбора директории
        /// </summary>
        /// <returns></returns>
        bool FolderPathDialog();
    }
}
