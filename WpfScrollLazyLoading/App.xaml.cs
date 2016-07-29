using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using FusionMVVM.Service;
using WpfScrollLazyLoading.ViewModel;

namespace WpfScrollLazyLoading
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            WindowLocator locator = new WindowLocator(ResourceAssembly);
            locator.RegisterAll();
            locator.ShowWindow(new MainViewModel());
        }
    }
}
