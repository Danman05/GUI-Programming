using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlaskeAutomatWPF
{
    internal class Drink
    {
        // Private field that stores the name of the drink
        private string name;

        // Public property that provides access to the name field
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        // Constructor for the Drink class that sets the name based on the argument passed in
        public Drink(string name)
        {
            this.name = name;
        }
    }
}
