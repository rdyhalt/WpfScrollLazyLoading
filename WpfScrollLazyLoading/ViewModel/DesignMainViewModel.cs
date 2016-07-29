using System.Collections.ObjectModel;
using FusionMVVM.Command;

namespace WpfScrollLazyLoading.ViewModel
{
    public class DesignMainViewModel : IMainViewModel
    {
        public ObservableCollection<string> Items { get; } = new ObservableCollection<string> { "Item 1" };
        public ObservableCollection<string> Items2 { get; } = new ObservableCollection<string> { "Item 2" };
        public RelayCommand ScrollToBottomCommand { get; } = new RelayCommand(() => { });
        public RelayCommand ScrollToBottomCommand2 { get; } = new RelayCommand(() => { });
    }
}