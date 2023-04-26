using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace FlaskeAutomatWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void StartProduceBtn_Click(object sender, RoutedEventArgs e)
        {
            Thread producerThread = new(Produce.ProduceBottle);
            Thread splitterConsumerThread = new(SplitterConsumer);
            Thread consumerDrinkThread = new(Disposer.DisposeBottle);

            producerThread.Start();
            splitterConsumerThread.Start();
            consumerDrinkThread.Start();
        }

        public void SplitterConsumer()
        {
            Drink drink;
            while (true)
            {
                try
                {
                    Monitor.Enter(Drink.drinkQ);

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
                            sodaLabel.Dispatcher.Invoke(new Action(() => sodaLabel.Content = "Soda bottle: " + Soda.sodaQ.Count.ToString()));
                        }
                        else if (drink.Name == "beer")
                        {
                            Monitor.Enter(Beer.beerQ);
                            Beer.beerQ.Enqueue(((Beer)drink));
                            beerLabel.Dispatcher.Invoke(new Action(() => beerLabel.Content = "Beer bottle: " + Beer.beerQ.Count.ToString()));
                        }
                        else
                        {
                            Debug.WriteLine("[Error] - Unknown bottle");
                        }

                    }

                }
                finally
                {
                    unsortedLabel.Dispatcher.Invoke(new Action(() => unsortedLabel.Content = "Unsorted bottles: " + Drink.drinkQ.Count.ToString()));
                    Monitor.Exit(Drink.drinkQ);

                    if (Monitor.IsEntered(Soda.sodaQ))
                        Monitor.Exit(Soda.sodaQ);

                    else if (Monitor.IsEntered(Beer.beerQ))
                        Monitor.Exit(Beer.beerQ);

                    Thread.Sleep(500);
                }

            }
        }

    }
}
