using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfTry.VievModel;

namespace wpfTry.Services.Events.EventInterface
{
    public interface IChangeBasket
    {
        public event EventHandler <ChangeBasketEventArgs> ChangeBasketLogin;
        public void InvokeEvent(ObservableCollection<BuscetItem> item);

    }
}
