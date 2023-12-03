using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfTry.VievModel.Interface
{ 
    public interface ICloseWindow
    {
        Action close { get; set; }
        bool CanClose();
    } 

}
