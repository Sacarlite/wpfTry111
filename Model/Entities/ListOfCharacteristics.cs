using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfTry.Model.Entities
{
    public class ListOfCharacteristics
    {
        public int Id { get; set; }
        public int CharacteristicsNameId { get; set; }
        public CharacteristicsName CharacteristicsName { get; set; } = null!;
        public int ProductTypeId { get; set; }
        public ProductType ProductTypeName { get; set; } = null!;
    }
}
