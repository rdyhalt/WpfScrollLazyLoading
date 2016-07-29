using System.Windows;
using FusionMVVM.Service;
using WpfScrollLazyLoading.ViewModel;

namespace WpfScrollLazyLoading
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public App()
        {
            WindowLocator locator = new WindowLocator(ResourceAssembly);
            locator.RegisterAll();
            locator.ShowWindow(new MainViewModel());
        }
    }
}
