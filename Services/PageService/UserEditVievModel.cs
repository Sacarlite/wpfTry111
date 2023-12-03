using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ControlzEx.Standard;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using wpfTry.Model.Entities;
using wpfTry.Viev;
using wpfTry.VievModel;

namespace wpfTry.Services.PageService
{
    public class UserEditVievModel:BaseVievModel
    {
        private string _newLogin;

        public string NewLogin
        {
            get { return _newLogin; }
            set
            {
                if (value != _newLogin)
                {
                    _newLogin = value;
                    OnPropertyChanged(nameof(NewLogin));
                }
            }
        }

        private string _newPassword;
        public string NewPassword
        {
            get { return _newPassword; }
            set
            {
                if (value != _newPassword)
                {
                    _newPassword = value;
                    OnPropertyChanged(nameof(NewPassword));
                }
            }
        }
        private bool _newRolleFlag;

        public bool NewRolleFlag
        {
            get { return _newRolleFlag; }
            set
            {
                if (value != _newRolleFlag)
                {
                    _newRolleFlag = value;
                    OnPropertyChanged(nameof(NewRolleFlag));
                }
            }
        }

        private ObservableCollection<User> _users;

        public ObservableCollection<User> UserCollection
        {
            get
            {
                return _users;
            }
            set
            {
                if (value != _users)
                {
                    _users=value;
                    OnPropertyChanged(nameof(UserCollection));
                }
            }
        }
        public UserEditVievModel()
        {
            _users=new ObservableCollection<User>();
            UserCollection = DatabaseLocator.Context.Users.ToObservableCollection();
        }
        public ICommand EditCommand
        {
            get
            {
                return new DelegateCommand<User>((user) =>
                {
                    var editUser = DatabaseLocator.Context.Users.Where(u => u.Id == user.Id).FirstOrDefault();
                    if (editUser != null)
                    {
                        editUser = user;
                    }
                    DatabaseLocator.Context.SaveChanges();
                    MessageBox.Show("Пользователь успешно изменён");
                });
            }
        }
        public ICommand DeleteCommand
        {
            get
            {
                return new DelegateCommand<User>((user) =>
                {
                    var editUser = DatabaseLocator.Context.Users.Where(u => u.Id == user.Id).FirstOrDefault();
                    if (editUser != null)
                    {
                        editUser = user;
                    }
                    DatabaseLocator.Context.Users.Remove(editUser);
                    MessageBox.Show("Пользователь успешно удалён");
                });
            }
        }
        public ICommand AddNewUserCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (NewRolleFlag != null && NewLogin != null && NewPassword != null)
                    {
                        var newUser = new User();
                        newUser.Login = NewLogin;
                        newUser.Password = NewPassword;
                        newUser.RolleFlag = NewRolleFlag;
                        DatabaseLocator.Context.Users.Add(newUser);
                        NewLogin = "";
                        NewPassword = "";
                        NewRolleFlag = false;
                        UserCollection = DatabaseLocator.Context.Users.ToObservableCollection();
                    }
                })
                {

                };
            }
        }
    }
}
