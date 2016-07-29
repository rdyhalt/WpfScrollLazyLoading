using System.Collections.ObjectModel;
using FusionMVVM.Command;

namespace WpfScrollLazyLoading.ViewModel
{
    public interface IMainViewModel
    {
        ObservableCollection<string> Items { get; }
        ObservableCollection<string> Items2 { get; }

        RelayCommand ScrollToBottomCommand { get; }
        RelayCommand ScrollToBottomCommand2 { get; }
    }
}