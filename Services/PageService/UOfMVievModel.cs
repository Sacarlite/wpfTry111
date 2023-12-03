using DevExpress.Mvvm.Native;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using wpfTry.Model.Entities;
using wpfTry.VievModel;
using wpfTry.Viev;

namespace wpfTry.Services.PageService
{
    public class UOfNVievModel : BaseVievModel
    {
        private string _newName;

        public string NewName
        {
            get { return _newName; }
            set
            {
                if (value != _newName)
                {
                    _newName = value;
                    OnPropertyChanged(nameof(NewName));
                }
            }
        }

        private ObservableCollection<UOfM> _uOfMCollection;

        public ObservableCollection<UOfM> UOfMCollection
        {
            get
            {
                return _uOfMCollection;
            }
            set
            {
                if (value != _uOfMCollection)
                {
                    _uOfMCollection = value;
                    OnPropertyChanged(nameof(UOfMCollection));
                }
            }
        }
        public UOfNVievModel()
        {
            _uOfMCollection = new ObservableCollection<UOfM>();
            UOfMCollection = DatabaseLocator.Context.UOfMs.ToObservableCollection();
        }
        public ICommand EditCommand
        {
            get
            {
                return new DelegateCommand<UOfM>((UOfM) =>
                {
                    var editUOfM = DatabaseLocator.Context.UOfMs.Where(u => u.Id == UOfM.Id).FirstOrDefault();
                    if (editUOfM != null)
                    {
                        editUOfM = UOfM;
                    }
                    DatabaseLocator.Context.SaveChanges();
                    MessageBox.Show("Еденица измерения успешно изменён");
                });
            }
        }
        public ICommand DeleteCommand
        {
            get
            {
                return new DelegateCommand<UOfM>((UOfM) =>
                {
                    var editUOfM = DatabaseLocator.Context.UOfMs.Where(u => u.Id == UOfM.Id).FirstOrDefault();
                    if (editUOfM != null)
                    {
                        editUOfM = UOfM;
                    }
                    DatabaseLocator.Context.UOfMs.Remove(editUOfM);
                    DatabaseLocator.Context.SaveChanges();
                    MessageBox.Show("Еденица измерения успешно удалена");
                });
            }
        }
        public ICommand AddUoFMCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (NewName != null)
                    {
                        var newUOfM = new UOfM(NewName);
                        DatabaseLocator.Context.UOfMs.Add(newUOfM);
                        NewName = "";
                        UOfMCollection = DatabaseLocator.Context.UOfMs.ToObservableCollection();
                        DatabaseLocator.Context.SaveChanges();
                    }
                })
                {

                };
            }
        }
    }
}
