using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

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
                    Monitor.Enter(Splitter.sodaQ);
                    // start disposing soda
                    if (Splitter.sodaQ.Count >= 10)
                    {
                        Debug.WriteLine("[Info] Disposing soda");
                        for (int i = 0; i < 10; i++)
                        {
                            Splitter.sodaQ.Dequeue();
                        }
                    }


                    Monitor.Enter(Splitter.beerQ);
                    if (Splitter.beerQ.Count >= 10)
                    {
                        Debug.WriteLine("[Info] Disposing beer");
                        for (int i = 0; i < 10; i++)
                        {
                            Splitter.beerQ.Dequeue();
                        }
                    }


                }
                finally
                {
                        Monitor.Exit(Splitter.sodaQ);

                        Monitor.Exit(Splitter.beerQ);

                    Thread.Sleep(500);
                }
            }
        }
    }
}
