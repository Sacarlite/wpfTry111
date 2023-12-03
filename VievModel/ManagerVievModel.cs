using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using DevExpress.Mvvm.Native;
using Microsoft.Win32;
using wpfTry.Model.Entities;
using wpfTry.Services.Events.EventInterface;
using wpfTry.Services.ProductService.Interface;
using wpfTry.Services.ProductViev;
using wpfTry.Services.PdfConverter;
using wpfTry.Viev;

namespace wpfTry.VievModel
{
    public class ManagerVievModel : BaseVievModel
    {
        private IOrder _orderWindow;
        private Order _order;
        public ObservableCollection<OrderTable> _orderTable;
        public string Name
        {
            get { return _order.Name; }
            set
            {
                if (_order.Name != value)
                {
                    _order.Name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public OrderTable _selectedProduct;
        public OrderTable SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                if (_selectedProduct != value)
                {
                    _selectedProduct = value;
                    OnPropertyChanged(nameof(SelectedProduct));
                }
            }
        }
        public int Id
        {
            get { return _order.Id; }
        }
        public string Surname
        {
            get { return _order.Surname; }
            set
            {
                if (_order.Surname != value)
                {
                    _order.Surname = value;
                    OnPropertyChanged(nameof(Surname));
                }
            }
        }
        public string Middlename
        {
            get { return _order.Middlename; }
            set
            {
                if (_order.Middlename != value)
                {
                    _order.Middlename = value;
                    OnPropertyChanged(nameof(Middlename));
                }
            }
        }
        public string Email
        {
            get { return _order.EMail; }
            set
            {
                if (_order.EMail != value)
                {
                    _order.EMail = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }
        public string PhoneNumber
        {
            get { return _order.PhoneNumber; }
            set
            {
                if (_order.PhoneNumber != value)
                {
                    _order.PhoneNumber = value;
                    OnPropertyChanged(nameof(PhoneNumber));
                }
            }
        }
        public ObservableCollection<OrderTable> OrderTables
        {
            get
            {
                return _orderTable;
            }
            set
            {
                if (_orderTable != value)
                {
                    _orderTable = value;
                    OnPropertyChanged(nameof(OrderTables));
                }
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
                    if (OrderTables.Count != 0)
                    {
                        foreach (var elem in OrderTables)
                        {
                            quantity += elem.Quantity;
                        }
                    }

                    _quantity = quantity;
                    OnPropertyChanged(nameof(Quantity));
                });
            }
        }
        public ManagerVievModel(Order order)
        {
            this._order = order;
            _orderTable = new ObservableCollection<OrderTable>();
            _orderTable =DatabaseLocator.Context.OrderTable.Where(u=>u.Order==order).ToObservableCollection();
            _orderWindow = new ProductFactory();

        }
        public ICommand DeleteProd
        {
            get
            {
                return new DelegateCommand<OrderTable>((obj) =>
                {
                    foreach (var elem in _orderTable)
                    {
                        if (elem == obj)
                        {
                            _orderTable.Remove(elem);
                            OnPropertyChanged(nameof(OrderTables));
                            OnPropertyChanged(nameof(Quantity));
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
      
        public ICommand PlusProd
        {
            get
            {
                return new DelegateCommand<OrderTable>((obj) =>
                {
                    foreach (var elem in _orderTable)
                    {
                        if (elem == obj)
                        {
                            elem.Quantity++;
                            OnPropertyChanged(nameof(OrderTables));
                            OnPropertyChanged(nameof(Quantity));
                            break;
                        }
                    }
                });
            }
        }
        public ICommand AddToOrder
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    var newOrder = DatabaseLocator.Context.Orders.Where(u => u.Id == _order.Id).FirstOrDefault();
                    newOrder = _order;
                    DatabaseLocator.Context.SaveChanges();
                    foreach (var elem in OrderTables)
                    {
                        var newOrderTable = DatabaseLocator.Context.OrderTable.Where(u => u.Id == elem.Id).FirstOrDefault();
                        newOrderTable = elem;
                        DatabaseLocator.Context.SaveChanges();
                    }
                });
            }
        }
        public ICommand ConvertToPDF
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    PdfConverter pdfConverter = new PdfConverter();
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Pdf Files|*.pdf";
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        pdfConverter.ExportToPdf(_order,saveFileDialog.FileName);
                    }
                   
                    
                });
            }
        }
        public ICommand MinusProd
        {
            get
            {
                return new DelegateCommand<OrderTable>((obj) =>
                {
                    foreach (var elem in _orderTable)
                    {
                        if (elem == obj)
                        {
                            if (elem.Quantity > 0)
                            {
                                elem.Quantity--;
                                OnPropertyChanged(nameof(OrderTables));

                                break;
                            }
                            else
                            {
                                _orderTable.Remove(elem);
                                OnPropertyChanged(nameof(OrderTables));
                                break;
                            }
                        }
                    }
                });
            }
        }
    }
}
