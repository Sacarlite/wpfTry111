using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using wpfTry.Services;

namespace wpfTry
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Bootstrapper.Bootstrapper? _bootstrapper;
        protected override void OnStartup(StartupEventArgs e)
        {
            _bootstrapper = new Bootstrapper.Bootstrapper();
            MainWindow = _bootstrapper.Run();

        }
    }
}
