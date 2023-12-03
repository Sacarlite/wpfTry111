using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using wpfTry.Model.Entities;
using wpfTry.Services;
using wpfTry.Services.Events.EventInterface;
using wpfTry.Services.PageService;
using wpfTry.Services.ProductService.Interface;
using wpfTry.Services.ProductViev;
using wpfTry.Viev.Templates;
using wpfTry.VievModel.Interface;

namespace wpfTry.VievModel
{
    public class AdminVievModel : BaseVievModel, ICloseWindow
    {
        private RolleModel _rolle;
        private IAdminCloseEvent _adminClose;
        private Visibility adminVisibility;
        public Visibility AdminViev
        {
            get
            {
                if (_rolle.AdminFlag)
                {
                    adminVisibility = Visibility.Visible;
                }
                else
                {
                    adminVisibility = Visibility.Hidden;
                }
                return adminVisibility;

            }
        }
        public Page TypesPage { get; set; }
        public Page UsersPage { get; set; }
        public Page CharacteristicsPage { get; set; }
        public Page UofMPage { get; set; }
        public Page OrdersPage { get; set; }
        public AdminVievModel( Action close,  RolleModel rolle, IAdminCloseEvent adminClose)
        {
            _adminClose= adminClose;
            _rolle = rolle;
            this.close = close;
            TypesPage=new TypesEditPage();
            TypesPage.DataContext = new TypesPageVievModel();
            UsersPage = new UserPage();
            UsersPage.DataContext = new UserEditVievModel();
            CharacteristicsPage = new CharacteristicsPage();
            CharacteristicsPage.DataContext = new CharacteristicVievModel();
            UofMPage = new UOfMsPage();
            UofMPage.DataContext = new UOfNVievModel();
            OrdersPage = new OrdersTablePage();
            OrdersPage.DataContext = new OrderTabelVievModel();
        }

        public Action close { get; set; }
            public bool CanClose()
            {
               return true;
            }
            public ICommand windowClosing
            {
                get
                {

                    return new DelegateCommand(() =>
                    {
                        CloseWindow();
                    });

                }
            }
            private void CloseWindow()
            {
                _adminClose.InvokeEvent();
            }
    }


}
