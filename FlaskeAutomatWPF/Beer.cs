using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlaskeAutomatWPF
{
    internal class Beer : Drink
    {
        public static Queue<Beer> beerQ = new();

        public Beer() : base("beer")
        {

        }
    }
}
