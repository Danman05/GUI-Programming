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

        // Disposer class that continuously monitors sodaQ and beerQ
        // It uses Monitor.Enter and Monitor.Exit to synchronize access to the shared queues between threads.
        // The method runs in an infinite loop with a delay of 500ms between each iteration.

        // Max capacity of drinks, to start disposing
        private const int disposeDrinkCapacity = 10;

        /// <summary>
        /// Static method that continuously disposes of soda and beer bottles from their queues in the Splitter class
        /// </summary>
        public static void DisposeBottle()
        {

            while (true)
            {
                try
                {
                    Monitor.Enter(Splitter.sodaQ);
                    Monitor.Enter(Splitter.beerQ);

                    if (Splitter.sodaQ.Count >= disposeDrinkCapacity)
                    {
                        Debug.WriteLine("[Info] Disposing soda");
                        for (int i = 0; i < 10; i++)
                        {
                            Splitter.sodaQ.Dequeue();
                        }
                    }

                    if (Splitter.beerQ.Count >= disposeDrinkCapacity)
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
