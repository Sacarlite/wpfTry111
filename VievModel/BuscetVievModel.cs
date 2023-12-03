using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using wpfTry.Model.Entities;
using wpfTry.Services;
using wpfTry.Services.ProductService.Interface;
using wpfTry.VievModel.Interface;
using wpfTry.Services.ProductViev;
using wpfTry.Services.Events.EventInterface;
using wpfTry.Viev;

namespace wpfTry.VievModel
{
    public class BuscetVievModel : BaseVievModel
    {
        private IProductWindowsFactory _productWindows;
        private IChangeBasket _busketEvent;
        private IOrder _orderWindow;
        public ObservableCollection<BuscetItem> _buscetProducts;

        public ObservableCollection<BuscetItem> BuscetProducts
        {
            get
            {
                return _buscetProducts;
            }
            set
            {
                if (_buscetProducts != value)
                {
                    _buscetProducts = value;
                    OnPropertyChanged(nameof(BuscetProducts));
                }
            }
        }
        
        private BuscetItem? selectedProduct;
        public BuscetItem SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));

            }
        }
        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged(nameof(Quantity));
                }
            }
        }
        public ICommand WindowLoaded
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    int quantity = 0;
                    if (BuscetProducts.Count!=0)
                    {
                        foreach (var elem in BuscetProducts)
                        {
                            quantity += elem.quantity;
                        }
                    }

                    _quantity = quantity;
                    OnPropertyChanged(nameof(Quantity));
                });
            }
        }
        public BuscetVievModel(ObservableCollection<BuscetItem> BuscetProducts, IChangeBasket busketEvent)
        {
            this._buscetProducts = BuscetProducts;
            _productWindows = new ProductFactory();
            _orderWindow = new ProductFactory();
            _busketEvent = busketEvent;
        }
        public ICommand DeleteProd
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    foreach (var elem in _buscetProducts)
                    {
                        if (elem == selectedProduct)
                        {
                            _buscetProducts.Remove(elem);
                            OnPropertyChanged(nameof(BuscetProducts));
                            OnPropertyChanged(nameof(Quantity));
                            _busketEvent.InvokeEvent(_buscetProducts);
                            break;
                        }
                    }
                });
            }
        }
        public ICommand OpenCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    //_productWindows.CreateProductWindow(selectedProduct).Show();
                });
            }
        }
        public ICommand DeleteBusket
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    _buscetProducts.Clear();
                    _quantity = 0;
                    OnPropertyChanged(nameof(BuscetProducts));
                    OnPropertyChanged(nameof(Quantity));
                    _busketEvent.InvokeEvent(_buscetProducts);
                });
            }
        }
        public ICommand PlusProd
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    foreach (var elem in _buscetProducts)
                    {
                        if (elem == selectedProduct)
                        {
                            elem.quantity++;
                            OnPropertyChanged(nameof(BuscetProducts));
                            OnPropertyChanged(nameof(Quantity));
                            break;
                        }
                    }
                });
            }
        }
        public ICommand AddOrder
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (_buscetProducts.Count != 0)
                    {
                        _orderWindow.CreateOrderWindow(_buscetProducts).Show();
                    }
                });
            }
        }
        public ICommand MinusProd
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    foreach (var elem in _buscetProducts)
                    {
                        if (elem == selectedProduct)
                        {
                            if (elem.quantity > 0)
                            {
                                elem.quantity--;
                                OnPropertyChanged(nameof(BuscetProducts));
                        
                                break;
                            }
                            else
                            {
                                _buscetProducts.Remove(elem);
                                OnPropertyChanged(nameof(BuscetProducts));
                                _busketEvent.InvokeEvent(_buscetProducts);
                                break;
                            }
                        }
                    }
                });
            }
        }
    }
   
}
