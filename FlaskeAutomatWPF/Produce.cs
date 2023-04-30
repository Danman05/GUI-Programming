using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace FlaskeAutomatWPF
{
    internal class Produce
    {

        // Produce class that continuously monitors drinkQ
        // It uses Monitor.Enter and Monitor.Exit to synchronize access to the shared queues between threads.
        // The method runs in an infinite loop with a delay of 500ms between each iteration.


        /// <summary>
        /// produces and enqueues beer and soda bottles in Splitter's drinkQ queue, 
        /// ensuring that it always contains at least 20 bottles.
        /// Random object is used to randomly select between beer and soda bottle types.
        /// </summary>
        public static void ProduceBottle()
        {

            Random random = new();
            int randomNum;
            string writeProduceString = "";


            while (true)
            {
                try
                {
                   
                    Monitor.Enter(Splitter.drinkQ);
                    if (Splitter.drinkQ.Count < 3)
                    {
                        while (Splitter.drinkQ.Count < 20)
                        {
                            writeProduceString = "";
                            randomNum = random.Next(1, 3);

                            if (randomNum == 1)
                            {
                                Splitter.drinkQ.Enqueue(new Beer());
                            }
                            else
                            {
                                Splitter.drinkQ.Enqueue(new Soda());
                            }
                        }
                        Monitor.PulseAll(Splitter.drinkQ);
                    }
                    else
                    {
                        writeProduceString = "[Info] Bottle producer waits";
                    }
                }
                finally
                {

                    Debug.WriteLine($"Unsorted bottles: {Splitter.drinkQ.Count}");
                    Debug.WriteLine($"{writeProduceString}");
                    Monitor.Exit(Splitter.drinkQ);
                    Thread.Sleep(500);
                }
            }
        }
    }
}
