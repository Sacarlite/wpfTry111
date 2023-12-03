using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using wpfTry.Model.Entities;
using wpfTry.VievModel.Interface;
using Microsoft.Win32;
using wpfTry.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.ComponentModel;
using wpfTry.Services.Events.EventInterface;
using wpfTry.Viev;

namespace wpfTry.VievModel
{
    public class EditVievModel : BaseVievModel, ICloseWindow
    {
        private IDelete _delete;
        private Product? _product;
        private List<UOfM> uoFMs;
        public int ProductId
        {
            get { return _product.Id; }
        }
        public string ProductName
        {
            get { return _product.Name; }
            set
            {
                if (_product.Name != value)
                {
                    _product.Name = value;
                    OnPropertyChanged(nameof(ProductName));
                }
            }
        }
        public string ProductColor
        {
            get { return _product.Color; }
            set
            {
                if (_product.Color != value)
                {
                    _product.Color = value;
                    OnPropertyChanged(nameof(ProductColor));
                }
            }
        }
        public string ProductHeatResistance
        {
            get { return _product.HeatResistance; }
            set
            {
                if (_product.HeatResistance != value)
                {
                    _product.HeatResistance = value;
                    OnPropertyChanged(nameof(ProductHeatResistance));
                }
            }
        }
        public string ProductManufacturer
        {
            get { return _product.manufacturer; }
            set
            {
                if (_product.manufacturer != value)
                {
                    _product.manufacturer = value;
                    OnPropertyChanged(nameof(ProductManufacturer));
                }
            }
        }
        public double ProductWidth
        {
            get { return _product.width; }
            set
            {
                if (_product.width != value)
                {
                    _product.width = value;
                    OnPropertyChanged(nameof(ProductWidth));
                }
            }
        }
        public double ProductHeight
        {
            get { return _product.height; }
            set
            {
                if (_product.height != value)
                {
                    _product.height = value;
                    OnPropertyChanged(nameof(ProductHeight));
                }
            }
        }
        public double ProductLength
        {
            get { return _product.length; }
            set
            {
                if (_product.length != value)
                {
                    _product.length = value;
                    OnPropertyChanged(nameof(ProductLength));
                }
            }
        }
        public double ProductVolume
        {
            get { return _product.volume; }
            set
            {
                if (_product.volume != value)
                {
                    _product.volume = value;
                    OnPropertyChanged(nameof(ProductVolume));
                }
            }
        }
        public string ProductDiscription
        {
            get { return _product.Discription; }
            set
            {
                if (_product.Discription != value)
                {
                    _product.Discription = value;
                    OnPropertyChanged(nameof(ProductDiscription));
                }
            }
        }
        private ObservableCollection<tmp2> _characteristicList;
        public ObservableCollection<tmp2> CharacteristicList
        {
            get { return _characteristicList; }
            set
            {
                _characteristicList = value;
                OnPropertyChanged(nameof(CharacteristicList));
            }
        }
        private ObservableCollection<ProductType> _typeList;
        public ObservableCollection<ProductType> typeList
        {
            get { return _typeList; }
            set
            {
                _typeList = value;
                OnPropertyChanged(nameof(typeList));
            }
        }
        private ProductType _sProductType;
        public ProductType sProductType
        {
            get { return _sProductType; }
            set
            {
                _sProductType = value;
                OnPropertyChanged(nameof(sProductType));
            }
        }

        void GetCharacteristic()
        {
            _characteristicList=new ObservableCollection<tmp2>();
            uoFMs = DatabaseLocator.Context.UOfMs.ToList();
            List<ListOfCharacteristics> specificationsList = DatabaseLocator.Context.ListsOfCharacteristics.Where(u => u.ProductTypeName == _sProductType).ToList();
            foreach (var elem in specificationsList)
            {

                tmp2 elemTmp = new tmp2(elem.CharacteristicsName.Name, uoFMs);
                var a = DatabaseLocator.Context.Specification.Where(u =>
                    u.CharacteristicsName == elem.CharacteristicsName && u.Product == _product).FirstOrDefault();
                elemTmp.content = a.Num;
                elemTmp.SelectedUOF = a.UOfM;
                _characteristicList.Add(elemTmp);
            }
            OnPropertyChanged(nameof(CharacteristicList));
        }
        public ICommand WindowLoaded
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    GetCharacteristic();
                });
            }
        }

        public EditVievModel(Product product, Action close, IDelete Delete)
        {
            _delete = Delete;
            this._product = product;
            this.close = close;
            _sProductType = _product.Type;
            OnPropertyChanged(nameof(sProductType));
            typeList = new ObservableCollection<ProductType>(DatabaseLocator.Context.ProductTypes.ToList());
            OnPropertyChanged(nameof(typeList));
        }
        public ICommand OpenImg
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    if (openFileDialog.ShowDialog() == true)
                    {
                        if (_product.Image != openFileDialog.FileName )
                        {
                            _product.Image = openFileDialog.FileName;
                        } ;
                        
                    }

                });
            }
        }
        public ICommand windowClosing
        {
            get
            {

                return new DelegateCommand(() =>
                {
                    CloseWindow();
                });

            }
        }
        public ICommand  SaveData
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    var prod = DatabaseLocator.Context.Products.Where(u => u.Id == _product.Id).FirstOrDefault();
                    prod = _product;
                    prod.Type = DatabaseLocator.Context.ProductTypes.Where(p => p.Id == _sProductType.Id).FirstOrDefault();
                    foreach (var elem in _characteristicList)
                    {
                        var specifications = DatabaseLocator.Context.Specification.Where(u=>u.CharacteristicsName.Name == elem.name).FirstOrDefault();
                        specifications.Num=elem.content;
                        specifications.UOfM = elem.SelectedUOF;
                        specifications.Product = prod;
                    }
                    DatabaseLocator.Context.SaveChanges();
                    MessageBox.Show("Сохранение прошло успешно!");
                    CloseWindow();
                });
            }
        }
        public ICommand DeleteData
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    var prod = DatabaseLocator.Context.Products.Where(u => u.Id == _product.Id).FirstOrDefault();
                    DatabaseLocator.Context.Products.Remove(prod);
                    _delete.InvokeDelteEvent();
                    DatabaseLocator.Context.SaveChanges();
                    MessageBox.Show("Удаление прошло успешно!");
                    CloseWindow();
                });
            }
        }
        public Action close { get; set; }
        private void CloseWindow()
        {
            close?.Invoke();
        }
        public bool CanClose()
        {
            return true;
        }
    }
}
