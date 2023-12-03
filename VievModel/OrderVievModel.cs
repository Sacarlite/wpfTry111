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
using wpfTry.Services.Events.EventInterface;
using wpfTry.Services.ProductViev;
using wpfTry.Viev;
using wpfTry.VievModel.Interface;

namespace wpfTry.VievModel
{
    public class OrderVievModel: BaseVievModel, ICloseWindow
    {
        public ObservableCollection<BuscetItem> _buscetProducts;
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        private string _surname;
        public string Surname
        {
            get { return _surname; }
            set
            {
                if (_surname != value)
                {
                    _surname = value;
                    OnPropertyChanged(nameof(Surname));
                }
            }
        }
        private string _middlename;
        public string Middlename
        {
            get { return _middlename; }
            set
            {
                if (_middlename != value)
                {
                    _middlename = value;
                    OnPropertyChanged(nameof(Middlename));
                }
            }
        }
        private string _eMail;
        public string Email
        {
            get { return _eMail; }
            set
            {
                if (_eMail != value)
                {
                    _eMail = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }
        private string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
                    OnPropertyChanged(nameof(PhoneNumber));
                }
            }
        }
        private void CloseWindow()
        {
            close?.Invoke();
        }
        public OrderVievModel(ObservableCollection<BuscetItem> BuscetProducts, Action close)
        {
            this._buscetProducts = BuscetProducts;
            this.close = close;
        }
        public ICommand AddOrder
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (!_phoneNumber.Contains("+7")|| !_phoneNumber.Contains("8"))
                    {
                        MessageBox.Show("Ошибка при вводе номера телефона");
                        _phoneNumber = "";
                        OnPropertyChanged(nameof(PhoneNumber));
                        return;
                    }
                    for (int i = 1; i < _phoneNumber.Length; i++)
                    {
                        if (!char.IsNumber(_phoneNumber[i]))
                        {
                            MessageBox.Show("Ошибка при вводе номера телефона");
                            _phoneNumber = "";
                            OnPropertyChanged(nameof(PhoneNumber));
                            return;
                        } 
                    }

                    var order = new Order(_name, _surname, _middlename, _eMail, _phoneNumber);
                    DatabaseLocator.Context.Orders.Add(order);
                    foreach (var elem in _buscetProducts)
                    {
                        var a = new OrderTable();
                        a.Product = elem.Product;
                        a.Quantity = elem.quantity;
                        a.Order = order;
                        DatabaseLocator.Context.OrderTable.Add(a);
                    }

                    DatabaseLocator.Context.SaveChanges();
                    MessageBox.Show("Заказ успешно совершён");
                    CloseWindow();
                });
            }
        }

        public Action close { get; set; }
        public bool CanClose()
        {
            return true;
        }
    }
}
