using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Controls;

namespace FlaskeAutomatWPF
{
    internal class Produce
    {

        public static void ProduceBottle()
        {
            MainWindow main = new MainWindow();
           
            Random random = new();
            int randomNum;
            string writeProduceString = "";

            while (true)
            {
                try
                {
                    Monitor.Enter(Drink.drinkQ);
                    if (Drink.drinkQ.Count < 3)
                    {
                        while (Drink.drinkQ.Count < 20)
                        {
                            writeProduceString = "";
                            randomNum = random.Next(1, 3);

                            if (randomNum == 1)
                            {
                                Drink.drinkQ.Enqueue(new Beer());
                            }
                            else
                            {
                                Drink.drinkQ.Enqueue(new Soda());
                            }
                        }
                        Monitor.PulseAll(Drink.drinkQ);
                    }
                    else
                    {
                        writeProduceString = "[Info] Bottle producer waits";
                    }
                }
                finally
                {
                    main.sodaCounter.Content = $"{Drink.drinkQ.Count}";
                    Debug.WriteLine($"Unsorted bottles: {Drink.drinkQ.Count}");
                    Debug.WriteLine($"Soda bottles: {Soda.sodaQ.Count}");
                    Debug.WriteLine($"Beer bottles: {Beer.beerQ.Count}");
                    Debug.WriteLine($"{writeProduceString}");
                    Monitor.Exit(Drink.drinkQ);
                    Thread.Sleep(500);
                }
            }
        }
    }
}
