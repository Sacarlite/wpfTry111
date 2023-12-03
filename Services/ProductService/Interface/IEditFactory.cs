using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfTry.Model.Entities;
using wpfTry.Services.Events.EventInterface;
using wpfTry.Viev.Interface;

namespace wpfTry.Services.ProductService.Interface
{
    public interface IEditWindowsFactory
    {
        IWindow CreateEditWindow(Product item, IDelete Delete);
    }
}
