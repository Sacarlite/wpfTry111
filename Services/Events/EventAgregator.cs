using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfTry.Model.Entities;
using wpfTry.Services.Events.EventInterface;
using wpfTry.VievModel;


namespace wpfTry.Services.Events
{
    
    public class LoginEventArgs : EventArgs
    {
        public readonly RolleModel item;

        public LoginEventArgs(RolleModel item)
        {
            this.item = item;

        }

    }
    public class ChangeBasketEventArgs : EventArgs
    {
        public readonly ObservableCollection<BuscetItem> item ;


        public ChangeBasketEventArgs(ObservableCollection<BuscetItem> item)
        {
            this.item = item;

        }

    }
    class EventAgregator: ILoginEvent, IChangeBasket, IAdminCloseEvent,IDelete
    {
        public event EventHandler<LoginEventArgs> Login;
        public void InvokeEvent(RolleModel item)
        {
           Login.Invoke(this,new LoginEventArgs(item));
        }
        public event EventHandler<ChangeBasketEventArgs> ChangeBasketLogin;

        public void InvokeEvent(ObservableCollection<BuscetItem> item)
        {
            ChangeBasketLogin.Invoke(this, new ChangeBasketEventArgs(item));
        }

        public event EventHandler? AdminClose;
        public event EventHandler? Delete;

        public void InvokeDelteEvent()
        {
            Delete.Invoke(this, new EventArgs());
        }
        public void InvokeEvent()
        {
            AdminClose.Invoke(this, new EventArgs());
        }
    }
}
