using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlaskeAutomatWPF
{
    internal class Splitter
    {
        public static Queue<Soda> sodaQ = new();
        public static Queue<Beer> beerQ = new();

        public void SplitterConsumer()
        {                   
            Drink drink;
            while (true)
            {
                try
                {
                    if (Monitor.TryEnter(Drink.drinkQ))
                    {
                        if (Drink.drinkQ.Count == 0)
                        {
                            Monitor.Wait(Drink.drinkQ);
                        }
                        else
                        {
                            // Check drink type
                            drink = Drink.drinkQ.Dequeue();

                            if (drink.Name == "soda")
                            {
                                Monitor.Enter(sodaQ);
                                sodaQ.Enqueue(((Soda)drink));
                                
                            }
                            else if (drink.Name == "beer")
                            {
                                Monitor.Enter(beerQ);
                                beerQ.Enqueue(((Beer)drink));
                            }
                            else
                            {
                                Console.WriteLine("Error");
                            }

                        }
                    }
                }
                finally
                {

                    if (Monitor.IsEntered(Drink.drinkQ))
                        Monitor.Exit(Drink.drinkQ);

                    if (Monitor.IsEntered(sodaQ))
                        Monitor.Exit(sodaQ);

                    if (Monitor.IsEntered(beerQ))
                        Monitor.Exit(beerQ);

                    Thread.Sleep(500);
                }

            }
        }
    }
}
