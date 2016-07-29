using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FusionMVVM;
using FusionMVVM.Command;

namespace WpfScrollLazyLoading.ViewModel
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        public int TotalCount { get; }
        public ObservableCollection<string> Items { get; } = new ObservableCollection<string>();
        public RelayCommand ChunkLoaderCommand { get; }

        private bool _doneGetMoreStuff;


        public MainViewModel()
        {
            ChunkLoaderCommand = new RelayCommand(GetMoreStuff);

            GetMoreStuff();
        }

        private void GetMoreStuff()
        {
            if (_doneGetMoreStuff) return;

            Console.WriteLine("GetMoreStuff... @ " + DateTime.Now);

            foreach (int i in Enumerable.Range(1, 21))
            {
                Items.Add($"Item {Guid.NewGuid()}");
            }


        }
    }
}
