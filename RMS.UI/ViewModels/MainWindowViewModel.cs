using RMS.UI.Services;
using System.Collections.Generic;

namespace RMS.UI.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Fields
        private readonly Dictionary<int, IPageViewModel>? _pageViewModels = new();
        private int _SelectedViewModelIndex;
        private IPageViewModel? _pageViewModel;
        #endregion

        #region Properties
        public int SelectedViewModelIndex
        {
            get { return _SelectedViewModelIndex; }
            set 
            {
                if (_SelectedViewModelIndex == value)
                    return;
                _SelectedViewModelIndex = value;
                CurrentPageViewModel = _pageViewModels[_SelectedViewModelIndex];
                OnPropertyChanged(nameof(SelectedViewModelIndex));
            }
        }
        public IPageViewModel? CurrentPageViewModel
        {
            get => _pageViewModel;

            set
            {
                _pageViewModel = value;
                OnPropertyChanged(nameof(CurrentPageViewModel));
            }
        }
        #endregion

        public MainWindowViewModel(IDataModel pageViews)
        {
            _pageViewModels[0] = new HomeViewModel(0);
            _pageViewModels[0].ViewChanged += (o, s) =>
            {
                CurrentPageViewModel = _pageViewModels[s.Value];
            };
            _pageViewModels[1] = new GroupViewModel(1);
            _pageViewModels[1].ViewChanged += ( o, s ) => 
            {
                CurrentPageViewModel = _pageViewModels[s.Value];
            };
            _pageViewModels[2] = new CompanyViewModel(2);
            _pageViewModels[2].ViewChanged += ( o, s ) =>
            {
                CurrentPageViewModel = _pageViewModels[s.Value];
            };
            _pageViewModels[3] = new ImportViewModel(3);
            _pageViewModels[3].ViewChanged += ( o, s ) =>
            {
                CurrentPageViewModel = _pageViewModels[s.Value];
            };
            _pageViewModels[4] = new ExportViewModel(4);
            _pageViewModels[4].ViewChanged += ( o, s ) =>
            {
                CurrentPageViewModel = _pageViewModels[s.Value];
            };
            _pageViewModels[5] = new SettingsViewModel(5);
            _pageViewModels[5].ViewChanged += ( o, s ) =>
            {
                CurrentPageViewModel = _pageViewModels[s.Value];
            };

            CurrentPageViewModel = _pageViewModels[0];
        }
    }
}
