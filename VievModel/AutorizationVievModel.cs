using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DevExpress.Mvvm;
using wpfTry.Services.AtorizationService.@interface;
using wpfTry.Services.Events.EventInterface;
using wpfTry.Services.ProductService.Interface;
using wpfTry.Services.ProductViev;
using wpfTry.Viev;
using wpfTry.VievModel.Interface;

namespace wpfTry.VievModel
{
    public class AutorizationVievModel:ViewModelBase, ICloseWindow
    {
        private IAdminWindowsFactory _adminFactory;
        private ILoginEvent _login;
        private string login;
        private string password;
        public string Login
        {
            get { return login; }
            set
            {
                if (login != value)
                {
                    login = value;
                    RaisePropertiesChanged(nameof(Login));
                }
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                    RaisePropertiesChanged(nameof(Password));
                }
            }
        }
        public AutorizationVievModel( Action close, ILoginEvent login)
        {
            _adminFactory = new ProductFactory();
            this.close = close;
            _login=login;
            
        }
        public Action close
        {
            get;
            set;
        }
        public bool CanClose()
        {
            return true;
        }
        public ICommand OpenAdminWindow
        {
            get
            {

                return new DelegateCommand(() =>
                {
                    if (login != null && password != null)
                    {

                        if (DatabaseLocator.Context.Users.Select(u => u.Login).ToList().Contains(login) &&
                            DatabaseLocator.Context.Users.Select(u => u.Password).ToList().Contains(password))
                        {
                            var rolleModel = new RolleModel();
                            if (DatabaseLocator.Context.Users.Where(p => p.Login == login && p.Password == password)
                                .Select(u => u.RolleFlag).FirstOrDefault())
                            {
                                rolleModel.ManagerFlag = false;
                                rolleModel.AdminFlag = true;
                            }
                            else
                            {
                                rolleModel.ManagerFlag = true;
                                rolleModel.ManagerFlag = false;
                            }

                            _login.InvokeEvent(rolleModel);
                        }
                    }
                    close.Invoke();
                });

            }
        }
    }
    public class RolleModel
    {
        public bool AdminFlag;
        public bool ManagerFlag;
    }
}
