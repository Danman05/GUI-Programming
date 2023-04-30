using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FlaskeAutomatWPF
{
    // Class that implements the INotifyPropertyChanged interface.
    // It has an event called PropertyChanged that is imported from the INotifyPropertyChanged interface.

    class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;


        /// <summary>
        /// method that raises the PropertyChanged event
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
