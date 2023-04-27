using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlaskeAutomatWPF
{
    class DrinkViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Drink> _drinks;

        public ObservableCollection<Drink> Drinks
        {
            get { return _drinks; }
            set
            {
                _drinks = value;
                OnPropertyChanged("Drinks");
            }
        }

        public DrinkViewModel()
        {
            Drinks = new ObservableCollection<Drink>();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
