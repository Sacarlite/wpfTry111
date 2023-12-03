using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using wpfTry.Model.Entities;
using wpfTry.Viev;
using wpfTry.VievModel;

namespace wpfTry.Services.PageService
{

    class TypesPageVievModel: BaseVievModel
    {
        public TypesAndCharacteristics _selectedType;
        public TypesAndCharacteristics SelectedType
        {
            get
            {
                return _selectedType;
            }
            set
            {
                if (value != _selectedType)
                {
                    _selectedType = value;
                    OnPropertyChanged(nameof(SelectedType));
                }
            }
        }
        public string _typeName;
        public string TypeName
        {
            get
            {
                return _typeName;
            }
            set
            {
                if (value != _typeName)
                {
                    _typeName = value;
                    OnPropertyChanged(nameof(TypeName));
                }
            }
        }
        private ObservableCollection<CharacteristicsName> _itemCharacteristicsCollection;
        public ObservableCollection<CharacteristicsName> ItemCharacteristicsCollection
        {
            get
            {
                return _itemCharacteristicsCollection;
            }
            set
            {
                if (value != _itemCharacteristicsCollection)
                {
                    _itemCharacteristicsCollection = value;
                    OnPropertyChanged(nameof(ItemCharacteristicsCollection));
                }
            }
        }
        public CharacteristicsName _selectedAddTemplateCharacteristic;
        public CharacteristicsName SelectedAddTemplateCharacteristic
        {
            get
            {
                return _selectedAddTemplateCharacteristic;
            }
            set
            {
                if (value != _selectedAddTemplateCharacteristic)
                {
                    _selectedAddTemplateCharacteristic= value;
                    OnPropertyChanged(nameof(SelectedAddTemplateCharacteristic));
                }
            }
        }
        public CharacteristicsName _selectedAddCharacteristic;
        public CharacteristicsName SelectedAddCharacteristic
        {
            get
            {
                return _selectedAddCharacteristic;
            }
            set
            {
                if (value != _selectedAddCharacteristic)
                {
                    _itemCharacteristicsCollection.Add(value);
                    OnPropertyChanged(nameof(ItemCharacteristicsCollection));
                    OnPropertyChanged(nameof(SelectedAddCharacteristic));
                }
            }
        }
        private ObservableCollection<CharacteristicsName> _characteristicsCollection;
        public ObservableCollection<CharacteristicsName> CharacteristicsCollection
        {
            get
            {
                return _characteristicsCollection;
            }
            set
            {
                if (value != _characteristicsCollection)
                {
                    _characteristicsCollection = value;
                    OnPropertyChanged(nameof(CharacteristicsCollection));
                }
            }
        }
        private ObservableCollection<TypesAndCharacteristics> _typesAndCharacteristicsCollection;
        public ObservableCollection<TypesAndCharacteristics> TypesAndCharacteristicsCollection
        {
            get
            {
                return _typesAndCharacteristicsCollection;
            }
            set
            {
                if (value != _typesAndCharacteristicsCollection)
                {
                    _typesAndCharacteristicsCollection= value;
                    OnPropertyChanged(nameof(TypesAndCharacteristicsCollection));
                }
            }
        }
        public TypesPageVievModel()
        {
            _itemCharacteristicsCollection = new ObservableCollection<CharacteristicsName>();
            _typesAndCharacteristicsCollection =new ObservableCollection<TypesAndCharacteristics>();
            var productTypes = DatabaseLocator.Context.ProductTypes.ToList();
            foreach (var elem in productTypes)
            {
                var a = DatabaseLocator.Context.ListsOfCharacteristics.Where(u => u.ProductTypeName == elem).ToList();
                var b = new List<CharacteristicsName>();
                foreach (var elem1 in a)
                {
                    b.Add(elem1.CharacteristicsName);

                }
                _typesAndCharacteristicsCollection.Add(new TypesAndCharacteristics(elem,b.ToObservableCollection()));
            }

            _characteristicsCollection = DatabaseLocator.Context.CharacteristicsNames.ToObservableCollection();
        }
        public ICommand DeleteNewCharacteristic
        {
            get
            {
                return new DelegateCommand<CharacteristicsName>((obj) =>
                {
                    var a = obj;
                    foreach (var elem in ItemCharacteristicsCollection)
                    {
                        if (elem.Id == a.Id)
                        {
                            ItemCharacteristicsCollection.Remove(elem);
                        }
                    }

                    OnPropertyChanged(nameof(ItemCharacteristicsCollection));
                });
            }
        }
        public ICommand AddNewTypeCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (TypeName != null)
                    {
                        var newType = new ProductType(TypeName);
                        DatabaseLocator.Context.ProductTypes.Add(newType);
                        foreach (var elem in ItemCharacteristicsCollection)
                        {
                            var newListOfCharacteristic = new ListOfCharacteristics();
                            newListOfCharacteristic.CharacteristicsName = elem;
                            newListOfCharacteristic.ProductTypeName = newType;
                            DatabaseLocator.Context.ListsOfCharacteristics.Add(newListOfCharacteristic);
                        }

                        DatabaseLocator.Context.SaveChanges();
                        _typesAndCharacteristicsCollection.Clear();
                        var productTypes = DatabaseLocator.Context.ProductTypes.ToList();
                        foreach (var elem in productTypes)
                        {
                            var a = DatabaseLocator.Context.ListsOfCharacteristics.Where(u => u.ProductTypeName == elem).ToList();
                            var b = new List<CharacteristicsName>();
                            foreach (var elem1 in a)
                            {
                                b.Add(elem1.CharacteristicsName);

                            }
                            _typesAndCharacteristicsCollection.Add(new TypesAndCharacteristics(elem, b.ToObservableCollection()));
                        }
                        OnPropertyChanged(nameof(TypesAndCharacteristicsCollection));
                        TypeName = "";
                        _itemCharacteristicsCollection .Clear();
                        OnPropertyChanged(nameof(ItemCharacteristicsCollection));
                        _selectedAddCharacteristic = null;
                        OnPropertyChanged(nameof(SelectedAddCharacteristic));
                    }
                });
            }
        }
        public ICommand AddNewTemplateCharacteristic
        {
            get
            {
                return new DelegateCommand<TypesAndCharacteristics>((obj) =>
                {
                    var a = obj;
                    if (_selectedAddTemplateCharacteristic != null)
                    {
                        a.CharacteristicsNames.Add(_selectedAddTemplateCharacteristic);
                        var listsOfCharacteristics = new ListOfCharacteristics();
                        listsOfCharacteristics.CharacteristicsName = _selectedAddTemplateCharacteristic;
                        listsOfCharacteristics.ProductTypeName = obj.ProductType;
                        DatabaseLocator.Context.ListsOfCharacteristics.Remove(listsOfCharacteristics);
                        OnPropertyChanged(nameof(TypesAndCharacteristicsCollection));
                    }
                });
            }
        }
        public ICommand DeleteCharacteristic
        {
            get
            {
                return new DelegateCommand<CharacteristicsName>((obj) =>
                {
                    var a = obj;
                    var b = _selectedType;
                    if (_selectedType != null)
                    {
                        foreach (var elem in b.CharacteristicsNames)
                        {
                            if (elem.Id == a.Id)
                            {
                                b.CharacteristicsNames.Remove(elem);
                                break;
                            }
                        }
                        var listsOfCharacteristics = DatabaseLocator.Context.ListsOfCharacteristics.Where(p=>p.ProductTypeName== _selectedType.ProductType&&p.CharacteristicsName==a).FirstOrDefault();
                        DatabaseLocator.Context.ListsOfCharacteristics.Remove(listsOfCharacteristics);
                        List<Specifications>? specifications = DatabaseLocator.Context.Specification.Where(p => p.Product.Type == _selectedType.ProductType && p.CharacteristicsName == a).ToList();
                        if (specifications != null)
                        {
                            foreach (var elem in specifications)
                            {
                                DatabaseLocator.Context.Specification.Remove(elem);
                            }
                        }
                        DatabaseLocator.Context.SaveChanges();
                        for (int i = 0; i < TypesAndCharacteristicsCollection.Count; i++)
                        {
                            if (TypesAndCharacteristicsCollection[i] == b)
                            {
                                TypesAndCharacteristicsCollection[i] = b;
                                break;
                            }
                        }

                        OnPropertyChanged(nameof(TypesAndCharacteristicsCollection));
                    }
                });
            }
        }
    }

    class TypesAndCharacteristics
    {
        public ProductType ProductType { get; set; }
        public ObservableCollection<CharacteristicsName> CharacteristicsNames { get; set; }
        public TypesAndCharacteristics(ProductType ProductType, ObservableCollection<CharacteristicsName> CharacteristicsNames)
        {
            this.ProductType = ProductType;
            this.CharacteristicsNames = CharacteristicsNames;
        }
    }
}
