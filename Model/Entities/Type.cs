using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace wpfTry.Model.Entities
{
    public class ProductType
    {
        public ProductType(string name)
        {
            Name = name;
        }
        public int Id { get; set; }
        
        public string Name { get; set; }
        public List<Product> products { get; set; } = null!;
        public List<ListOfCharacteristics> CharacteristicsNames { get; set; } = null!;
    }
}
