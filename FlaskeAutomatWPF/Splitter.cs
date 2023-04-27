using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlaskeAutomatWPF
{
    internal class Splitter  : BaseViewModel
    {
        public static Queue<Soda> sodaQ = new();
        public static Queue<Beer> beerQ = new();

        private int _sodaQueueCount = sodaQ.Count;
        public int SodaQueueCount
        {
            get 
            {
                return _sodaQueueCount;
            }
            set
            {
                if (_sodaQueueCount != value)
                {
                    _sodaQueueCount = value;
                    OnPropertyChanged(nameof(SodaQueueCount));
                }
            }
        }
        private int _beerQueueCount = beerQ.Count;

        public int BeerQueueCount
        {
            get { return _beerQueueCount; }
            set 
            {
                if (_beerQueueCount != value)
                {
                    _beerQueueCount = value;
                    OnPropertyChanged(nameof(BeerQueueCount));
                }
            }
        }


        public Splitter()
        {
            //Set up a timer to update the queue count every time

            var timer = new System.Timers.Timer(500);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            // Update the soda queue count
            SodaQueueCount = sodaQ.Count;
            BeerQueueCount = beerQ.Count;

        }

        public void SplitterConsumer()
        {                   
            
            Drink drink;
            while (true)
            {
                try
                {
                    Debug.WriteLine($"Soda queue count{SodaQueueCount}");
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
