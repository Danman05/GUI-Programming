using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlaskeAutomatWPF
{
    internal class Drink : BaseViewModel
    {
        // TOOD: Find way to update drinkqueueCount value on the data bind in MainWINdow.xaml line 16
        public static Queue<Drink> drinkQ = new();

        private string name;
        private int _drinkQueueCount = drinkQ.Count;


        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int DrinkQueueCount
        {
            get { return _drinkQueueCount; }
            set
            {
                if (_drinkQueueCount != value)
                {
                    _drinkQueueCount = value;
                    OnPropertyChanged(nameof(DrinkQueueCount));
                }
            }
        }
        public Drink(string name)
        {
            this.name = name;
        }
    }
}
