using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlaskeAutomatWPF
{
    internal class Soda : Drink
    {
        public static Queue<Soda> sodaQ = new();
        public Soda() : base("soda")
        {

        }
    }
}
