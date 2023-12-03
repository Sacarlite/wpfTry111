using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using DevExpress.Mvvm;
using System.Windows.Input;
using wpfTry.Model;
using wpfTry.Model.Entities;
using System.Linq.Expressions;
using System.Collections;
using wpfTry.Services.AtorizationService.@interface;
using wpfTry.Services.ProductService.Interface;
using DevExpress.Mvvm.Native;
using System.Diagnostics;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using wpfTry.Services.Events.EventInterface;
using wpfTry.Viev.Templates;
using wpfTry.Services.PageService;
using wpfTry.Services.PageService.PageInterface;
using ControlzEx.Standard;
using wpfTry.Services.ProductViev;
using wpfTry.Services.Events;
using wpfTry.Viev;

namespace wpfTry.VievModel
{

    public class MainVievModel: BaseVievModel
    {
        private IProductWindowsFactory _productWindowsFactory;
        private IAdminWindowsFactory _adminWindowFactory;
        private IEditWindowsFactory _editProductWindowsFactory;
        private IAuthorizationFactory _authorizationFactory;
        private IAddFactory _addWindowsFactory;
        private IBascetWindowsFactory _bascetWindowsFactory;
        private ILoginEvent _login;
        private IChangeBasket _changeBasket;
        private IAdminCloseEvent _adminClose;
        private IDelete _delete;
        private RolleModel _rolle;
        private bool adminVievFlag = false;
        private bool sortedFlag = false;
        private ProductType? selectedType;
        private Visibility adminVisibility;
        private Visibility sortedVisibility=Visibility.Hidden;
        private Product? selectedProduct;
        private ObservableCollection<Product> _products;
        private string _serchText;
        public List <ProductType> ProductTypes { get; set; }
        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set
            {
                if (_products != value)
                {
                    _products = value;
                    OnPropertyChanged(nameof(Products));
                }
            }
        }
        public ObservableCollection<BuscetItem> BuscetProducts { get; set; }
        public Product SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));

            }
        }

        private ObservableCollection<FilterValue> _filterValueList;
        public ObservableCollection<FilterValue> FilterValueList
        {
            get { return _filterValueList; }
            set
            {
                _filterValueList = value;
                OnPropertyChanged(nameof(FilterValueList));
            }
        }
        private ObservableCollection<BoolCharacteristic> _colorCharacteristicList;
        public ObservableCollection<BoolCharacteristic> ColorcharacteristicList
        {
            get { return _colorCharacteristicList; }
            set
            {
                _colorCharacteristicList = value;
                OnPropertyChanged(nameof(ColorcharacteristicList));

            }
        }
        private ObservableCollection<BoolCharacteristic> _heatResistancecharacteristicList;
        public ObservableCollection<BoolCharacteristic> HeatResistanceCharacteristicList
        {
            get { return _heatResistancecharacteristicList; }
            set
            {
                _heatResistancecharacteristicList = value;
                OnPropertyChanged(nameof(HeatResistanceCharacteristicList));

            }
        }
        private ObservableCollection<BoolCharacteristic> _manufacturerCharacteristicList;
        public ObservableCollection<BoolCharacteristic> ManufacturerCharacteristicList
        {
            get { return _manufacturerCharacteristicList; }
            set
            {
                _manufacturerCharacteristicList = value;
                OnPropertyChanged(nameof(ManufacturerCharacteristicList));

            }
        }
        private double _volumeMaxValue;
        private double _volumeMinSliderValue;
        private double _volumeMaxSliderValue;
        private double _volumeMinValue;
        public double VolumeMaxValue
        {
            get { return _volumeMaxValue; }
            set
            {
                if (_volumeMaxValue != value)
                {
                    _volumeMaxValue = value;
                    OnPropertyChanged(nameof(VolumeMaxValue));
                }
            }
        }
        public double VolumeMinSliderValue
        {
            get { return _volumeMinSliderValue; }
            set
            {
                if (_volumeMinSliderValue != value)
                {
                    _volumeMinSliderValue = value;
                    OnPropertyChanged(nameof(VolumeMinSliderValue));
                }
            }
        }
        public double VolumeMaxSliderValue
        {
            get { return _volumeMaxSliderValue; }
            set
            {
                if (_volumeMaxSliderValue != value)
                {
                    _volumeMaxSliderValue = value;
                    OnPropertyChanged(nameof(VolumeMaxSliderValue));
                }
            }
        }
        public double VolumeMinValue
        {
            get { return _volumeMinValue; }
            set
            {
                if (_volumeMinValue != value)
                {
                    _volumeMinValue = value;
                    OnPropertyChanged(nameof(VolumeMinValue));
                }
            }
        }
        private double _widthMaxValue;
        private double _widthMinSliderValue;
        private double _widthMaxSliderValue;
        private double _widthMinValue;
        public double WidthMaxValue
        {
            get { return _widthMaxValue; }
            set
            {
                if (_widthMaxValue != value)
                {
                    _widthMaxValue = value;
                    OnPropertyChanged(nameof(WidthMaxValue));
                }
            }
        }
        public double WidthMinSliderValue
        {
            get { return _widthMinSliderValue; }
            set
            {
                if (_widthMinSliderValue != value)
                {
                    _widthMinSliderValue = value;
                    OnPropertyChanged(nameof(WidthMinSliderValue));
                }
            }
        }
        public double WidthMaxSliderValue
        {
            get { return _widthMaxSliderValue; }
            set
            {
                if (_widthMaxSliderValue != value)
                {
                    _widthMaxSliderValue = value;
                    OnPropertyChanged(nameof(WidthMaxSliderValue));
                }
            }
        }
        public double WidthMinValue
        {
            get { return _widthMinValue; }
            set
            {
                if (_widthMinValue != value)
                {
                    _widthMinValue = value;
                    OnPropertyChanged(nameof(WidthMinValue));
                }
            }
        }
        private double _heightMaxValue;
        private double _heightMinSliderValue;
        private double _heightMaxSliderValue;
        private double _heightMinValue;
        public double HeightMaxValue
        {
            get { return _heightMaxValue; }
            set
            {
                if (_heightMaxValue != value)
                {
                    _heightMaxValue = value;
                    OnPropertyChanged(nameof(HeightMaxValue));
                }
            }
        }
        public double HeightMinSliderValue
        {
            get { return _heightMinSliderValue; }
            set
            {
                if (_heightMinSliderValue != value)
                {
                    _heightMinSliderValue = value;
                    OnPropertyChanged(nameof(HeightMinSliderValue));
                }
            }
        }
        public double HeightMaxSliderValue
        {
            get { return _heightMaxSliderValue; }
            set
            {
                if (_heightMaxSliderValue != value)
                {
                    _heightMaxSliderValue = value;
                    OnPropertyChanged(nameof(HeightMaxSliderValue));
                }
            }
        }
        public double HeightMinValue
        {
            get { return _heightMinValue; }
            set
            {
                if (_heightMinValue != value)
                {
                    _heightMinValue = value;
                    OnPropertyChanged(nameof(HeightMinValue));
                }
            }
        }
        private double _lengthMaxValue;
        private double _lengthMinSliderValue;
        private double _lengthMaxSliderValue;
        private double _lengthMinValue;
        public double LengthMaxValue
        {
            get { return _lengthMaxValue; }
            set
            {
                if (_lengthMaxValue != value)
                {
                    _lengthMaxValue = value;
                    OnPropertyChanged(nameof(LengthMaxValue));
                }
            }
        }
        public double LengthMinSliderValue
        {
            get { return _lengthMinSliderValue; }
            set
            {
                if (_lengthMinSliderValue != value)
                {
                    _lengthMinSliderValue = value;
                    OnPropertyChanged(nameof(LengthMinSliderValue));
                }
            }
        }
        public double LengthMaxSliderValue
        {
            get { return _lengthMaxSliderValue; }
            set
            {
                if (_lengthMaxSliderValue != value)
                {
                    _lengthMaxSliderValue = value;
                    OnPropertyChanged(nameof(LengthMaxSliderValue));
                }
            }
        }
        public double LengthMinValue
        {
            get { return _lengthMinValue; }
            set
            {
                if (_lengthMinValue != value)
                {
                    _lengthMinValue = value;
                    OnPropertyChanged(nameof(LengthMinValue));
                }
            }
        }
        public bool EnabledFilterViev
        {
            get
            {
                return sortedFlag;
            }
        }
        public Visibility SortedViev
        {
            get
            {
                return sortedVisibility;
            }
        }
        public Visibility AdminViev
        {
            get
            {
                if (_rolle.AdminFlag)
                {
                    adminVisibility = Visibility.Visible;
                }
                else
                {
                    adminVisibility = Visibility.Hidden;
                }
                return adminVisibility;

            }
        }
        public string SerchText
        {
            get { return _serchText; }
            set
            {
                _serchText = value;
                OnPropertyChanged(nameof(SerchText));

            }
        }
        public ProductType SelectedType
        {
            get { return selectedType; }
            set
            {
                if (selectedType != value)
                {
                    selectedType = value;
                    GetProductList(selectedType);
                    if (value == null)
                    {
                        sortedVisibility = Visibility.Hidden;
                    }
                    else
                    {
                        sortedVisibility = Visibility.Visible;
                    }
                    OnPropertyChanged(nameof(FilterValueList));
                    OnPropertyChanged(nameof(SortedViev));
                }

            }
        }
        private void GetProductList (ProductType selectedType)
        {
                _filterValueList.Clear();
                Products.Clear();
                Products = DatabaseLocator.Context.Products.Where(p => p.Type == selectedType).ToObservableCollection();
                if (Products.Count == 0)
                {
                    return;
                }
                _serchText = "";
                {
                    _colorCharacteristicList = new ObservableCollection<BoolCharacteristic>();
                    var characteristic =
                        DatabaseLocator.Context.Products.Where(u => u.Type == selectedType).ToList();
                    List<string> tmpList = new List<string>();
                    foreach (var elem in characteristic)
                    {
                        tmpList.Add(elem.Color);
                    }

                    tmpList = removeDuplicates(tmpList);
                    foreach (var elem in tmpList)
                    {
                        _colorCharacteristicList.Add(new BoolCharacteristic(elem));
                    }
                    OnPropertyChanged(nameof(ColorcharacteristicList));
            }
                {
                    _heatResistancecharacteristicList = new ObservableCollection<BoolCharacteristic>();
                    var characteristic =
                        DatabaseLocator.Context.Products.Where(u => u.Type == selectedType).ToList();
                    List<string> tmpList = new List<string>();
                    foreach (var elem in characteristic)
                    {
                        tmpList.Add(elem.HeatResistance);
                    }

                    tmpList = removeDuplicates(tmpList);
                    foreach (var elem in tmpList)
                    {
                        _heatResistancecharacteristicList.Add(new BoolCharacteristic(elem));
                    }
                    OnPropertyChanged(nameof(HeatResistanceCharacteristicList));
            }
                {
                    _manufacturerCharacteristicList = new ObservableCollection<BoolCharacteristic>();
                    var characteristic =
                        DatabaseLocator.Context.Products.Where(u => u.Type == selectedType).ToList();
                    List<string> tmpList = new List<string>();
                    foreach (var elem in characteristic)
                    {
                        tmpList.Add(elem.manufacturer);
                    }

                    tmpList = removeDuplicates(tmpList);
                    foreach (var elem in tmpList)
                    {
                        _manufacturerCharacteristicList.Add(new BoolCharacteristic(elem));
                    }
                    OnPropertyChanged(nameof(ManufacturerCharacteristicList));
            }
                {
                    List<double> doubleCharacteristics = new List<double>();
                    var characteristic =
                        DatabaseLocator.Context.Products.Where(u => u.Type == selectedType).ToList();

                    foreach (var elem in characteristic)
                    {
                        doubleCharacteristics.Add(Convert.ToDouble(elem.width));
                    }

                    VolumeMinValue = doubleCharacteristics.Min();
                    VolumeMaxValue = doubleCharacteristics.Max();
            }
                {
                    List<double> doubleCharacteristics = new List<double>();
                    var characteristic =
                        DatabaseLocator.Context.Products.Where(u => u.Type == selectedType).ToList();

                    foreach (var elem in characteristic)
                    {
                        doubleCharacteristics.Add(Convert.ToDouble(elem.width));
                    }

                    HeightMinValue = doubleCharacteristics.Min();
                    HeightMaxValue = doubleCharacteristics.Max();
                }
                {
                    List<double> doubleCharacteristics = new List<double>();
                    var characteristic =
                        DatabaseLocator.Context.Products.Where(u => u.Type == selectedType).ToList();

                    foreach (var elem in characteristic)
                    {
                        doubleCharacteristics.Add(Convert.ToDouble(elem.width));
                    }

                    WidthMinValue = doubleCharacteristics.Min();
                    WidthMaxValue = doubleCharacteristics.Max();
                }
                {
                    List<double> doubleCharacteristics = new List<double>();
                    var characteristic =
                        DatabaseLocator.Context.Products.Where(u => u.Type == selectedType).ToList();

                    foreach (var elem in characteristic)
                    {
                        doubleCharacteristics.Add(Convert.ToDouble(elem.width));
                    }

                    LengthMinValue = doubleCharacteristics.Min();
                    LengthMaxValue = doubleCharacteristics.Max();
                }
            var CharacteristicList= DatabaseLocator.Context.ListsOfCharacteristics.Where(p => p.ProductTypeName == selectedType).ToList();
                foreach (var elem in CharacteristicList)
                {
                    if (elem.CharacteristicsName.Coment)
                    {
                        Page curentPage = new ChecBoxPage();
                        var a= new CheckBoxPageVievModel(elem.CharacteristicsName, selectedType);
                        curentPage.DataContext = a;
                        _filterValueList.Add(new FilterValue(elem.CharacteristicsName, curentPage,a));
                    }
                    else
                    {

                        Page curentPage = new SliderPage();
                        var a= new SliderPageVievModel(elem.CharacteristicsName, selectedType);
                        curentPage.DataContext = a;
                        _filterValueList.Add(new FilterValue(elem.CharacteristicsName, curentPage, a));
                    }

                }
                OnPropertyChanged(nameof(SerchText));
                OnPropertyChanged(nameof(FilterValueList));
                OnPropertyChanged(nameof(Products));
        }
        public MainVievModel(IProductWindowsFactory productWindowsFactory, IAuthorizationFactory authorizationFactory, IEditWindowsFactory editProductWindowsFactory, ILoginEvent login, IChangeBasket basketChange,
            IAddFactory addWindowsFactory, IBascetWindowsFactory bascetWindowsFactory, IAdminWindowsFactory adminWindowFactory, IAdminCloseEvent adminClose, IDelete Delete)
        {
            var defaultRolle = new RolleModel();
            defaultRolle.AdminFlag = false;
            defaultRolle.ManagerFlag = false;
            _rolle = defaultRolle;
            _delete = Delete;
            _login = login;
            _bascetWindowsFactory = bascetWindowsFactory;
             _filterValueList = new ObservableCollection<FilterValue>();
            {
                //Добавление пользователей
                User admin=new User();
                admin.Login = "root";
                admin.Password = "111";
                admin.RolleFlag = true;
                DatabaseLocator.Context.Users.Add(admin);
                //Добавление единиц измерения
                UOfM Kg = new UOfM("Кг");
                DatabaseLocator.Context.UOfMs.Add(Kg);
                UOfM G = new UOfM("Г");
                DatabaseLocator.Context.UOfMs.Add(G);
                UOfM Mg = new UOfM("Мг");
                DatabaseLocator.Context.UOfMs.Add(Mg);
                UOfM M = new UOfM("М");
                DatabaseLocator.Context.UOfMs.Add(M);
                UOfM Mm = new UOfM("Мм");
                DatabaseLocator.Context.UOfMs.Add(Mm);
                UOfM piece = new UOfM("Шт");
                DatabaseLocator.Context.UOfMs.Add(piece);
                UOfM units = new UOfM("Ед");
                DatabaseLocator.Context.UOfMs.Add(units);
                UOfM null_point = new UOfM("-");
                DatabaseLocator.Context.UOfMs.Add(null_point);
                DatabaseLocator.Context.SaveChanges();
                //Добавление типов
                ProductType laboratory_beaker = new ProductType("Лабораторный стакан");
                DatabaseLocator.Context.ProductTypes.Add(laboratory_beaker);
                ProductType test_tube = new ProductType("Пробирка");
                DatabaseLocator.Context.ProductTypes.Add(test_tube);
                ProductType measuring_cup = new ProductType("Мерный цилиндр");
                DatabaseLocator.Context.ProductTypes.Add(measuring_cup);
                ProductType bunsen_flask = new ProductType("Колба Бунзена");
                DatabaseLocator.Context.ProductTypes.Add(bunsen_flask);
                ProductType flask = new ProductType("Колба");
                DatabaseLocator.Context.ProductTypes.Add(flask);
                ProductType bowl = new ProductType("Чаша");
                DatabaseLocator.Context.ProductTypes.Add(bowl);
                //Добавление Характеристик
                CharacteristicsName graduation = new CharacteristicsName("Градуировка",true);
                DatabaseLocator.Context.CharacteristicsNames.Add(graduation);
                CharacteristicsName presence_of_a_spout = new CharacteristicsName("Наличие носика", true);
                DatabaseLocator.Context.CharacteristicsNames.Add(presence_of_a_spout);
                CharacteristicsName division_price = new CharacteristicsName("Цена деления",false);
                DatabaseLocator.Context.CharacteristicsNames.Add(division_price);
                CharacteristicsName slot = new CharacteristicsName("Шлиф",false);
                DatabaseLocator.Context.CharacteristicsNames.Add(slot);
                CharacteristicsName small_slot = new CharacteristicsName("Малый шлиф",false);
                DatabaseLocator.Context.CharacteristicsNames.Add(small_slot);
                CharacteristicsName upper_diameter = new CharacteristicsName("Верхний диаметр",false);
                DatabaseLocator.Context.CharacteristicsNames.Add(upper_diameter);
                CharacteristicsName lower_diameter = new CharacteristicsName("Нижний диаметр",false);
                DatabaseLocator.Context.CharacteristicsNames.Add(lower_diameter);
                //Добавление Характеристик типов
                {
                    ListOfCharacteristics laboratory_beaker__graduation = new ListOfCharacteristics();
                    laboratory_beaker__graduation.ProductTypeName = laboratory_beaker;
                    laboratory_beaker__graduation.CharacteristicsName = graduation;
                    DatabaseLocator.Context.ListsOfCharacteristics.Add(laboratory_beaker__graduation);
                    ListOfCharacteristics laboratory_beaker__presence_of_a_spout = new ListOfCharacteristics();
                    laboratory_beaker__presence_of_a_spout.ProductTypeName = laboratory_beaker;
                    laboratory_beaker__presence_of_a_spout.CharacteristicsName = presence_of_a_spout;
                    DatabaseLocator.Context.ListsOfCharacteristics.Add(laboratory_beaker__presence_of_a_spout);
                    ListOfCharacteristics laboratory_beaker__slot = new ListOfCharacteristics();
                    laboratory_beaker__slot.ProductTypeName = laboratory_beaker;
                    laboratory_beaker__slot.CharacteristicsName = slot;
                    DatabaseLocator.Context.ListsOfCharacteristics.Add(laboratory_beaker__slot);
                    //
                    ListOfCharacteristics test_tube__slot = new ListOfCharacteristics();
                    test_tube__slot.ProductTypeName = test_tube;
                    test_tube__slot.CharacteristicsName = slot;
                    DatabaseLocator.Context.ListsOfCharacteristics.Add(test_tube__slot);
                    //
                    ListOfCharacteristics measuring_cup__slot = new ListOfCharacteristics();
                    measuring_cup__slot.ProductTypeName = measuring_cup;
                    measuring_cup__slot.CharacteristicsName = slot;
                    DatabaseLocator.Context.ListsOfCharacteristics.Add(measuring_cup__slot);
                    ListOfCharacteristics measuring_cup__graduation = new ListOfCharacteristics();
                    measuring_cup__graduation.ProductTypeName = measuring_cup;
                    measuring_cup__graduation.CharacteristicsName = graduation;
                    DatabaseLocator.Context.ListsOfCharacteristics.Add(measuring_cup__graduation);
                    ListOfCharacteristics measuring_cup__presence_of_a_spout = new ListOfCharacteristics();
                    measuring_cup__presence_of_a_spout.ProductTypeName = measuring_cup;
                    measuring_cup__presence_of_a_spout.CharacteristicsName = presence_of_a_spout;
                    DatabaseLocator.Context.ListsOfCharacteristics.Add(measuring_cup__graduation);
                    ListOfCharacteristics measuring_cup__division_price = new ListOfCharacteristics();
                    measuring_cup__division_price.ProductTypeName = measuring_cup;
                    measuring_cup__division_price.CharacteristicsName = division_price;
                    DatabaseLocator.Context.ListsOfCharacteristics.Add(measuring_cup__division_price);
                    //
                    ListOfCharacteristics bunsen_flask__slot = new ListOfCharacteristics();
                    bunsen_flask__slot.ProductTypeName = bunsen_flask;
                    bunsen_flask__slot.CharacteristicsName = slot;
                    DatabaseLocator.Context.ListsOfCharacteristics.Add(bunsen_flask__slot);
                    ListOfCharacteristics bunsen_flask__small_slot = new ListOfCharacteristics();
                    bunsen_flask__small_slot.ProductTypeName = bunsen_flask;
                    bunsen_flask__small_slot.CharacteristicsName = small_slot;
                    DatabaseLocator.Context.ListsOfCharacteristics.Add(bunsen_flask__small_slot);
                    //
                    ListOfCharacteristics flask__slot = new ListOfCharacteristics();
                    flask__slot.ProductTypeName = flask;
                    flask__slot.CharacteristicsName = slot;
                    DatabaseLocator.Context.ListsOfCharacteristics.Add(flask__slot);
                    ListOfCharacteristics flask__graduation = new ListOfCharacteristics();
                    flask__graduation.ProductTypeName = flask;
                    flask__graduation.CharacteristicsName = graduation;
                    DatabaseLocator.Context.ListsOfCharacteristics.Add(flask__graduation);
                    ListOfCharacteristics flask__presence_of_a_spout = new ListOfCharacteristics();
                    flask__presence_of_a_spout.ProductTypeName = flask;
                    flask__presence_of_a_spout.CharacteristicsName = presence_of_a_spout;
                    DatabaseLocator.Context.ListsOfCharacteristics.Add(flask__presence_of_a_spout);
                    //
                    ListOfCharacteristics bowl__presence_of_a_spout = new ListOfCharacteristics();
                    bowl__presence_of_a_spout.ProductTypeName = bowl;
                    bowl__presence_of_a_spout.CharacteristicsName = presence_of_a_spout;
                    DatabaseLocator.Context.ListsOfCharacteristics.Add(bowl__presence_of_a_spout);
                    ListOfCharacteristics bowl__upper_diameter = new ListOfCharacteristics();
                    bowl__upper_diameter.ProductTypeName = bowl;
                    bowl__upper_diameter.CharacteristicsName = upper_diameter;
                    DatabaseLocator.Context.ListsOfCharacteristics.Add(bowl__upper_diameter);
                    ListOfCharacteristics bowl__lower_diameter = new ListOfCharacteristics();
                    bowl__lower_diameter.ProductTypeName = bowl;
                    bowl__lower_diameter.CharacteristicsName = lower_diameter;
                    DatabaseLocator.Context.ListsOfCharacteristics.Add(bowl__lower_diameter);
                }
                //Добавление продуктов
                {
                    Product laboratory_beaker1 =
                        new Product("Стакан лабораторный высокий В - 2 - 50, 50 мл, без носика(ГОСТ 25336 - 82)",
                            "Безцветное", "", "ТС", 0, 0, 0, 0, "Россия",
                            @"C:\\Users\\bokov\\OneDrive\\Рабочий стол\\Фотки для БД\\Screenshot_1.jpg",false);
                    laboratory_beaker1.Type = laboratory_beaker;
                    DatabaseLocator.Context.Products.Add(laboratory_beaker1);
                    Product laboratory_beaker2 =
                        new Product("Стакан лабораторный высокий В-1-50, 50 мл, с носиком (ГОСТ 25336-82)",
                            "Безцветное", "",
                            "ТС", 0, 0, 0, 0, "Россия",
                            @"C:\\Users\\bokov\\OneDrive\\Рабочий стол\\Фотки для БД\\Screenshot_2.jpg", false);
                    laboratory_beaker2.Type = laboratory_beaker;
                    DatabaseLocator.Context.Products.Add(laboratory_beaker2);
                    Product laboratory_beaker3 =
                        new Product("Стакан лабораторный высокий В-1-250, 250 мл, с носиком (ГОСТ 25336-82)",
                            "Безцветное", "",
                            "ТС", 0, 0, 0, 0, "Россия",
                            @"C:\\Users\\bokov\\OneDrive\\Рабочий стол\\Фотки для БД\\Screenshot_3.jpg", false);
                    laboratory_beaker3.Type = laboratory_beaker;
                    DatabaseLocator.Context.Products.Add(laboratory_beaker3);
                    //
                    Product test_tube1 =
                        new Product(
                            "Пробирка лабораторная, 6 мл, цилиндрические, кварцевое стекло, без пробки и делений",
                            "Безцветное", "", "ТС", 0, 0, 0, 0, "Россия",
                            @"C:\\Users\\bokov\\OneDrive\\Рабочий стол\\Фотки для БД\\Screenshot_4.jpg", false);
                    test_tube1.Type = test_tube;
                    DatabaseLocator.Context.Products.Add(test_tube1);
                    Product test_tube2 =
                        new Product(
                            "Пробирка лабораторная, 145 мл, цилиндрические, кварцевое стекло, без пробки и делений",
                            "Безцветное", "", "ТС", 0, 0, 0, 0, "Россия",
                            @"C:\\Users\\bokov\\OneDrive\\Рабочий стол\\Фотки для БД\\Screenshot_4.jpg", false);
                    test_tube2.Type = test_tube;
                    DatabaseLocator.Context.Products.Add(test_tube2);
                    Product test_tube3 = new Product(
                        "Пробирка лабораторная, 80 мл, цилиндрические, фторопласт 1-го сорта, без пробки и делений",
                        "Безцветное", "", "ТС", 0, 0, 0, 0, "Россия",
                        @"C:\\Users\\bokov\\OneDrive\\Рабочий стол\\Фотки для БД\\Screenshot_5.jpg", false);
                    test_tube3.Type = test_tube;
                    DatabaseLocator.Context.Products.Add(test_tube3);
                    //
                    Product measuring_cup1 =
                        new Product("Цилиндр мерный 1-50-2, 50 мл, со стеклянным основанием, с носиком, (ГОСТ 1770-74)",
                            "Безцветное", "", "ТС", 0, 0, 0, 0, "Россия",
                            @"C:\\Users\\bokov\\OneDrive\\Рабочий стол\\Фотки для БД\\Screenshot_6.jpg", false);
                    measuring_cup1.Type = measuring_cup;
                    DatabaseLocator.Context.Products.Add(measuring_cup1);
                    Product measuring_cup2 =
                        new Product(
                            "Цилиндр мерный 1-100-2, 100 мл, со стеклянным основанием, с носиком, (ГОСТ 1770-74)",
                            "Безцветное", "", "ТС", 0, 0, 0, 0, "Россия",
                            @"C:\\Users\\bokov\\OneDrive\\Рабочий стол\\Фотки для БД\\Screenshot_7.jpg", false);
                    measuring_cup2.Type = measuring_cup;
                    DatabaseLocator.Context.Products.Add(measuring_cup2);
                    Product measuring_cup3 = new Product(
                        "Цилиндр мерный 1-1000-2, 1000 мл, со стеклянным основанием, с носиком, белая шкала, (ГОСТ 1770-74)",
                        "Безцветное", "", "ТС", 0, 0, 0, 0, "Россия",
                        @"C:\\Users\\bokov\\OneDrive\\Рабочий стол\\Фотки для БД\\Screenshot_7.jpg", false);
                    measuring_cup3.Type = measuring_cup;
                    DatabaseLocator.Context.Products.Add(measuring_cup3);
                    //
                    Product bunsen_flask1 = new Product("Колба Бунзена, 1-250, 250 мл, без шлифа (ГОСТ 25336-82)",
                        "Безцветное",
                        "", "ТС", 0, 0, 0, 0, "Россия",
                        @"C:\\Users\\bokov\\OneDrive\\Рабочий стол\\Фотки для БД\\Screenshot_8.jpg", false);
                    bunsen_flask1.Type = bunsen_flask;
                    DatabaseLocator.Context.Products.Add(bunsen_flask1);
                    Product bunsen_flask2 = new Product("Колба Бунзена, 1-250, 250 мл, шлиф 29/32 (ГОСТ 25336-82)",
                        "Безцветное", "", "ТС", 0, 0, 0, 0, "Россия",
                        @"C:\\Users\\bokov\\OneDrive\\Рабочий стол\\Фотки для БД\\Screenshot_9.jpg", false);
                    bunsen_flask2.Type = bunsen_flask;
                    DatabaseLocator.Context.Products.Add(bunsen_flask2);
                    Product bunsen_flask3 = new Product("Колба Бунзена, 1-2000, 2000 мл, без шлифа (ГОСТ 25336-82)",
                        "Безцветное", "", "ТС", 0, 0, 0, 0, "Россия",
                        @"C:\\Users\\bokov\\OneDrive\\Рабочий стол\\Фотки для БД\\Screenshot_10.jpg", false);
                    bunsen_flask3.Type = bunsen_flask;
                    DatabaseLocator.Context.Products.Add(bunsen_flask3);
                    //
                    Product flask1 = new Product("Колба коническая КН-1-10, 10 мл, шлиф 14/23 (ГОСТ 25336-82)",
                        "Безцветное",
                        "", "ТС", 0, 0, 0, 0, "Россия",
                        @"C:\\Users\\bokov\\OneDrive\\Рабочий стол\\Фотки для БД\\Screenshot_11.jpg", false);
                    flask1.Type = flask;
                    DatabaseLocator.Context.Products.Add(flask1);
                    Product flask2 = new Product(
                        "Колба коническая КН-2-100-22, 100 мл, горло 22 мм, без шлифа (ГОСТ 25336-82)",
                        "Безцветное", "", "ТС", 0, 0, 0, 0, "Россия",
                        @"C:\\Users\\bokov\\OneDrive\\Рабочий стол\\Фотки для БД\\Screenshot_12.jpg", false);
                    flask2.Type = flask;
                    DatabaseLocator.Context.Products.Add(flask2);
                    Product flask3 = new Product("Колба коническая КН-1-50, 50 мл, шлиф 19/26 (ГОСТ 25336-82)",
                        "Безцветное",
                        "", "ТС", 0, 0, 0, 0, "Россия",
                        @"C:\\Users\\bokov\\OneDrive\\Рабочий стол\\Фотки для БД\\Screenshot_13.jpg", false);
                    flask3.Type = flask;
                    DatabaseLocator.Context.Products.Add(flask3);
                    //
                    Product bowl1 = new Product("Чаша для выпаривания, 20 мл, кварцевое стекло", "Безцветное", "", "ТС",
                        0, 0,
                        0, 0, "Россия", @"C:\\Users\\bokov\\OneDrive\\Рабочий стол\\Фотки для БД\\Screenshot_14.jpg", false);
                    bowl1.Type = bowl;
                    DatabaseLocator.Context.Products.Add(bowl1);
                    Product bowl2 = new Product("Чаша для выпаривания, 40 мл, кварцевое стекло", "Безцветное", "", "ТС",
                        0, 0,
                        0, 0, "Россия", @"C:\\Users\\bokov\\OneDrive\\Рабочий стол\\Фотки для БД\\Screenshot_14.jpg", false);
                    bowl2.Type = bowl;
                    DatabaseLocator.Context.Products.Add(bowl2);
                    Product bowl3 = new Product("Чаша для выпаривания, 200 мл, кварцевое стекло", "Безцветное", "",
                        "ТС", 0, 0,
                        0, 0, "Россия", @"C:\\Users\\bokov\\OneDrive\\Рабочий стол\\Фотки для БД\\Screenshot_15.jpg", false);
                    bowl3.Type = bowl;
                    DatabaseLocator.Context.Products.Add(bowl3);
                    
                    //Спецификации
                    {
                        Specifications laboratory_beaker1_slot = new Specifications();
                        laboratory_beaker1_slot.UOfM = Mm;
                        laboratory_beaker1_slot.CharacteristicsName = slot;
                        laboratory_beaker1_slot.Product = laboratory_beaker1;
                        laboratory_beaker1_slot.Num = "10";
                        DatabaseLocator.Context.Specification.Add(laboratory_beaker1_slot);
                        Specifications laboratory_beaker1_graduation = new Specifications();
                        laboratory_beaker1_graduation.UOfM = null_point;
                        laboratory_beaker1_graduation.CharacteristicsName = graduation;
                        laboratory_beaker1_graduation.Product = laboratory_beaker1;
                        laboratory_beaker1_graduation.Num = "Да";
                        DatabaseLocator.Context.Specification.Add(laboratory_beaker1_graduation);
                        Specifications laboratory_beaker1_presence_of_a_spout = new Specifications();
                        laboratory_beaker1_presence_of_a_spout.UOfM = null_point;
                        laboratory_beaker1_presence_of_a_spout.CharacteristicsName = presence_of_a_spout;
                        laboratory_beaker1_presence_of_a_spout.Product = laboratory_beaker1;
                        laboratory_beaker1_presence_of_a_spout.Num = "Да";
                        DatabaseLocator.Context.Specification.Add(laboratory_beaker1_presence_of_a_spout);
                        //
                        Specifications laboratory_beaker2_slot = new Specifications();
                        laboratory_beaker2_slot.UOfM = Mm;
                        laboratory_beaker2_slot.CharacteristicsName = slot;
                        laboratory_beaker2_slot.Product = laboratory_beaker2;
                        laboratory_beaker2_slot.Num = "15";
                        Specifications laboratory_beaker2_graduation = new Specifications();
                        laboratory_beaker2_graduation.UOfM = null_point;
                        laboratory_beaker2_graduation.CharacteristicsName = graduation;
                        laboratory_beaker2_graduation.Product = laboratory_beaker2;
                        laboratory_beaker2_graduation.Num = "Да";
                        DatabaseLocator.Context.Specification.Add(laboratory_beaker2_graduation);
                        Specifications laboratory_beaker2_presence_of_a_spout = new Specifications();
                        laboratory_beaker2_presence_of_a_spout.UOfM = null_point;
                        laboratory_beaker2_presence_of_a_spout.CharacteristicsName = presence_of_a_spout;
                        laboratory_beaker2_presence_of_a_spout.Product = laboratory_beaker2;
                        laboratory_beaker2_presence_of_a_spout.Num = "Да";
                        DatabaseLocator.Context.Specification.Add(laboratory_beaker2_presence_of_a_spout);
                        //
                        Specifications laboratory_beaker3_slot = new Specifications();
                        laboratory_beaker3_slot.UOfM = Mm;
                        laboratory_beaker3_slot.CharacteristicsName = slot;
                        laboratory_beaker3_slot.Product = laboratory_beaker3;
                        laboratory_beaker3_slot.Num = "20";
                        DatabaseLocator.Context.Specification.Add(laboratory_beaker3_slot);
                        Specifications laboratory_beaker3_graduation = new Specifications();
                        laboratory_beaker3_graduation.UOfM = null_point;
                        laboratory_beaker3_graduation.CharacteristicsName = graduation;
                        laboratory_beaker3_graduation.Product = laboratory_beaker3;
                        laboratory_beaker3_graduation.Num = "Да";
                        DatabaseLocator.Context.Specification.Add(laboratory_beaker3_graduation);
                        Specifications laboratory_beaker3_presence_of_a_spout = new Specifications();
                        laboratory_beaker3_presence_of_a_spout.UOfM = null_point;
                        laboratory_beaker3_presence_of_a_spout.CharacteristicsName = presence_of_a_spout;
                        laboratory_beaker3_presence_of_a_spout.Product = laboratory_beaker3;
                        laboratory_beaker3_presence_of_a_spout.Num = "Да";
                        DatabaseLocator.Context.Specification.Add(laboratory_beaker3_presence_of_a_spout);
                    }
                    {
                        Specifications test_tube1__slot = new Specifications();
                        test_tube1__slot.UOfM = Mm;
                        test_tube1__slot.CharacteristicsName = slot;
                        test_tube1__slot.Product = test_tube1;
                        test_tube1__slot.Num = "8";
                        DatabaseLocator.Context.Specification.Add(test_tube1__slot);
                        Specifications test_tube2__slot = new Specifications();
                        test_tube2__slot.UOfM = Mm;
                        test_tube2__slot.CharacteristicsName = slot;
                        test_tube2__slot.Product = test_tube2;
                        test_tube2__slot.Num = "5";
                        DatabaseLocator.Context.Specification.Add(test_tube2__slot);
                        Specifications test_tube3__slot = new Specifications();
                        test_tube3__slot.UOfM = Mm;
                        test_tube3__slot.CharacteristicsName = slot;
                        test_tube3__slot.Product = test_tube3;
                        test_tube3__slot.Num = "10";
                        DatabaseLocator.Context.Specification.Add(test_tube3__slot);
                    }
                    {
                        Specifications measuring_cup1_slot = new Specifications();
                        measuring_cup1_slot.UOfM = Mm;
                        measuring_cup1_slot.CharacteristicsName = slot;
                        measuring_cup1_slot.Product = measuring_cup1;
                        measuring_cup1_slot.Num = "20";
                        DatabaseLocator.Context.Specification.Add(measuring_cup1_slot);
                        Specifications measuring_cup1_graduation = new Specifications();
                        measuring_cup1_graduation.UOfM = null_point;
                        measuring_cup1_graduation.CharacteristicsName = graduation;
                        measuring_cup1_graduation.Product = measuring_cup1;
                        measuring_cup1_graduation.Num = "Да";
                        DatabaseLocator.Context.Specification.Add(measuring_cup1_graduation);
                        Specifications measuring_cup1_presence_of_a_spout = new Specifications();
                        measuring_cup1_presence_of_a_spout.UOfM = null_point;
                        measuring_cup1_presence_of_a_spout.CharacteristicsName = presence_of_a_spout;
                        measuring_cup1_presence_of_a_spout.Product = measuring_cup1;
                        measuring_cup1_presence_of_a_spout.Num = "Нет";
                        DatabaseLocator.Context.Specification.Add(measuring_cup1_presence_of_a_spout);
                        Specifications measuring_cup1_division_price = new Specifications();
                        measuring_cup1_division_price.UOfM = Mm;
                        measuring_cup1_division_price.CharacteristicsName = division_price;
                        measuring_cup1_division_price.Product = measuring_cup1;
                        measuring_cup1_division_price.Num = "1";
                        DatabaseLocator.Context.Specification.Add(measuring_cup1_division_price);
                        //
                        Specifications measuring_cup2_slot = new Specifications();
                        measuring_cup2_slot.UOfM = Mm;
                        measuring_cup2_slot.CharacteristicsName = slot;
                        measuring_cup2_slot.Product = measuring_cup2;
                        measuring_cup2_slot.Num = "30";
                        DatabaseLocator.Context.Specification.Add(measuring_cup2_slot);
                        Specifications measuring_cup2_graduation = new Specifications();
                        measuring_cup2_graduation.UOfM = null_point;
                        measuring_cup2_graduation.CharacteristicsName = graduation;
                        measuring_cup2_graduation.Product = measuring_cup2;
                        measuring_cup2_graduation.Num = "Нет";
                        DatabaseLocator.Context.Specification.Add(measuring_cup2_graduation);
                        Specifications measuring_cup2_presence_of_a_spout = new Specifications();
                        measuring_cup2_presence_of_a_spout.UOfM = null_point;
                        measuring_cup2_presence_of_a_spout.CharacteristicsName = presence_of_a_spout;
                        measuring_cup2_presence_of_a_spout.Product = measuring_cup2;
                        measuring_cup2_presence_of_a_spout.Num = "Да";
                        DatabaseLocator.Context.Specification.Add(measuring_cup2_presence_of_a_spout);
                        Specifications measuring_cup2_division_price = new Specifications();
                        measuring_cup2_division_price.UOfM = Mm;
                        measuring_cup2_division_price.CharacteristicsName = division_price;
                        measuring_cup2_division_price.Product = measuring_cup2;
                        measuring_cup2_division_price.Num = "1";
                        DatabaseLocator.Context.Specification.Add(measuring_cup2_division_price);
                        //
                        Specifications measuring_cup3_slot = new Specifications();
                        measuring_cup3_slot.UOfM = Mm;
                        measuring_cup3_slot.CharacteristicsName = slot;
                        measuring_cup3_slot.Product = measuring_cup2;
                        measuring_cup3_slot.Num = "40";
                        DatabaseLocator.Context.Specification.Add(measuring_cup3_slot);
                        Specifications measuring_cup3_graduation = new Specifications();
                        measuring_cup3_graduation.UOfM = null_point;
                        measuring_cup3_graduation.CharacteristicsName = graduation;
                        measuring_cup3_graduation.Product = measuring_cup3;
                        measuring_cup3_graduation.Num = "Нет";
                        DatabaseLocator.Context.Specification.Add(measuring_cup3_graduation);
                        Specifications measuring_cup3_presence_of_a_spout = new Specifications();
                        measuring_cup3_presence_of_a_spout.UOfM = null_point;
                        measuring_cup3_presence_of_a_spout.CharacteristicsName = presence_of_a_spout;
                        measuring_cup3_presence_of_a_spout.Product = measuring_cup3;
                        measuring_cup3_presence_of_a_spout.Num = "Да";
                        DatabaseLocator.Context.Specification.Add(measuring_cup3_presence_of_a_spout);
                        Specifications measuring_cup3_division_price = new Specifications();
                        measuring_cup3_division_price.UOfM = Mm;
                        measuring_cup3_division_price.CharacteristicsName = division_price;
                        measuring_cup3_division_price.Product = measuring_cup3;
                        measuring_cup3_division_price.Num = "1";
                        DatabaseLocator.Context.Specification.Add(measuring_cup3_division_price);
                    }
                    {
                        Specifications bunsen_flask1_slot = new Specifications();
                        bunsen_flask1_slot.UOfM = Mm;
                        bunsen_flask1_slot.CharacteristicsName = slot;
                        bunsen_flask1_slot.Product = bunsen_flask1;
                        bunsen_flask1_slot.Num = "20";
                        DatabaseLocator.Context.Specification.Add(bunsen_flask1_slot);
                        Specifications bunsen_flask1_small_slot = new Specifications();
                        bunsen_flask1_small_slot.UOfM = Mm;
                        bunsen_flask1_small_slot.CharacteristicsName = small_slot;
                        bunsen_flask1_small_slot.Product = bunsen_flask1;
                        bunsen_flask1_small_slot.Num = "8";
                        DatabaseLocator.Context.Specification.Add(bunsen_flask1_small_slot);
                        //
                        Specifications bunsen_flask2_slot = new Specifications();
                        bunsen_flask2_slot.UOfM = Mm;
                        bunsen_flask2_slot.CharacteristicsName = slot;
                        bunsen_flask2_slot.Product = bunsen_flask2;
                        bunsen_flask2_slot.Num = "40";
                        DatabaseLocator.Context.Specification.Add(bunsen_flask2_slot);
                        Specifications bunsen_flask2_small_slot = new Specifications();
                        bunsen_flask2_small_slot.UOfM = Mm;
                        bunsen_flask2_small_slot.CharacteristicsName = small_slot;
                        bunsen_flask2_small_slot.Product = bunsen_flask2;
                        bunsen_flask2_small_slot.Num = "3";
                        DatabaseLocator.Context.Specification.Add(bunsen_flask2_small_slot);
                        //
                        Specifications bunsen_flask3_slot = new Specifications();
                        bunsen_flask3_slot.UOfM = Mm;
                        bunsen_flask3_slot.CharacteristicsName = slot;
                        bunsen_flask3_slot.Product = bunsen_flask3;
                        bunsen_flask3_slot.Num = "30";
                        DatabaseLocator.Context.Specification.Add(bunsen_flask3_slot);
                        Specifications bunsen_flask3_small_slot = new Specifications();
                        bunsen_flask3_small_slot.UOfM = Mm;
                        bunsen_flask3_small_slot.CharacteristicsName = small_slot;
                        bunsen_flask3_small_slot.Product = bunsen_flask3;
                        bunsen_flask3_small_slot.Num = "5";
                        DatabaseLocator.Context.Specification.Add(bunsen_flask3_small_slot);
                    }
                    {
                        Specifications flask1_slot = new Specifications();
                        flask1_slot.UOfM = Mm;
                        flask1_slot.CharacteristicsName = slot;
                        flask1_slot.Product = flask1;
                        flask1_slot.Num = "10";
                        DatabaseLocator.Context.Specification.Add(flask1_slot);
                        Specifications flask1_graduation = new Specifications();
                        flask1_graduation.UOfM = null_point;
                        flask1_graduation.CharacteristicsName = graduation;
                        flask1_graduation.Product = flask1;
                        flask1_graduation.Num = "Да";
                        DatabaseLocator.Context.Specification.Add(flask1_graduation);
                        Specifications flask1_presence_of_a_spout = new Specifications();
                        flask1_presence_of_a_spout.UOfM = null_point;
                        flask1_presence_of_a_spout.CharacteristicsName = presence_of_a_spout;
                        flask1_presence_of_a_spout.Product = flask1;
                        flask1_presence_of_a_spout.Num = "Да";
                        DatabaseLocator.Context.Specification.Add(flask1_presence_of_a_spout);
                        //
                        Specifications flask2_slot = new Specifications();
                        flask2_slot.UOfM = Mm;
                        flask2_slot.CharacteristicsName = slot;
                        flask2_slot.Product = flask2;
                        flask2_slot.Num = "15";
                        Specifications flask2_graduation = new Specifications();
                        flask2_graduation.UOfM = null_point;
                        flask2_graduation.CharacteristicsName = graduation;
                        flask2_graduation.Product = flask2;
                        flask2_graduation.Num = "Да";
                        DatabaseLocator.Context.Specification.Add(flask2_graduation);
                        Specifications flask2_presence_of_a_spout = new Specifications();
                        flask2_presence_of_a_spout.UOfM = null_point;
                        flask2_presence_of_a_spout.CharacteristicsName = presence_of_a_spout;
                        flask2_presence_of_a_spout.Product = flask2;
                        flask2_presence_of_a_spout.Num = "Да";
                        DatabaseLocator.Context.Specification.Add(flask2_presence_of_a_spout);
                        //
                        Specifications flask3_slot = new Specifications();
                        flask3_slot.UOfM = Mm;
                        flask3_slot.CharacteristicsName = slot;
                        flask3_slot.Product = flask3;
                        flask3_slot.Num = "20";
                        DatabaseLocator.Context.Specification.Add(flask3_slot);
                        Specifications flask3_graduation = new Specifications();
                        flask3_graduation.UOfM = null_point;
                        flask3_graduation.CharacteristicsName = graduation;
                        flask3_graduation.Product = flask3;
                        flask3_graduation.Num = "Да";
                        DatabaseLocator.Context.Specification.Add(flask3_graduation);
                        Specifications flask3_presence_of_a_spout = new Specifications();
                        flask3_presence_of_a_spout.UOfM = null_point;
                        flask3_presence_of_a_spout.CharacteristicsName = presence_of_a_spout;
                        flask3_presence_of_a_spout.Product = flask3;
                        flask3_presence_of_a_spout.Num = "Да";
                        DatabaseLocator.Context.Specification.Add(flask3_presence_of_a_spout);
                    }
                    {
                        Specifications bowl1_upper_diameter = new Specifications();
                        bowl1_upper_diameter.UOfM = Mm;
                        bowl1_upper_diameter.CharacteristicsName = upper_diameter;
                        bowl1_upper_diameter.Product = bowl1;
                        bowl1_upper_diameter.Num = "10";
                        DatabaseLocator.Context.Specification.Add(bowl1_upper_diameter);
                        Specifications bowl1_lower_diameter = new Specifications();
                        bowl1_lower_diameter.UOfM = Mm;
                        bowl1_lower_diameter.CharacteristicsName = lower_diameter;
                        bowl1_lower_diameter.Product = bowl1;
                        bowl1_lower_diameter.Num = "2";
                        DatabaseLocator.Context.Specification.Add(bowl1_lower_diameter);
                        Specifications bowl1_presence_of_a_spout = new Specifications();
                        bowl1_presence_of_a_spout.UOfM = null_point;
                        bowl1_presence_of_a_spout.CharacteristicsName = presence_of_a_spout;
                        bowl1_presence_of_a_spout.Product = bowl1;
                        bowl1_presence_of_a_spout.Num = "Да";
                        DatabaseLocator.Context.Specification.Add(bowl1_presence_of_a_spout);
                        //
                        Specifications bowl2_upper_diameter = new Specifications();
                        bowl2_upper_diameter.UOfM = Mm;
                        bowl2_upper_diameter.CharacteristicsName = upper_diameter;
                        bowl2_upper_diameter.Product = bowl2;
                        bowl2_upper_diameter.Num = "15";
                        DatabaseLocator.Context.Specification.Add(bowl2_upper_diameter);
                        Specifications bowl2_lower_diameter = new Specifications();
                        bowl2_lower_diameter.UOfM = Mm;
                        bowl2_lower_diameter.CharacteristicsName = lower_diameter;
                        bowl2_lower_diameter.Product = bowl2;
                        bowl2_lower_diameter.Num = "3";
                        DatabaseLocator.Context.Specification.Add(bowl2_lower_diameter);
                        Specifications bowl2_presence_of_a_spout = new Specifications();
                        bowl2_presence_of_a_spout.UOfM = null_point;
                        bowl2_presence_of_a_spout.CharacteristicsName = presence_of_a_spout;
                        bowl2_presence_of_a_spout.Product = bowl2;
                        bowl2_presence_of_a_spout.Num = "Нет";
                        DatabaseLocator.Context.Specification.Add(bowl2_presence_of_a_spout);
                        //
                        Specifications bowl3_upper_diameter = new Specifications();
                        bowl3_upper_diameter.UOfM = Mm;
                        bowl3_upper_diameter.CharacteristicsName = upper_diameter;
                        bowl3_upper_diameter.Product = bowl3;
                        bowl3_upper_diameter.Num = "20";
                        DatabaseLocator.Context.Specification.Add(bowl3_upper_diameter);
                        Specifications bowl3_lower_diameter = new Specifications();
                        bowl3_lower_diameter.UOfM = null_point;
                        bowl3_lower_diameter.CharacteristicsName = lower_diameter;
                        bowl3_lower_diameter.Product = bowl3;
                        bowl3_lower_diameter.Num = "8";
                        DatabaseLocator.Context.Specification.Add(bowl3_lower_diameter);
                        Specifications bowl3_presence_of_a_spout = new Specifications();
                        bowl3_presence_of_a_spout.UOfM = null_point;
                        bowl3_presence_of_a_spout.CharacteristicsName = presence_of_a_spout;
                        bowl3_presence_of_a_spout.Product = bowl3;
                        bowl3_presence_of_a_spout.Num = "Да";
                        DatabaseLocator.Context.Specification.Add(bowl3_presence_of_a_spout);
                    }
                }
           
            }
            DatabaseLocator.Context.SaveChanges();
            _productWindowsFactory = productWindowsFactory;
            _authorizationFactory = authorizationFactory;
            _adminWindowFactory = adminWindowFactory;
            _editProductWindowsFactory = editProductWindowsFactory;
            _addWindowsFactory= addWindowsFactory;
            _changeBasket = basketChange;
            _login.Login += LoginSuccess;
            _adminClose= adminClose;
            _adminClose.AdminClose += _adminClose_AdminClose;
            _delete.Delete += _adminClose_AdminClose;
            _changeBasket.ChangeBasketLogin += _changeBasket_ChangeBasketLogin; 

        }

        private void _adminClose_AdminClose(object? sender, EventArgs e)
        {
            ProductTypes=DatabaseLocator.Context.ProductTypes.ToList();
            Products = DatabaseLocator.Context.Products.ToObservableCollection();
            OnPropertyChanged(nameof(ProductTypes));
            OnPropertyChanged(nameof(Products));
        }
        private void _changeBasket_ChangeBasketLogin(object? sender, ChangeBasketEventArgs e)
        {
            var busket = e.item;
            Products.Clear();
            OnPropertyChanged(nameof(Products));
            Products = DatabaseLocator.Context.Products.ToObservableCollection();
            foreach (var elem in Products)
            {
                elem.buscetFlag = false;
           
            }

            foreach (var elem in Products)
            {
                foreach (var busketElem in busket)
                {
                    if (elem == busketElem.Product)
                    {
                        elem.buscetFlag = true;
                        break;
                    }
                }
            }

            if (selectedType != null)
            {
                Products = Products.Where(p => p.Type == selectedType).ToObservableCollection();
            }


            OnPropertyChanged(nameof(Products));
        }

        private void LoginSuccess(object? sender, LoginEventArgs e)
        {
            _rolle = e.item;
            OnPropertyChanged(nameof(AdminViev));
            _rolle.AdminFlag = !_rolle.AdminFlag;
        }

        public ICommand EnterAccountCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (_rolle.AdminFlag|| _rolle.ManagerFlag)
                    {
                        _adminWindowFactory.CreateAdminWindow(_rolle, _adminClose).Show();
                    }
                    else
                    {
                        _authorizationFactory.CreateAutorizationindow(_login).Show();
                    }
                });
            }
        }

        public ICommand Search
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (_serchText != "")
                    {
                        Products =
                        DatabaseLocator.Context.Products.Where(p => p.Type == selectedType&&p.Name.Contains(_serchText)).ToObservableCollection();
                        OnPropertyChanged(nameof(Products));
                    }

                });
            }
        }
        public ICommand CheckAdminViev
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    _rolle.AdminFlag = !_rolle.AdminFlag;
                });
            }
        }
        public ICommand ProductSort
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Products = DatabaseLocator.Context.Products.Where(p => p.Type == selectedType).ToObservableCollection();
                        {
                        ObservableCollection<Product> tmpProducts = new ObservableCollection<Product>(Products);
                        Products.Clear();
                        var b = DatabaseLocator.Context.Products.Where(u =>  u.Type == selectedType).ToList();
                        List<string> tmp = new List<string>();
                        for (int i = 0; i < _colorCharacteristicList.Count; i++)
                        {
                            if (_colorCharacteristicList[i].CharacteristicFlag)
                            {
                                tmp.Add(_colorCharacteristicList[i].NumName);
                            }
                        }
                        b = b.Where(u => tmp.Contains(u.Color)).ToList();
                        foreach (var elem1 in b)
                        {
                            if (tmpProducts.Contains(elem1))
                            {
                                Products.Add(elem1);
                            }
                        }
                        OnPropertyChanged(nameof(Products));
                    }
                    {
                        ObservableCollection<Product> tmpProducts = new ObservableCollection<Product>(Products);
                        Products.Clear();
                        var b = DatabaseLocator.Context.Products.Where(u => u.Type == selectedType).ToList();
                        List<string> tmp = new List<string>();
                        for (int i = 0; i < _heatResistancecharacteristicList.Count; i++)
                        {
                            if (_heatResistancecharacteristicList[i].CharacteristicFlag)
                            {
                                tmp.Add(_heatResistancecharacteristicList[i].NumName);
                            }
                        }
                        b = b.Where(u => tmp.Contains(u.HeatResistance)).ToList();
                        foreach (var elem1 in b)
                        {
                            if (tmpProducts.Contains(elem1))
                            {
                                Products.Add(elem1);
                            }
                        }
                        OnPropertyChanged(nameof(Products));
                    }
                    {
                        ObservableCollection<Product> tmpProducts = new ObservableCollection<Product>(Products);
                        Products.Clear();
                        var b = DatabaseLocator.Context.Products.Where(u => u.Type == selectedType).ToList();
                        List<string> tmp = new List<string>();
                        for (int i = 0; i < _manufacturerCharacteristicList.Count; i++)
                        {
                            if (_manufacturerCharacteristicList[i].CharacteristicFlag)
                            {
                                tmp.Add(_manufacturerCharacteristicList[i].NumName);
                            }
                        }
                        b = b.Where(u => tmp.Contains(u.manufacturer)).ToList();
                        foreach (var elem1 in b)
                        {
                            if (tmpProducts.Contains(elem1))
                            {
                                Products.Add(elem1);
                            }
                        }
                        OnPropertyChanged(nameof(Products));
                    }
                    {
                        ObservableCollection<Product> tmpProducts = new ObservableCollection<Product>(Products);
                        Products.Clear();
                        var b = DatabaseLocator.Context.Products.Where(u => u.Type == selectedType).ToList();
                        b = b.Where(u => Convert.ToDouble(u.width) <= _widthMaxSliderValue && Convert.ToDouble(u.width) >= _widthMinSliderValue).ToList();
                        foreach (var elem2 in b)
                        {
                            if (tmpProducts.Contains(elem2))
                            {
                                Products.Add(elem2);
                            }
                        }
                        OnPropertyChanged(nameof(Products));
                    }
                    {
                        ObservableCollection<Product> tmpProducts = new ObservableCollection<Product>(Products);
                        Products.Clear();
                        var b = DatabaseLocator.Context.Products.Where(u => u.Type == selectedType).ToList();
                        b = b.Where(u => Convert.ToDouble(u.width) <= _heightMaxSliderValue && Convert.ToDouble(u.width) >= _widthMinSliderValue).ToList();
                        foreach (var elem2 in b)
                        {
                            if (tmpProducts.Contains(elem2))
                            {
                                Products.Add(elem2);
                            }
                        }
                        OnPropertyChanged(nameof(Products));
                    }
                    {
                        ObservableCollection<Product> tmpProducts = new ObservableCollection<Product>(Products);
                        Products.Clear();
                        var b = DatabaseLocator.Context.Products.Where(u => u.Type == selectedType).ToList();
                        b = b.Where(u => Convert.ToDouble(u.width) <= _lengthMaxSliderValue && Convert.ToDouble(u.width) >= _lengthMinSliderValue).ToList();
                        foreach (var elem2 in b)
                        {
                            if (tmpProducts.Contains(elem2))
                            {
                                Products.Add(elem2);
                            }
                        }
                        OnPropertyChanged(nameof(Products));
                    }
                    {
                        ObservableCollection<Product> tmpProducts = new ObservableCollection<Product>(Products);
                        Products.Clear();
                        var b = DatabaseLocator.Context.Products.Where(u => u.Type == selectedType).ToList();
                        b = b.Where(u => Convert.ToDouble(u.width) <= _volumeMaxSliderValue && Convert.ToDouble(u.width) >= _volumeMinSliderValue).ToList();
                        foreach (var elem2 in b)
                        {
                            if (tmpProducts.Contains(elem2))
                            {
                                Products.Add(elem2);
                            }
                        }
                        OnPropertyChanged(nameof(Products));
                    }
                    foreach (var elem in _filterValueList)
                    {
                        ObservableCollection<Product> tmpProducts = new ObservableCollection<Product>(Products);
                        Products.Clear();
                        var a = elem.CurentVM;
                        FilterResult filterResult = a.GetPageResult();
                        if (filterResult.NameVM == nameof(CheckBoxPageVievModel))
                        {
                            var b = DatabaseLocator.Context.Specification.Where(u =>
                                u.CharacteristicsName == elem.CharacteristicsName&&u.Product.Type== selectedType).ToList();
                            List<string> tmp=new List<string>();
                            for (int i=0;i< filterResult.boolResult.Count;i++)
                            {
                                if (filterResult.boolResult[i].CharacteristicFlag)
                                {
                                    tmp.Add(filterResult.boolResult[i].NumName);
                                }
                            }
                            b= b.Where(u => tmp.Contains(u.Num)).ToList();
                            foreach (var elem1 in b)
                            {
                                if (tmpProducts.Contains(elem1.Product))
                                {
                                    Products.Add(elem1.Product);
                                }
                            }
                        }
                        else
                        {
                            var b = DatabaseLocator.Context.Specification.Where(u =>
                                u.CharacteristicsName == elem.CharacteristicsName && u.Product.Type == selectedType).ToList();
                            b = b.Where(u => Convert.ToDouble(u.Num) <= filterResult.maxValue && Convert.ToDouble(u.Num) >= filterResult.minValue).ToList();
                            foreach (var elem2 in b)
                            {
                                if (tmpProducts.Contains(elem2.Product))
                                {
                                    Products.Add(elem2.Product);
                                }
                            }
                            OnPropertyChanged(nameof(Products));
                        }

                    }
                });
            }
        }

        public ICommand CatalogClear
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    SelectedProduct = null;
                    Products = DatabaseLocator.Context.Products.ToObservableCollection();
                    _filterValueList.Clear();
                    OnPropertyChanged(nameof(FilterValueList));
                    OnPropertyChanged(nameof(Products));
                });
            }
        }
        public ICommand AddToBuscet
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    bool flag = true;
                    foreach (var elem in BuscetProducts)
                    {
                        if (elem.Product == SelectedProduct)
                        {
                            flag = false;
                            BuscetProducts.Remove(elem);
                        }
                    }

                    if (flag)
                    {
                        BuscetProducts.Add(new BuscetItem(SelectedProduct));
                    }

                });
            }
        }
        public ICommand OpenAddProduct
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    _addWindowsFactory.CreateFactoryWindow().Show();
                });
            }
        }
        public ICommand OpenBascet
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    BuscetProducts = new ObservableCollection<BuscetItem>();
                    if (Products != null)
                    {
                        foreach (var elem in Products)
                        {
                            if (elem.buscetFlag == true)
                            {
                                BuscetProducts.Add(new BuscetItem(elem));
                            }
                        }
                    }

                    _bascetWindowsFactory.CreateBusketWindow( BuscetProducts, _changeBasket).Show();
                });
            }
        }
        public ICommand OpenProduct
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (_rolle.AdminFlag||_rolle.ManagerFlag)
                    {
                        _editProductWindowsFactory.CreateEditWindow(selectedProduct,_delete).Show();
                    }
                    else
                    {
                        _productWindowsFactory.CreateProductWindow(selectedProduct).Show();
                    }

                });
            }
        }
        public ICommand ClearFilters
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Products = DatabaseLocator.Context.Products.Where(p => p.Type == selectedType).ToObservableCollection();
                    _serchText = "";
                    OnPropertyChanged(nameof(Products));
                });
            }
        }
        public ICommand WindowLoaded
        {
            get
            {
                  return new DelegateCommand(() =>
                {
                    ProductTypes=new List<ProductType> (DatabaseLocator.Context.ProductTypes.ToList());
                    Products = new ObservableCollection<Product>(DatabaseLocator.Context.Products.ToObservableCollection());
                    OnPropertyChanged(nameof(ProductTypes));
                    OnPropertyChanged(nameof(Products));
                });
            }
        }
        public static List<string> removeDuplicates(List<string> list)
        {
            return new HashSet<string>(list).ToList();
        }

    }

    public class FilterValue
    {
        public FilterValue(CharacteristicsName CharacteristicsName, Page CurentPage, IPage CurentVM)
        {
            this.CharacteristicsName= CharacteristicsName;
            this.CurentPage= CurentPage;
            this.CurentVM=CurentVM;
        }
        public CharacteristicsName CharacteristicsName { get; set; }
        public Page CurentPage { get; set; }
        public IPage CurentVM;
    }
    public class FilterResult
    {
        public string NameVM; 
        public double minValue;
        public double maxValue;
        public List<BoolCharacteristic> boolResult;
    }

    public class BuscetItem
    {
        public BuscetItem(Product product)
        {
            Product= product;
            quantity = 1;
        }
        public Product Product { get; set; }
        public int quantity { get; set; }
    }
}
