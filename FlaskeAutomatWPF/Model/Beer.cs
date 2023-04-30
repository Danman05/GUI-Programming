using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlaskeAutomatWPF
{
    internal class Beer : Drink
    {
        // Beer inherits from the Drink class and sets the name of the beer
        // to "beer" using the base constructor. 
        public Beer() : base("beer")
        {

        }
    }
}
