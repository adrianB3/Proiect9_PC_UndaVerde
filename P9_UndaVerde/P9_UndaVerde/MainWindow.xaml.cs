using System;
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
        SemaphoreSystem theSystem;
        Animation carAnim, carAnim1, car90Anim, car90Anim2;
      
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

            List<Point> coordinates = new List<Point>()
            {
               new Point (40, 100 ),
               new Point (40, 300 ),
               new Point (40, 470 ),
               new Point (40, 680 ),
               new Point (40, 990 ),
            };

            theSystem = new SemaphoreSystem();

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
            Car train1 = new Car("Train.png", "train1", 140, 80, 180, 0);//train1
            Car train2 = new Car("Train.png", "train2", 140, 80, 160, 0);//train2
            Car car90_1 = new Car("car90.png", "car90_1", 35, 45, 0, 188);//top to bottom
            Car car90_2 = new Car("car90.png", "car90_2", 35, 45, 0, 390);//top to bottom
            Car redCar90_3 = new Car("redcar90.png", "redCar90_3", 35, 45, 390, 140);//bottom to top

            carAnim = new Animation(new Point(-90,0));
            carAnim1 = new Animation(new Point(-1240, 0));
            car90Anim = new Animation(new Point(0, 1000));
            car90Anim2 = new Animation(new Point(0, -440));

            car1.createImage();
            car2.createImage();
            train1.createImage();//train
            train2.createImage();
            car90_1.createImage();
            car90_2.createImage();
            redCar90_3.createImage();
            theSystem.StartSystem();

            carAnim.startAnimation(car1, 4, 200);           
            carAnim1.startAnimation(car2, 3, 200);

            carAnim1.startAnimation(train1, 3, 200);

            carAnim.startAnimation(train2, 5, 200);
            car90Anim.startAnimation(car90_1, 5, 200);
            car90Anim.startAnimation(car90_2, 3, 200);
            car90Anim2.startAnimation(redCar90_3, 4, 200);


            carAnim.story.Completed += delegate {

                carAnim.story.Remove();
                Animation carAnim2 = new Animation(new Point(-1024, 0));
                carAnim2.startAnimation(car1, 3, 200);
            };
        }

        private void windowLoaded(object sender, RoutedEventArgs e)
        {
                     
        }

        private void stopAnimation(object sender, RoutedEventArgs e)
        {
            carAnim.stopAnimation();
            carAnim1.stopAnimation();
        }

        private void pauseAnimation(object sender, RoutedEventArgs e)
        {
            carAnim.pauseAnimation();
            carAnim1.pauseAnimation();
        }
    }
}