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
using System.Windows.Media.Animation;

namespace FlaskeAutomatWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly Thread producerThread = new(Produce.ProduceBottle);
        readonly Thread splitterConsumerThread = new(Splitter.SplitterConsumer);
        readonly Thread consumerDrinkThread = new(Disposer.DisposeBottle);

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new Splitter();
        }

        private void StartProduceBtn_Click(object sender, RoutedEventArgs e)
        {

            // Runs the threads if they are not alive
            if (!producerThread.IsAlive)
                producerThread.Start();

            if (!splitterConsumerThread.IsAlive)
                splitterConsumerThread.Start();

            if (!consumerDrinkThread.IsAlive)
                consumerDrinkThread.Start();

        }
    }
}
