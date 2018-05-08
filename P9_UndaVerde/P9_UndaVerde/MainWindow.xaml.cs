﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Timers;
using TrafficSimTM;

namespace P9_UndaVerde
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public class credits
    {
        public int nr { get; set; }
        public string Name { get; set; }
    }

    public partial class MainWindow : Window
    {
        SemaphoreUI sem1;
        SemaphoreUI sem2;
        SemaphoreUI sem3;
        SemaphoreUI sem4;
        SemaphoreUI sem5;

        Animation carAnim;

        private static System.Timers.Timer aTimer;
        public MainWindow()
        {
            InitializeComponent();
            List<credits> credits = new List<credits>();
            string names = "Andreea Carp @AndreeaCamelia, Andrei Chirap @AndreiChirap, Adrian-Gabriel Balanescu @adrianB3, Gabriel Bizdoc @GabiBVG ,Raul-Adrian Chincea @RaulChincea, Diana Dalea @dianadalea,Andreea Balasoiu @AndreeaBalasoiu, Alina Bacalete @AlinaBacalete, Voicu Carole @carolevoicu, Anamaria Larisa Bala @AnamariaLarisa, Simona-Rebeca Buse @SimonaRebeca, Adrian Coneac @adrianconeac, Mario-Razvan Cioara @MarioCioara, Raluca-Andreea Cozma @ralucacozma, Raul Cojocaru @raulcojocaru, Robert Burdusel @robertb21";
            var n = names.Split(',');
            int index = 1;
            foreach (string item in n)
            {
                credits.Add(new credits() { Name = item , nr = index++});
            }

            theCreators.ItemsSource = credits;

            sem1 = new SemaphoreUI("sem1", false, 20, 20, 0, 40, 100);
            sem2 = new SemaphoreUI("sem2", false, 20, 20, 0, 40, 300);
            sem3 = new SemaphoreUI("sem3", false, 20, 20, 0, 40, 470);
            sem4 = new SemaphoreUI("sem4", false, 20, 20, 0, 40, 680);
            sem5 = new SemaphoreUI("sem5", false, 20, 20, 0, 40, 990);

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
            Car car2 = new Car("redcar.png", "car2", 45, 35, 160, 0);

            carAnim = new Animation(new Point(-1240,0));

            car1.createImage();
            car2.createImage();
            

            carAnim.startAnimation(car1,3, 200);
            
            carAnim.startAnimation(car2, 3, 200);

            var timer = new System.Timers.Timer(1000);
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Enabled = true;

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
            }));

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

           
            BackgroundWorker worker = new BackgroundWorker();
            pbCalculationProgress.Value = 0;
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerAsync(100);*/
           
                
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            liveTime.Text = e.SignalTime.ToShortTimeString();
        }

        /*void worker_DoWork(object sender, DoWorkEventArgs e)
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
        }*/


        private void windowLoaded(object sender, RoutedEventArgs e)
        {
                     
        }

        private void stopAnimation(object sender, RoutedEventArgs e)
        {
            carAnim.stopAnimation();
        }

        private void pauseAnimation(object sender, RoutedEventArgs e)
        {
            carAnim.pauseAnimation();
        }
    }
}