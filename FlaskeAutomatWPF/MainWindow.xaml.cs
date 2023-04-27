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
using System.Buffers.Text;

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
                        Dispatcher.Invoke(new Action(() =>
                        {
                            DataContext = this;
                        }));
                        if (drink.Name == "soda")
                        {
                            
                            Monitor.Enter(Splitter.sodaQ);

                            AnimateSoda();

                            Splitter.sodaQ.Enqueue((Soda)drink);

                            sodaLabel.Dispatcher.Invoke(new Action(() => sodaLabel.Content = "Soda bottle: " + Splitter.sodaQ.Count.ToString()));
                        }
                        else if (drink.Name == "beer")
                        {
                            Monitor.Enter(Splitter.beerQ);
                            Splitter.beerQ.Enqueue(((Beer)drink));
                            beerLabel.Dispatcher.Invoke(new Action(() => beerLabel.Content = "Beer bottle: " + Splitter.beerQ.Count.ToString()));
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

                    if (Monitor.IsEntered(Splitter.sodaQ))
                        Monitor.Exit(Splitter.sodaQ);

                    else if (Monitor.IsEntered(Splitter.beerQ))
                        Monitor.Exit(Splitter.beerQ);

                    Thread.Sleep(500);
                }

            }
        }

        void AnimateSoda()
        {
            double originalLeft = 0;
            double originalTop = 0;

            double currentLeft;
            double currentTop;

            Dispatcher.Invoke(new Action(() =>
            {
                Image img = new Image
                {
                    //Visibility = Visibility.Visible,
                    Height = 50,
                    Width = 50,
                    //Margin = new Thickness(130, 160, 542, 163),
                    Source = new BitmapImage(new Uri("H:\\Skole\\H2\\Kodning\\GUI-Programming\\FlaskeAutomatWPF\\Ressource\\sodaCan.png", UriKind.Relative))

                };
                Canvas.SetLeft(img, 50);
                Canvas.SetTop(img, 50);


                //ok.Children.Add(img);

                //MainGrid.Children.Add(img);

            }));
            Dispatcher.Invoke(new Action(() =>
            {
                sodaCanImg.Visibility = Visibility.Visible;
                originalLeft = Canvas.GetLeft(sodaCanImg);
                originalTop = Canvas.GetTop(sodaCanImg);
            }));
            for (int i = 0; i < 18; i++)
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    currentLeft = Canvas.GetLeft(sodaCanImg);

                    Canvas.SetLeft(sodaCanImg, currentLeft + 10);
                }));
                Thread.Sleep(50);
            }
            for (int i = 0; i < 12; i++)
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    currentTop = Canvas.GetTop(sodaCanImg);

                    Canvas.SetTop(sodaCanImg, currentTop - 10);
                }));
                Thread.Sleep(50);
            }

            Dispatcher.Invoke(new Action(() =>
            {
                sodaCanImg.Visibility = Visibility.Hidden;
                Canvas.SetLeft(sodaCanImg, originalLeft);
                Canvas.SetTop(sodaCanImg, originalTop);

            }));

        }
    }
}
