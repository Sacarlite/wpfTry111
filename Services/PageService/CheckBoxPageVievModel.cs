using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using DevExpress.Mvvm.Native;
using wpfTry.Model.Entities;
using wpfTry.Services.PageService.PageInterface;
using wpfTry.Viev;
using wpfTry.VievModel;

namespace wpfTry.Services.PageService
{
    class CheckBoxPageVievModel: BaseVievModel, IPage
    {
        private ObservableCollection<BoolCharacteristic> _characteristicList;
        public ObservableCollection<BoolCharacteristic> CharacteristicList
        {
            get { return _characteristicList; }
            set
            {
                _characteristicList = value;
                OnPropertyChanged(nameof(CharacteristicList));
                
            }
        }
        public  CheckBoxPageVievModel(CharacteristicsName CharacteristicsName, ProductType selectedType)
      {
          _characteristicList = new ObservableCollection<BoolCharacteristic>();
            var characteristic =
                DatabaseLocator.Context.Specification.Where(u => u.CharacteristicsName == CharacteristicsName && u.Product.Type == selectedType).ToList();
            List<string> tmpList = new List<string>();
            foreach (var elem in characteristic)
            {
                tmpList.Add(elem.Num);
            }
            tmpList= removeDuplicates(tmpList);
            foreach (var elem in tmpList)
            {
                _characteristicList.Add(new BoolCharacteristic(elem));
            }
        }
        public static List<string> removeDuplicates(List<string> list)
        {
            return new HashSet<string>(list).ToList();
        }

        public FilterResult GetPageResult()
      {
          FilterResult filterResult = new FilterResult();
          filterResult.NameVM = nameof(CheckBoxPageVievModel);
          filterResult.boolResult = _characteristicList.ToList();
          return filterResult;
      }
    }

   public class BoolCharacteristic
    {
       public BoolCharacteristic(string NumName)
        {
            this.NumName = NumName;
        }

        public string NumName { get; set; }
        public bool CharacteristicFlag { get; set; }
    }
}
