using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfTry.Model.Entities;
using wpfTry.Viev.Interface;

namespace wpfTry.Services.ProductService.Interface
{
    public interface IProductWindowsFactory
    {
        IWindow CreateProductWindow(Product item);
    }
}
