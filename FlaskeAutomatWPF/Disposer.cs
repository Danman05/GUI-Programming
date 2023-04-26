using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlaskeAutomatWPF
{
    internal class Disposer
    {
        public static void DisposeBottle()
        {

            while (true)
            {
                try
                {
                    Monitor.Enter(Soda.sodaQ);
                    // start disposing soda
                    if (Soda.sodaQ.Count >= 10)
                    {
                        Debug.WriteLine("[Info] Disposing soda");
                        for (int i = 0; i < 10; i++)
                        {
                            Soda.sodaQ.Dequeue();
                        }
                    }


                    Monitor.Enter(Beer.beerQ);
                    if (Beer.beerQ.Count >= 10)
                    {
                        Debug.WriteLine("[Info] Disposing beer");
                        for (int i = 0; i < 10; i++)
                        {
                            Beer.beerQ.Dequeue();
                        }
                    }


                }
                finally
                {
                        Monitor.Exit(Soda.sodaQ);

                        Monitor.Exit(Beer.beerQ);

                    Thread.Sleep(500);
                }
            }
        }
    }
}
