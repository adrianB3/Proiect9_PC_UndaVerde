using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using TrafficSimTM;

namespace P9_UndaVerde
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
        
        private void aplicationExit(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void startAnimation(object sender, RoutedEventArgs e)
        {
            

            var tokenSource = new CancellationTokenSource();
            var ct = tokenSource.Token;
            var UiSyncContext = TaskScheduler.FromCurrentSynchronizationContext();

            Car car1 = new Car("car.png", "car1", 45, 35, 130, 0);
            Car car2 = new Car("car.png", "car2", 45, 35, 160, 0);

            Car car3 = new Car("car.png", "car3", 45, 35, 130, 50);


            Car car4 = new Car("car.png", "car3", 45, 35, 130, 100);


            Car car5 = new Car("car.png", "car3", 45, 35, 130, 150);


            Animation anim = new Animation();


            Animation anim2 = new Animation();


            Animation anim3 = new Animation();


            Animation anim4 = new Animation();


            Animation anim5 = new Animation();

            car1.createImage();
            car2.createImage();
            car3.createImage();
            car4.createImage();
            car5.createImage();

            anim.startAnimation(car1,3, 200);
            anim2.startAnimation(car2,3, 1000);
            anim3.startAnimation(car3,3, 2000);
            anim4.startAnimation(car4,3, 2000);
            anim5.startAnimation(car4,3, 2000);


            /*var t1 = Task.Factory.StartNew(() =>
            {

               pbCalculationProgress1.Value = 10;
               Thread.Sleep(1000);

            }, ct, TaskCreationOptions.None,UiSyncContext);*/

            /*Dispatcher.BeginInvoke(new Action(delegate
            {
                for (int i = 0; i < 100; i++)
                {
                    pbCalculationProgress1.Value = i;
                    Thread.Sleep(100);
                }
            }));*/

            Progress<int> progress = new Progress<int>();
            Task tsk = new Task(() =>
            {
                for (int i = 0; i <= 100; i++)
                {
                    ((IProgress<int>)progress).Report(i);
                    Thread.Sleep(50);
                }
            });

            progress.ProgressChanged += change;

            tsk.Start();

            Progress<int> progress1 = new Progress<int>();
            Task tsk1 = new Task(() =>
            {
                int i = 0;
                Thread.Sleep(1000);
                i = 1;
                ((IProgress<int>)progress).Report(i);
            });
            progress1.ProgressChanged += change1;

            tsk1.Start();
            /* Task.Factory.StartNew(() =>
             {
                 Thread.Sleep(1000);
                 label1.Content = "I waited 1 sec to get here";
             },ct, TaskCreationOptions.None, UiSyncContext);*/




            BackgroundWorker worker = new BackgroundWorker();
            pbCalculationProgress.Value = 0;
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerAsync(100);
                
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i <= (int)e.Argument; i++)
            {
                int progressPercentage = i;
                (sender as BackgroundWorker).ReportProgress(progressPercentage,i);              
                Thread.Sleep(100);
                e.Result = i.ToString();
            }
        }
        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbCalculationProgress.Value = e.ProgressPercentage;
            if (e.UserState != null)
                labell.Content = e.UserState;
        }

        void change(object sender, int i)
        {
            pbCalculationProgress1.Value = i;
        }

        void change1(object sender, int i)
        {
            if(i == 1)
            {
                label1.Content = "I waited 1 second to get here";
            }
        }

        private void windowLoaded(object sender, RoutedEventArgs e)
        {
            SemaphoreUI sem1 = new SemaphoreUI("sem1",false,20,20,0,40,100);           
        }

        private void stopAnimation(object sender, RoutedEventArgs e)
        {
            
        }
    }
}