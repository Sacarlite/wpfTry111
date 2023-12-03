using DevExpress.Mvvm.Native;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using wpfTry.Model.Entities;
using wpfTry.VievModel;
using wpfTry.Services.ProductViev;
using wpfTry.Services.ProductService.Interface;
using wpfTry.Viev;

namespace wpfTry.Services.PageService
{
    public class OrderTabelVievModel : BaseVievModel
    {

        private ObservableCollection<Order> _orderCollection;
        private IManagerWindowFactory _managerWindow;
        private Order _selectedOrder;
        public Order SelectedOrder
        {
            get
            {
                return _selectedOrder;
            }
            set
            {
                if (value != _selectedOrder)
                {
                    _selectedOrder = value;
                    OnPropertyChanged(nameof(SelectedOrder));
                }
            }
        }
        public ObservableCollection<Order> OrderCollection
        {
            get
            {
                return _orderCollection;
            }
            set
            {
                if (value != _orderCollection)
                {
                    _orderCollection = value;
                    OnPropertyChanged(nameof(OrderCollection));
                }
            }
        }
        public OrderTabelVievModel()
        {
            _orderCollection = new ObservableCollection<Order>();
            _managerWindow = new ProductFactory();
            OrderCollection = DatabaseLocator.Context.Orders.ToObservableCollection();
        }
        public ICommand OpenOrderWindow
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    _managerWindow.CreateManagerWindow(_selectedOrder).Show();
                });
            }
        }

    }
}
