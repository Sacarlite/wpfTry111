using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using wpfTry.Model;
using wpfTry.Services.AtorizationService;
using wpfTry.Services.Events;
using wpfTry.Services.ProductService.Interface;
using wpfTry.Services.ProductViev;
using wpfTry.Viev;
using wpfTry.VievModel;

namespace wpfTry.Bootstrapper
{
    class Bootstrapper
    {
        public Window Run()
        {
            var context = new ProductContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            DatabaseLocator.Context = context;
            var mainWindow = new MainWindow();
            mainWindow.DataContext = new MainVievModel(new ProductFactory(), new AuthorizationFactory(),new ProductFactory(),new EventAgregator(), new EventAgregator(), new ProductFactory(), new ProductFactory(), new ProductFactory(),new EventAgregator(), new EventAgregator());
            mainWindow.Show();
            return mainWindow;
        }
    }
}
