using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;
using wpfTry.Model.Entities;
using wpfTry.Viev;
using wpfTry.VievModel.Interface;
namespace wpfTry.VievModel
{
    public class ProductVievModel: BaseVievModel, ICloseWindow
    {
        private Product product;
        private int quantity;
        private ObservableCollection<tmp2> _characteristicList;
        private List<UOfM> uoFMs;
        public int ProductId
        {
            get { return product.Id; }
        }
        public string ProductName
        {
            get { return product.Name; }
        }
        public string ProductImage
        {
            get
            {
                return product.Image;
            }
        }
        public string ProductColor
        {
            get
            {
                return product.Color;
            }
        }
        public double ProductWidht
        {
            get
            {
                return product.width;
            }
        }
        public double ProductHeight
        {
            get
            {
                return product.height;
            }
        }
        public double ProductLength
        {
            get
            {
                return product.length;
            }
        }
        public double ProductVolume
        {
            get
            {
                return product.volume;
            }
        }
        public string ProductHeatResistance
        {
            get
            {
                return product.HeatResistance;
            }
        }
        public string ProductManufacturer
        {
            get
            {
                return product.manufacturer;
            }
        }
        public string ProductDescription
        {
            get
            {
                return product.Discription;
            }
        }
        public ObservableCollection<tmp2> CharacteristicList
        {
            get { return _characteristicList; }
            set
            {
                _characteristicList = value;
                OnPropertyChanged(nameof(CharacteristicList));
            }
        }
        void GetCharacteristic()
        {
            _characteristicList = new ObservableCollection<tmp2>();
            uoFMs = DatabaseLocator.Context.UOfMs.ToList();
            List<ListOfCharacteristics> specificationsList = DatabaseLocator.Context.ListsOfCharacteristics.Where(u => u.ProductTypeName == product.Type).ToList();
            foreach (var elem in specificationsList)
            {

                tmp2 elemTmp = new tmp2(elem.CharacteristicsName.Name, uoFMs);
                var a = DatabaseLocator.Context.Specification.Where(u =>
                    u.CharacteristicsName == elem.CharacteristicsName && u.Product == product).FirstOrDefault();
                elemTmp.content = a.Num;
                elemTmp.SelectedUOF = a.UOfM;
                _characteristicList.Add(elemTmp);
            }
            OnPropertyChanged(nameof(CharacteristicList));
        }
        public ProductVievModel(Product product, Action? close)
        {
            this.product = product;
            this.close = close;
            GetCharacteristic();
        }
        
        public Action close { get; set; }
        public bool CanClose()
        {
            return true;
        }
        private void CloseWindow()
        {
            close?.Invoke();
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
    }
}
