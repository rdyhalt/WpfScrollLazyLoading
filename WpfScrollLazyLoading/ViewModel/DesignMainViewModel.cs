using System.Collections.ObjectModel;
using FusionMVVM.Command;

namespace WpfScrollLazyLoading.ViewModel
{
    public class DesignMainViewModel : IMainViewModel
    {
        public int TotalCount { get; } = 1000;
        public ObservableCollection<string> Items { get; } = new ObservableCollection<string> { "Item 1" };
        public RelayCommand ChunkLoaderCommand { get; } = new RelayCommand(() => { });
        

    }
}