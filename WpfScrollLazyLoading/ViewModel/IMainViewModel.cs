using System.Collections.ObjectModel;
using FusionMVVM.Command;

namespace WpfScrollLazyLoading.ViewModel
{
    public interface IMainViewModel
    {
        int TotalCount { get; }

        ObservableCollection<string> Items { get; }

        RelayCommand ChunkLoaderCommand { get; }
        
    }
}