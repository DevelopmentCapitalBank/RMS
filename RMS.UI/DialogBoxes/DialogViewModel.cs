using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RMS.UI.DialogBoxes
{
    public class DialogViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Fields 
        private string _Msg;
        #endregion

        #region Properties
        public string Msg
        {
            get => _Msg;
            set
            {
                if (_Msg == value)
                    return;
                _Msg = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
