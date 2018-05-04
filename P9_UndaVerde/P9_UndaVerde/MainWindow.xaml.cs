using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace P9_UndaVerde
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //Se initiaslizeaza componentele
            InitializeComponent();
        }
        
        private void aplicationExit(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void startAnimation(object sender, RoutedEventArgs e)
        {
            
            Storyboard story = new Storyboard();
           
            Canvas.SetLeft(car, canvasCar.ActualWidth);
            Canvas.SetTop(car, canvasCar.ActualHeight - 265);
            NameScope.SetNameScope(this, new NameScope());
            MatrixTransform carTransform = new MatrixTransform();
            car.RenderTransform = carTransform;
            this.RegisterName("carTransform", carTransform);

            PathGeometry animPath = new PathGeometry();
            PathFigure pathFigure = new PathFigure();
            pathFigure.StartPoint = new Point(0, 0);

            pathFigure.Segments.Add(new LineSegment(new Point(-170, 0), false));
            //pathFigure.Segments.Add(new ArcSegment(new Point(-100,50), new Size(20,10),15,false,SweepDirection.Clockwise, false));
            pathFigure.Segments.Add(new LineSegment(new Point(-170, -120), false));

            animPath.Figures.Add(pathFigure);
            animPath.Freeze();

            MatrixAnimationUsingPath mAnim = new MatrixAnimationUsingPath();
            mAnim.PathGeometry = animPath;
            mAnim.Duration = TimeSpan.FromSeconds(3);
            mAnim.DoesRotateWithTangent = true;

            Storyboard.SetTargetName(mAnim, "carTransform");
            Storyboard.SetTargetProperty(mAnim, new PropertyPath(MatrixTransform.MatrixProperty));
            story.Children.Add(mAnim);
            


            Canvas.SetLeft(car1, canvasCar.ActualWidth);
            Canvas.SetTop(car1, canvasCar.ActualHeight - 257);
           
            MatrixTransform carTransform1 = new MatrixTransform();
            car1.RenderTransform = carTransform1;
            this.RegisterName("carTransform1", carTransform1);

            PathGeometry animPath1 = new PathGeometry();
            PathFigure pathFigure1 = new PathFigure();
            pathFigure1.StartPoint = new Point(0, 0);

            pathFigure1.Segments.Add(new LineSegment(new Point(-210, 0), false));
            //pathFigure.Segments.Add(new ArcSegment(new Point(-100,50), new Size(20,10),15,false,SweepDirection.Clockwise, false));
            pathFigure1.Segments.Add(new LineSegment(new Point(-1000, 0), false));

            animPath1.Figures.Add(pathFigure1);
            animPath1.Freeze();

            MatrixAnimationUsingPath mAnim1 = new MatrixAnimationUsingPath();
            mAnim1.PathGeometry = animPath1;
            mAnim1.Duration = TimeSpan.FromSeconds(5);
            mAnim1.DoesRotateWithTangent = false;

            Storyboard.SetTargetName(mAnim1, "carTransform1");
            Storyboard.SetTargetProperty(mAnim1, new PropertyPath(MatrixTransform.MatrixProperty));
            Storyboard story1 = new Storyboard();
            story1.Children.Add(mAnim1);

            story.Begin(this);
            story1.Begin(this);

            var tokenSource = new CancellationTokenSource();
            var ct = tokenSource.Token;
            var UiSyncContext = TaskScheduler.FromCurrentSynchronizationContext();


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
    }
}