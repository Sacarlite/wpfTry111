using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfTry.Services;
using wpfTry.Services.AtorizationService.@interface;
using wpfTry.Services.Events.EventInterface;
using wpfTry.Viev;
using wpfTry.Viev.Interface;
using wpfTry.VievModel;

namespace wpfTry.Services.AtorizationService
{
    public class AuthorizationFactory : IAuthorizationFactory
    {
        public IWindow CreateAutorizationindow(ILoginEvent login)
        {
            var window = new Autorization();
            var viewModel = new AutorizationVievModel( window.Close,  login);
            window.DataContext = viewModel;
            return window;
        }
    }
}
