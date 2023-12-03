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
using wpfTry.VievModel.Interface;
using wpfTry.Viev;

namespace wpfTry.VievModel
{
    public class AddVievModel : BaseVievModel, ICloseWindow
    {
        private string _productName;
        private string _productColor;
        private string _productHeatResistance;
        private string _productManufacturer;
        private double _productWidth;
        private double _productHeight;
        private double _productLength;
        private double _productVolume;
        private string _productDiscription;
        private string _image;
        private ObservableCollection<tmp2> _characteristicList; 
        private List<UOfM> uoFMs;

        public string ProductName
        {
            get { return _productName; }
            set
            {
                if (_productName != value)
                {
                    _productName = value;
                    OnPropertyChanged(nameof(ProductName));
                }
            }
        }
        public string ProductColor
        {
            get { return _productColor; }
            set
            {
                if (_productColor != value)
                {
                    _productColor = value;
                    OnPropertyChanged(nameof(ProductColor));
                }
            }
        }
        public string ProductHeatResistance
        {
            get { return _productHeatResistance; }
            set
            {
                if (_productHeatResistance != value)
                {
                    _productHeatResistance = value;
                    OnPropertyChanged(nameof(ProductHeatResistance));
                }
            }
        }
        public string ProductManufacturer
        {
            get { return _productManufacturer; }
            set
            {
                if (_productManufacturer != value)
                {
                    _productManufacturer = value;
                    OnPropertyChanged(nameof(ProductManufacturer));
                }
            }
        }
        public double ProductWidth
        {
            get { return _productWidth; }
            set
            {
                if (_productWidth != value)
                {
                    _productWidth = value;
                    OnPropertyChanged(nameof(ProductWidth));
                }
            }
        }
        public double ProductHeight
        {
            get { return _productHeight; }
            set
            {
                if (_productHeight != value)
                {
                    _productHeight = value;
                    OnPropertyChanged(nameof(ProductHeight));
                }
            }
        }
        public double ProductLength
        {
            get { return _productLength; }
            set
            {
                if (_productLength != value)
                {
                    _productLength = value;
                    OnPropertyChanged(nameof(ProductLength));
                }
            }
        }
        public double ProductVolume
        {
            get { return _productVolume; }
            set
            {
                if (_productVolume != value)
                {
                    _productVolume = value;
                    OnPropertyChanged(nameof(ProductVolume));
                }
            }
        }
        public string ProductDiscription
        {
            get { return _productDiscription; }
            set
            {
                if (_productDiscription != value)
                {
                    _productDiscription = value;
                    OnPropertyChanged(nameof(ProductDiscription));
                }
            }
        }
        private ObservableCollection<ProductType> _typeList;
        public ObservableCollection<tmp2> CharacteristicList
        {
            get { return _characteristicList; }
            set
            {
                _characteristicList = value;
                OnPropertyChanged(nameof(CharacteristicList));
            }
        }
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
                 GetCharacteristic(value);
                OnPropertyChanged(nameof(sProductType));
            }
        }

        void GetCharacteristic(ProductType productType)
        {
            _characteristicList.Clear();
            uoFMs = DatabaseLocator.Context.UOfMs.ToList();
            List<ListOfCharacteristics> specificationsList = DatabaseLocator.Context.ListsOfCharacteristics.Where(u=>u.ProductTypeName== productType).ToList();
            foreach (var elem in specificationsList)
            {

                tmp2 elemTmp = new tmp2(elem.CharacteristicsName.Name, uoFMs);
                elemTmp.content = "                 ";
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
                });
            }
        }

        public AddVievModel( Action close)
        {
            this.close = close;
            _characteristicList = new ObservableCollection<tmp2>();
            typeList = new ObservableCollection<ProductType>(DatabaseLocator.Context.ProductTypes.ToList());
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
                        if (openFileDialog.FileName != "")
                        {
                            _image = openFileDialog.FileName;
                        };
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
        public ICommand SaveData
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    var prod = new Product(_productName, _productColor, _productDiscription, _productHeatResistance, _productWidth
                    , _productHeight, _productLength, _productVolume, _productManufacturer, _image,false);
                    foreach (var elem in _characteristicList)
                    {
                        var specifications = new Specifications();
                        var tmpElem= DatabaseLocator.Context.ListsOfCharacteristics
                            .Where(u => u.CharacteristicsName.Name == elem.name).FirstOrDefault();
                        specifications.CharacteristicsName = tmpElem.CharacteristicsName;
                        specifications.Num = elem.content;
                        specifications.UOfM = elem.SelectedUOF;
                        specifications.Product = prod;
                        DatabaseLocator.Context.Specification.Add(specifications);
                    }
                    prod.Type = DatabaseLocator.Context.ProductTypes.Where(p => p.Id == _sProductType.Id).FirstOrDefault();
                    DatabaseLocator.Context.SaveChanges();
                    MessageBox.Show("Сохранение прошло успешно!");
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
    public class tmp2
    {
        public tmp2(string name, List<UOfM> UOF)
        {
            this.name = name;
            this.UOF = UOF;
        }
        public string name { get; set; }
        public string content { get; set; }
        public List<UOfM> UOF { get; set; }
        public UOfM SelectedUOF { get; set; }

        public override string ToString()
        {
            return name;
        }
    }
}
