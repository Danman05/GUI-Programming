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
                                Monitor.Enter(Soda.sodaQ);
                                Soda.sodaQ.Enqueue((Soda)drink);
                                
                            }
                            else if (drink.Name == "beer")
                            {
                                Monitor.Enter(Beer.beerQ);
                                Beer.beerQ.Enqueue(((Beer)drink));
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

                    if (Monitor.IsEntered(Soda.sodaQ))
                        Monitor.Exit(Soda.sodaQ);

                    if (Monitor.IsEntered(Beer.beerQ))
                        Monitor.Exit(Beer.beerQ);

                    Thread.Sleep(500);
                }

            }
        }
    }
}
