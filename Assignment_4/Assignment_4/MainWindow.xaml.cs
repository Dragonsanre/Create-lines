using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace Assignment_4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ArrayList threadList = new ArrayList();

        public ArrayList lineList = new ArrayList();
        int count = 0;
        bool paused = false;
        bool started = false;
        int currentvalue = 1;
        DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = TimeSpan.FromMilliseconds(50);
            timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {

            Stick stick1 = new Stick();
            int newcounter = 0;
            int maxlines = 0;
            int counter = 0;
            timer.Stop();
            if (sldrName.Value == currentvalue)
            {

            }
            else if (sldrName.Value > currentvalue)
            {
                while(sldrName.Value > currentvalue)
                {
                    currentvalue += 1;
                }
                
                sldrName.Value = currentvalue;
            }
            else if (sldrName.Value < currentvalue)
            {
                while (sldrName.Value < currentvalue)
                {
                    currentvalue -= 1;
                }
                sldrName.Value = currentvalue;
            }
            if (started == true && paused == false)
            {
                while (newcounter < lineList.Count)
                {
                    maxlines = currentvalue;
                    stick1.Assign = (Stick)lineList[newcounter];
                    while (stick1.Lines > maxlines)
                    {
                        threadList.RemoveAt(0);
                        this.stickArea.Children.Remove(stick1.RemoveLine());
                        lineList.RemoveAt(newcounter);
                        lineList.Insert(newcounter, stick1);
                    }
                    newcounter++;
                }
            }
            else if(paused == true)
            {
                while (counter < lineList.Count)
                {
                    stick1.Assign = (Stick)lineList[counter];
                    if (stick1.Lines > 0)
                    {
                        threadList.RemoveAt(0);
                        this.stickArea.Children.Remove(stick1.RemoveLine());
                        lineList[counter] = stick1;

                    }
                    counter++;
                }
            }
            count = 0;
            if (paused == false)
            {
                
            }
            
            timer.Start();
        }
        
        private void bStart_Click(object sender, RoutedEventArgs e)
        {
            started = true;
            Thread t1 = new Thread(new ThreadStart(DrawLine));
            t1.SetApartmentState(ApartmentState.STA);
            t1.Start();
            threadList.Add(t1);


        }
        private void bStop_Click(object sender, RoutedEventArgs e)
        {
            int newcounter = 0;
            Stick stick1 = new Stick();
            while(newcounter < lineList.Count)
            {
                stick1.Assign = (Stick)lineList[newcounter];
                stick1.State = false;
                lineList[newcounter] = stick1;
                newcounter++;
            }
        }
        private void bPause_Click(object sender, RoutedEventArgs e)
        {
            paused = true;
        }
        private void bResume_Click(object sender, RoutedEventArgs e)
        {
            paused = false;
        }
        public void DrawLine()
        {
            Stick newStick = new Stick(this.stickArea.ActualWidth, this.stickArea.ActualHeight);
            Random random = new Random();
            this.Dispatcher.Invoke(() =>
            {
                this.stickArea.Children.Add(newStick.CreateLine(random.Next(0,(int)this.stickArea.Width), random.Next(0, (int)this.stickArea.Width), random.Next(0, (int)this.stickArea.Height), random.Next(0, (int)this.stickArea.Height)));
                lineList.Add(newStick);
            });

        }
        public void NewLine(object stick)
        {
            Stick stick1 = new Stick();
            Thread t1 = new Thread(new ParameterizedThreadStart(NewLine));
            this.Dispatcher.Invoke(() =>
            {
                stick1.Assign = (Stick)stick;
                this.stickArea.Children.Add(stick1.MoveLine());
                Thread.Sleep(50);
                t1.SetApartmentState(ApartmentState.STA);
                t1.Start(stick1);

                if (stick1.Lines > this.sldrName.Value)
                {
                    threadList.RemoveAt(0);
                    this.stickArea.Children.Remove(stick1.RemoveLine());
                }
            
            });

        }
    }
}
