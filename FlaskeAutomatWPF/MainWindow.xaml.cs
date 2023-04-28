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
        static Splitter splitter = new();
        
        Thread splitterConsumerThread = new(splitter.SplitterConsumer);
        Thread consumerDrinkThread = new(Disposer.DisposeBottle);

        static CancellationTokenSource source = new CancellationTokenSource();
        public static CancellationToken token = source.Token;
        public MainWindow()
        {
            
            InitializeComponent();

            this.DataContext = new Splitter();
        }

        private void StartProduceBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new();
            Produce produce = new();
            Thread producerThread = new(produce.ProduceBottle);

            Splitter spl = new(main);
            Thread splittercons = new(spl.SplitterConsumer);

            //Image image = new Image();
            //image.Source = new BitmapImage(new Uri("Ressource/beerCan.png", UriKind.Relative));
            //image.Width = 50;
            //image.Height = 50;
            //otherGrid.Children.Add(image);


            //Thread makeAnimation = new(AnimateDrink);

            //makeAnimation.Start();

            if (!producerThread.IsAlive)
                producerThread.Start();

            if (!splittercons.IsAlive)
                splittercons.Start();

            if (!consumerDrinkThread.IsAlive)
                consumerDrinkThread.Start();

        }


        void MakeImage()
        {

        }

        public void AnimateDrink(object drinkType)
        {
            int drinkNumber = 1;
            string filePath = "";

            if (drinkNumber == 1)
                filePath = "Ressource/sodaCan.png";
            else if (drinkNumber == 2)
                filePath = "Ressource/beerCan.png";

            double originalLeft = 0;
            double originalTop = 0;

            double currentLeft;
            double currentTop;


            // Add the image to the canvas on the UI thread
            
                Debug.WriteLine(Dispatcher.CurrentDispatcher);
                Image image = new Image();
                image.Source = new BitmapImage(new Uri("Ressource/beerCan.png", UriKind.Relative));
                image.Width = 50;
                image.Height = 50;
                otherGrid.Children.Add(image);
                Thread.Sleep(100);
            
           

        }
    }
}
