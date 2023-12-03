using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace wpfTry.Model.Entities
{
    public class Specifications
    {
        public int Id { get; set; }
        public int CharacteristicsNameId { get; set; }
        public CharacteristicsName CharacteristicsName { get; set; } = null!;

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public string Num { get; set; }
        public int UOfMId { get; set; }
        public UOfM UOfM { get; set; } = null!;

        
    }
}
