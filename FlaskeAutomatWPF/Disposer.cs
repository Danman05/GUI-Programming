using System;
using System.Collections.Generic;
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
                    if (Monitor.TryEnter(Soda.sodaQ))
                    {
                        // start disposing soda
                        if (Soda.sodaQ.Count >= 10)
                        {
                            Console.WriteLine("[Info] Disposing soda");
                            for (int i = 0; i < 10; i++)
                            {
                                Soda.sodaQ.Dequeue();
                            }
                        }
                        Monitor.Exit(Soda.sodaQ);
                    }
                    if (Monitor.TryEnter(Beer.beerQ))
                    {
                        if (Beer.beerQ.Count >= 10)
                        {
                            Console.WriteLine("[Info] Disposing beer");
                            for (int i = 0; i < 10; i++)
                            {
                                Beer.beerQ.Dequeue();
                            }
                        }
                        Monitor.Exit(Beer.beerQ);
                    }
                }
                finally
                {
                    Thread.Sleep(500);
                }
            }
        }
    }
}
