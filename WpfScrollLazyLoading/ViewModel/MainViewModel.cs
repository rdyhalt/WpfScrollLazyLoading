using System;
using System.Collections.ObjectModel;
using System.Linq;
using FusionMVVM;
using FusionMVVM.Command;

namespace WpfScrollLazyLoading.ViewModel
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        public ObservableCollection<string> Items { get; } = new ObservableCollection<string>();
        public ObservableCollection<string> Items2 { get; } = new ObservableCollection<string>();
        public RelayCommand ScrollToBottomCommand { get; }
        public RelayCommand ScrollToBottomCommand2 { get; }

        public MainViewModel()
        {
            ScrollToBottomCommand = new RelayCommand(GetMoreStuff);
            ScrollToBottomCommand2 = new RelayCommand(GetFoo);

            GetFoo();
            GetMoreStuff();
        }

        private void GetFoo()
        {
            Console.WriteLine("GetFoo... @ " + DateTime.Now);

            foreach (int i in Enumerable.Range(1, 15))
            {
                Items2.Add($"Item2 {Guid.NewGuid()}");
            }
        }

        private void GetMoreStuff()
        {
            Console.WriteLine("GetMoreStuff... @ " + DateTime.Now);

            foreach (int i in Enumerable.Range(1, 21))
            {
                Items.Add($"Item {Guid.NewGuid()}");
            }
        }
    }
}
