using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfTry.Model.Entities;
using wpfTry.Viev.Interface;
using wpfTry.VievModel;

namespace wpfTry.Services.ProductService.Interface
{
    public interface IManagerWindowFactory
    {
        IWindow CreateManagerWindow(Order item);
    }
}
