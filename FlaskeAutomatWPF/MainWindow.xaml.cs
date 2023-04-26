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
            Thread splitterConsumerThread = new(Splitter.SplitterConsumer);
            Thread consumerDrinkThread = new(Disposer.DisposeBottle);

            producerThread.Start();
            splitterConsumerThread.Start();
            consumerDrinkThread.Start();
            
        }


    }
}
