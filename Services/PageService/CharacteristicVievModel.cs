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
    public class CharacteristicVievModel:BaseVievModel
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

        private bool _newComent;
        public bool NewComent
        {
            get { return _newComent; }
            set
            {
                if (value != _newComent)
                {
                    _newComent = value;
                    OnPropertyChanged(nameof(NewComent));
                }
            }
        }

        private ObservableCollection<CharacteristicsName> _characteristicCollection;

        public ObservableCollection<CharacteristicsName> CharacteristicCollection
        {
            get
            {
                return _characteristicCollection;
            }
            set
            {
                if (value != _characteristicCollection)
                {
                    _characteristicCollection=value;
                    OnPropertyChanged(nameof(CharacteristicCollection));
                }
            }
        }
        public CharacteristicVievModel()
        {
            _characteristicCollection=new ObservableCollection<CharacteristicsName>();
            CharacteristicCollection = DatabaseLocator.Context.CharacteristicsNames.ToObservableCollection();
        }
        public ICommand EditCommand
        {
            get
            {
                return new DelegateCommand<CharacteristicsName>((characteristic) =>
                {
                    var editCharacteristic = DatabaseLocator.Context.CharacteristicsNames.Where(u => u.Id == characteristic.Id).FirstOrDefault();
                    if (editCharacteristic != null)
                    {
                        editCharacteristic = characteristic;
                    }
                    DatabaseLocator.Context.SaveChanges();
                    MessageBox.Show("Характеристика успешно изменена");
                });
            }
        }
        public ICommand DeleteCommand
        {
            get
            {
                return new DelegateCommand<CharacteristicsName>((characteristic) =>
                {
                    var editCharacteristic = DatabaseLocator.Context.CharacteristicsNames.Where(u => u.Id == characteristic.Id).FirstOrDefault();
                    if (editCharacteristic != null)
                    {
                        editCharacteristic = characteristic;
                    }
                    DatabaseLocator.Context.CharacteristicsNames.Remove(editCharacteristic);
                    DatabaseLocator.Context.SaveChanges();
                    MessageBox.Show("Пользователь успешно удалён");
                });
            }
        }
        public ICommand AddNewCharacteristicCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (NewComent != null && NewName != null)
                    {
                        var newCharacteristic = new CharacteristicsName(NewName, NewComent);
                        DatabaseLocator.Context.CharacteristicsNames.Add(newCharacteristic);
                        NewName = "";
                        NewComent = false;
                        CharacteristicCollection = DatabaseLocator.Context.CharacteristicsNames.ToObservableCollection();
                        DatabaseLocator.Context.SaveChanges();
                    }
                })
                {

                };
            }
        }
    }
}
