using System.Collections.Generic;

namespace RMS.UI.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Fields
        private Dictionary<int, IPageViewModel> _pageViewModels = new();
        private int _SelectedViewModelIndex;
        private IPageViewModel _pageViewModel;
        #endregion

        #region Properties
        public Dictionary<int, IPageViewModel> PageViewModels
        {
            get { return _pageViewModels; }
            set { _pageViewModels = value; }
        }
        public int SelectedViewModelIndex
        {
            get { return _SelectedViewModelIndex; }
            set 
            {
                if (_SelectedViewModelIndex == value)
                {
                    return;
                }
                _SelectedViewModelIndex = value;
                CurrentPageViewModel = _pageViewModels[_SelectedViewModelIndex];
                OnPropertyChanged(nameof(SelectedViewModelIndex));
            }
        }
        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return _pageViewModel;
            }
            set
            {
                _pageViewModel = value;
                OnPropertyChanged(nameof(CurrentPageViewModel));
            }
        }
        #endregion
    }
}
