using System;
using System.Collections.Concurrent;
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
        private readonly List<Point> _intersection1SemPoints = new List<Point>() {
            new Point (45, 90),
            new Point (35, 230),
            new Point (230, 230 ),
            new Point (235, 90 ),
        };
        private readonly List<Point> _intersection2SemPoints = new List<Point>() {
            new Point (45, 465 ),
            new Point (35, 630 ),
            new Point (230, 635 ),
            new Point (235, 470 ),
        };
        private readonly List<Point> _intersection3SemPoints = new List<Point>() {
            new Point (45, 985 ),
            new Point (35, 1120 ),
            new Point (230, 1125 ),
            new Point (235, 991 ),
        };

        private List<Intersection> Intersections = new List<Intersection>();
        private List<Car> carsList = new List<Car>();
        private List<Task> listOfTasks = new List<Task>();


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

            Intersections.Add(new Intersection(_intersection1SemPoints));
            Intersections.Add(new Intersection(_intersection2SemPoints));
            Intersections.Add(new Intersection(_intersection3SemPoints));     
        }
        
        private void ApplicationExit(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
        
        private void startAnimation(object sender, RoutedEventArgs e)
        {
            Task intersectionSyncTask = new Task(async () =>
            {
                foreach (var intersection in Intersections)
                {
                    intersection.StartIntersectionSync();
                    await Task.Delay(2000);
                }
            });

            intersectionSyncTask.Start(TaskScheduler.FromCurrentSynchronizationContext());

            List<Animation> Animations = new List<Animation>();

            Animations.Add(new Animation(new Point(0,0), new Point(-90,0)));
            Animations.Add(new Animation(new Point(-90,0), new Point(-480,0)));
            Animations.Add(new Animation(new Point(-480,0), new Point(-985,0)));
            Animations.Add(new Animation(new Point(-985,0), new Point(-1200,0)));

            List<Animation> Animations1 = new List<Animation>();

            Animations1.Add(new Animation(new Point(0, 0), new Point(75, 0)));
            Animations1.Add(new Animation(new Point(75, 0), new Point(550, 0)));
            Animations1.Add(new Animation(new Point(550, 0), new Point(980, 0)));
            Animations1.Add(new Animation(new Point(980, 0), new Point(1250, 0)));

            carsList.Add(new Car(Animations,"car.png","car",45,25,135,0,2));
            carsList.Add(new Car(Animations, "redcar.png", "car1", 45, 25, 165, 0, 2));
            carsList.Add(new Car(Animations1, "redcar180.png", "car2", 45, 25, 255, 1180, 2));

            
            foreach (var car in carsList)
            {
                car.createImage();
                listOfTasks.Add(new Task(async () =>
                {
                    int i = 0;
                    foreach (var animation in car._animationsList)
                    {
                        animation.startAnimation(car,1,0);
                        
                        while (Intersections[i]._TrafficLights[0].isRed())
                        {
                          await Task.Delay(100);
                        }
                        if(i<2)
                            i++;
                    }
                }));
            }

            foreach (var task in listOfTasks)
            {
                task.Start(TaskScheduler.FromCurrentSynchronizationContext());
            }
        }
        
        private void windowLoaded(object sender, RoutedEventArgs e)
        {
                     
        }

        private void stopAnimation(object sender, RoutedEventArgs e)
        {
            
        }

        private void pauseAnimation(object sender, RoutedEventArgs e)
        {
            
        }
    }
}