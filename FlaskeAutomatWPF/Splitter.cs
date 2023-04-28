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
        private MainWindow _mainWindow;

        public static Queue<Drink> drinkQ = new();
        public static Queue<Soda> sodaQ = new();
        public static Queue<Beer> beerQ = new();

        private int _sodaQueueCount = sodaQ.Count;
        private int _beerQueueCount = beerQ.Count;
        private int _drinkQueueCount = drinkQ.Count;

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

        public Splitter(MainWindow mainWindow)
        {
            //Set up a timer to update the queue count every time
            this._mainWindow = mainWindow;
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
            DrinkQueueCount = drinkQ.Count;
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
                    if (Monitor.TryEnter(drinkQ))
                    {
                        if (drinkQ.Count == 0)
                        {
                            Monitor.Wait(drinkQ);
                        }
                        else
                        {
                            // Check drink type
                            drink = drinkQ.Dequeue();

                            _mainWindow.AnimateDrink(drink);
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

                    if (Monitor.IsEntered(drinkQ))
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
