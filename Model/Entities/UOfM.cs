using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfTry.Model.Entities
{
    public class UOfM
    {
        public UOfM(string name)
        {
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public List<Specifications> Specifications { get; set; } = null!;
        public override string ToString()
        {
            return Name;
        }
    }
}
