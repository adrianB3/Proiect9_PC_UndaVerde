using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
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
    public class startLocation
    {
        public string _name { get; set; }
        public Point _startPoint { get; set; }
    }

    public partial class MainWindow : Window
    {
        private readonly List<Point> _intersection1SemPoints = new List<Point>() {
            new Point (40, 85),
            new Point (5, 235),
            new Point (230, 240 ),
            new Point (235, 90 ),
        };
        private readonly List<Point> _intersection2SemPoints = new List<Point>() {
            new Point (10, 455 ),
            new Point (35, 635 ),
            new Point (230, 645 ),
            new Point (235, 465 ),
            new Point (120,600)
        };
        private readonly List<Point> _intersection3SemPoints = new List<Point>() {
            new Point (40, 980 ),
            new Point (35, 1130 ),
            new Point (230, 1135 ),
            new Point (240, 987 ),
        };

        public Dictionary<Point, string> _startLocations = new Dictionary<Point, string>() {
            {new Point(140, 0),"Liviu Rebreanu RF"},
            {new Point(140, 1200),"Liviu Rebreanu FR"},
            {new Point(0, 200),"Cosminului"},
            {new Point(0, 500),"Drubeta"},
            {new Point(0, 700),"C-tin Brancoveanu"},
        };
        private List<Intersection> Intersections = new List<Intersection>();
        private List<Sensor> Sensors = new List<Sensor>();

        // Current Scenario data structures
        private List<Car> carsList = new List<Car>();
        private List<Task> listOfTasks = new List<Task>();
        private List<startLocation> currentStartLocations = new List<startLocation>();

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

            Sensors.Add(new Sensor(0,0,20,150));
            Sensors.Add(new Sensor(1,0,450,150));
            Sensors.Add(new Sensor(2,0,980,150));
            Sensors.Add(new Sensor(3,0,1150,270));

            List<startLocation> startLocationsList = new List<startLocation>();
            foreach (var loc in _startLocations)
            {
                startLocationsList.Add(new startLocation() { _name = loc.Value, _startPoint = loc.Key});
            }

            StreetList.ItemsSource = startLocationsList;
            currentStartLocations.Add(new startLocation() {_name = "test", _startPoint = new Point(0,0)});

        }
         
        private void ApplicationExit(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
        
        private void startAnimation(object sender, RoutedEventArgs e)
        {
            List<Animation> Animations = new List<Animation>();

            Animations.Add(new Animation(new Point(0,0), new Point(-90,0)));
            Animations.Add(new Animation(new Point(-90,0), new Point(-480,0)));
            Animations.Add(new Animation(new Point(-480,0), new Point(-985,0)));
            Animations.Add(new Animation(new Point(-985,0), new Point(-1200,0)));

            List<Animation> Animations1 = new List<Animation>();

            Animations1.Add(new Animation(new Point(0, 0), new Point(75, 0)));
            Animations1.Add(new Animation(new Point(75, 0), new Point(530, 0)));
            Animations1.Add(new Animation(new Point(550, 0), new Point(950, 0)));
            Animations1.Add(new Animation(new Point(980, 0), new Point(1250, 0)));

            List<Animation> Animations2 = new List<Animation>();
            Animations2.Add(new Animation(new Point(0,0), new Point(0,90)));
            Animations2.Add(new Animation(new Point(0,90), new Point(0,700)));

            List<Animation> Animations3 = new List<Animation>();
            Animations3.Add(new Animation(new Point(0, 0), new Point(-400, 0)));
            Animations3.Add(new Animation(new Point(-400, 0), new Point(-1200, 0)));

            carsList.Add(new Car(new []{0,1,2},0,Animations,"car.png","car",45,25,135,0,200));
            carsList.Add(new Car(new []{0, 1, 2}, 0, Animations, "redcar.png", "car1", 45, 25, 165, 0, 100));
            carsList.Add(new Car(new []{2, 1, 0},2 ,Animations1, "redcar180.png", "car2", 45, 25, 255, 1180, 150));
            carsList.Add(new Car(new []{1},1 ,Animations2, "car90.png", "car3", 65, 45, 0, 550, 120));
            carsList.Add(new Car(new []{1},4 ,Animations3, "train.png", "train", 85, 55, 180, 0, 300));
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            
            foreach (var car in carsList)
            {
                car.createImage();
                listOfTasks.Add(new Task(async () =>
                {
                    int i = 0;
                    foreach (var animation in car._animationsList)
                    {
                        animation.startAnimation(car, animation.speedCalculation(car), 0);
                        await Task.Delay(animation.speedCalculation(car)*1000);
                        while (Intersections[car.intersectionsTraveled[i]]._TrafficLights[car.semType].isRed())
                        {
                            await Task.Delay(100);
                        }

                        if (i < car.intersectionsTraveled.Length - 1)
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

        private void StartTrafficLightsSync(object sender, RoutedEventArgs e)
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

        }

        private void AddCar(object sender, RoutedEventArgs e)
        {
            // TODO!!!!!!

            //Animation MyAnimation = new Animation(new Point(-90, 0), new Point(-480, 0));
            //carsList.Add(new Car(animationsList[0],"car.png", "car", 45, 25, 135, 0, 2));
        }

        private void addStreet(object sender, MouseButtonEventArgs e)
        {           
            selectedItems.ItemsSource = currentStartLocations;
        }

        private void clearScenario(object sender, RoutedEventArgs e)
        {
            foreach (var car in carsList)
            {
                car.removeImage();
            }
            carsList.Clear();
            listOfTasks.Clear();
        }

        private void StartSensorMonitor(object sender, RoutedEventArgs e)
        {
            foreach (var sensor in Sensors)
            {
                sensor.startSensor();
            }
        }

        private void StopSensorMonitor(object sender, RoutedEventArgs e)
        {
            foreach (var sensor in Sensors)
            {
                sensor.stopSensor();
            }
        }
    }
}