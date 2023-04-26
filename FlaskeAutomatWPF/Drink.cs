using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlaskeAutomatWPF
{
    internal class Drink
    {

        public static Queue<Drink> drinkQ = new();

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Drink(string name)
        {
            this.name = name;
        }
    }
}
