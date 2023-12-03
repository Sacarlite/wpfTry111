using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfTry.Model.Entities;
using wpfTry.Services.PageService.PageInterface;
using wpfTry.Viev;
using wpfTry.VievModel;

namespace wpfTry.Services.PageService
{
    class SliderPageVievModel : BaseVievModel, IPage
    {
        private double _maxValue;
        private double _minSliderValue;
        private double _maxSliderValue;
        private double _minValue;
        public double MaxValue
        {
            get { return _maxValue; }
            set
            {
                if (_maxValue != value)
                {
                    _maxValue = value;
                    OnPropertyChanged(nameof(MaxValue));
                }
            }
        }
        public double MinSliderValue
        {
            get { return _minSliderValue; }
            set
            {
                if (_minSliderValue != value)
                {
                    _minSliderValue = value;
                    OnPropertyChanged(nameof(MinSliderValue));
                }
            }
        }
        public double MaxSliderValue
        {
            get { return _maxSliderValue; }
            set
            {
                if (_maxSliderValue != value)
                {
                    _maxSliderValue = value;
                    OnPropertyChanged(nameof(MaxSliderValue));
                }
            }
        }
        public double MinValue
        {
            get { return _minValue; }
            set
            {
                if (_minValue != value)
                {
                    _minValue = value;
                    OnPropertyChanged(nameof(MinValue));
                }
            }
        }
        public SliderPageVievModel(CharacteristicsName CharacteristicsName, ProductType selectedType)
        {
            List<double> doubleCharacteristics=new List<double>();
            var characteristic =
                DatabaseLocator.Context.Specification.Where(u => u.CharacteristicsName == CharacteristicsName && u.Product.Type== selectedType).ToList();

            foreach (var elem in characteristic)
            {
                doubleCharacteristics.Add(Convert.ToDouble(elem.Num));
            }

            _minValue = doubleCharacteristics.Min();
            _minSliderValue= doubleCharacteristics.Min();
            _maxValue = doubleCharacteristics.Max();
            _maxSliderValue = doubleCharacteristics.Max();
            OnPropertyChanged(nameof(MaxValue));
            OnPropertyChanged(nameof(MinValue));
        }

        public FilterResult GetPageResult()
        {
            FilterResult filterResult=new FilterResult();
            filterResult.NameVM = nameof(SliderPageVievModel);
            filterResult.maxValue = _maxSliderValue;
            filterResult.minValue = _minSliderValue;
            return filterResult;
        }
    }
}
