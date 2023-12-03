using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace wpfTry.Model.Entities
{
    public class CharacteristicsName
    {
        public CharacteristicsName(string name,bool coment)
        {
            Name = name;
            Coment= coment;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool Coment { get; set; }
        public List<ListOfCharacteristics> CharacteristicsList { get; set; } = null!;
        public override string ToString()
        {
            return Name;
        }
    }
}
