using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FlaskeAutomatWPF
{
    class SodaViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<Soda> _sodas;

        public ObservableCollection<Soda> Sodas
        {
            get { return _sodas; }
            set
            {
                _sodas = value;
                OnPropertyChanged("Sodas");
                
            }
        }

        public SodaViewModel()
        {
            new Soda { Name = "Cola" };
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
