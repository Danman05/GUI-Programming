using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace FlaskeAutomatWPF
{
    internal class Splitter : BaseViewModel
    {

        // Class represents a splitter that takes in drinks from a queue and -
        // splits them into separate queues for soda and beer
        // It also updates the queue counts every 500 milliseconds using a timer

        public static Queue<Drink> drinkQ = new();
        public static Queue<Soda> sodaQ = new();
        public static Queue<Beer> beerQ = new();

        // The queues for drinks, soda, and beer

        private int _sodaQueueCount = sodaQ.Count;
        private int _beerQueueCount = beerQ.Count;
        private int _drinkQueueCount = drinkQ.Count;

        // Properties for accessing the queue counts

        public int SodaQueueCount
        {
            get { return _sodaQueueCount; }
            set
            {
                if (_sodaQueueCount != value)
                {
                    _sodaQueueCount = value;
                    OnPropertyChanged(nameof(SodaQueueCount));
                }
            }
        }
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
        public int DrinkQueueCount
        {
            get { return _drinkQueueCount; }
            set
            {
                if (_drinkQueueCount != value)
                {
                    _drinkQueueCount = value;
                    OnPropertyChanged(nameof(DrinkQueueCount));
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

        // Timer that updates the queue counts

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            // Update the soda queue count
            DrinkQueueCount = drinkQ.Count;
            SodaQueueCount = sodaQ.Count;
            BeerQueueCount = beerQ.Count;

        }

        /// <summary>
        /// Dequeus an drink from the drink queue and puts them into the soda or beer queue based on their type
        /// </summary>
        public static void SplitterConsumer()
        {
            Drink drink;
            while (true)
            {
                try
                {
                    Monitor.Enter(drinkQ);

                    if (drinkQ.Count == 0)
                    {
                        Monitor.Wait(drinkQ);
                    }
                    else
                    {
                        drink = drinkQ.Dequeue();

                        // Check drink type

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
                finally
                {
                    Monitor.Exit(drinkQ);

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
