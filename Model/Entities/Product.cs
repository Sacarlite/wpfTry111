using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfTry.VievModel;


namespace wpfTry.Model.Entities
{
    public class Product : BaseVievModel
    {
        public Product(string name, string color, string Discription,  string HeatResistance, double width, 
            double height, double length, double volume, string manufacturer, string Image,bool buscetFlag)
        {
            Name = name;
            Color= color;
            this.Discription = Discription;
            this.HeatResistance = HeatResistance;
            this.width= width;
            this.height= height;
            this.length= length;
            this.volume= volume;
            this.manufacturer= manufacturer;
            this.buscetFlag= buscetFlag;
            this.Image= Image;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public ProductType Type { get; set; } = null!;
        public string Color { get; set; }//
        public string Discription { get; set; }
        public string HeatResistance { get; set; }//
        public double width { get; set; }//
        public double height { get; set; }//
        public double length { get; set; }//
        public double volume { get; set; }//
        public string manufacturer { get; set; }//
        public string Image { get; set; }

        [NotMapped]
        private bool _buscetFlag;
        public bool buscetFlag
        {
            get
            {
                return _buscetFlag;
            }
            set
            {
                if (_buscetFlag != value)
                {
                    _buscetFlag = value;
                    OnPropertyChanged(nameof(buscetFlag));
                }
            }
        }
        public List<Specifications> Specifications { get; set; } = null!;
        public List<OrderTable> order { get; set; } = null!;
    }
}
