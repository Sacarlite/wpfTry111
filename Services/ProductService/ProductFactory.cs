using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfTry.Model.Entities;
using wpfTry.Services.Events.EventInterface;
using wpfTry.Services.ProductService.Interface;
using wpfTry.Viev.Interface;
using wpfTry.VievModel;


namespace wpfTry.Services.ProductViev
{
    public class ProductFactory: IProductWindowsFactory, IAdminWindowsFactory, IEditWindowsFactory, IAddFactory, IBascetWindowsFactory, IOrder, IManagerWindowFactory
    {
        public IWindow CreateProductWindow(Product item)
        {
            var window = new Viev.ProductWindow();
            var viewModel = new ProductVievModel(item, window.Close);
            window.DataContext = viewModel;
            return window;
        }
        public IWindow CreateEditWindow(Product item, IDelete Delete)
        {
            var window = new Viev.EditWindow();
            var viewModel = new EditVievModel(item, window.Close,Delete);
            window.DataContext = viewModel;
            return window;
        }


        public IWindow CreateAdminWindow(RolleModel rolle, IAdminCloseEvent adminClose)
        {
            var window = new Viev.AdminWindow();
            var viewModel = new AdminVievModel( window.Close,  rolle, adminClose);
            window.DataContext = viewModel;
            return window;
        }

        public IWindow CreateFactoryWindow()
        {
            var window = new Viev.AddNewProductWindow();
            var viewModel = new AddVievModel(window.Close);
            window.DataContext = viewModel;
            return window;
        }

        public IWindow CreateBusketWindow(ObservableCollection<BuscetItem> item, IChangeBasket busketChange)
        {
            var window = new Viev.Buscet();
            var viewModel = new BuscetVievModel(item, busketChange);
            window.DataContext = viewModel;
            return window;
        }

        public IWindow CreateOrderWindow(ObservableCollection<BuscetItem> item)
        {
            var window = new Viev.OrderWindow();
            var viewModel = new OrderVievModel(item, window.Close);
            window.DataContext = viewModel;
            return window;
        }

        public IWindow CreateManagerWindow(Order item)
        {
            var window = new Viev.ManagerOrderViev();
            var viewModel = new ManagerVievModel(item);
            window.DataContext = viewModel;
            return window;
        }
    }
}
