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
            Task tsk = new Task(async () =>
            {
                foreach (var intersection in Intersections)
                {
                    intersection.StartIntersectionSync();
                    await Task.Delay(2000);
                }
            });

            tsk.Start(TaskScheduler.FromCurrentSynchronizationContext());

            List<Animation> Animations = new List<Animation>();

            Animations.Add(new Animation(new Point(0,0), new Point(-90,0)));
            Animations.Add(new Animation(new Point(-90,0), new Point(-495,0)));
            Animations.Add(new Animation(new Point(-495,0), new Point(-985,0)));
            Animations.Add(new Animation(new Point(-985,0), new Point(-1200,0)));

            carsList.Add(new Car(Animations,"car.png","car",45,25,135,0,2));

            foreach (var car in carsList)
            {
                car.createImage();
            }

            Task tsk1 = new Task(async () =>
            {
                
                foreach (var car in carsList)
                {
                    int i = 0;
                    await Task.Factory.StartNew( async () =>
                    {
                        
                        foreach (var animation in Animations)
                        {
                           
                            animation.startAnimation(car, animation.speedCalculation(car), 0);
                            animation.story.Completed += (async (o, args) =>
                            {
                                while (Intersections[i]._TrafficLights[0].isRed())
                                {
                                    await Task.Delay(100);
                                }
                            });
                        }
                    },TaskCreationOptions.AttachedToParent);
                    i++;

                    Task.WaitAny();
                }

            });

            tsk1.Start(TaskScheduler.FromCurrentSynchronizationContext());

        // lista de animatii
        // lista masini
        // lista semafoare pe traseu
        /*Task tsk1 = new Task(async () =>
        {
            anim1.startAnimation(car1, anim1.speedCalculation(car1), 2);
            anim1.story.Completed += (async (o, args) =>
            {
                while(_intersection1._TrafficLights[0].isRed())
                {
                    await Task.Delay(100);
                }
                Animation anim2 = new Animation(new Point(-100, 0), new Point(-1200, 0));
                anim2.startAnimation(car1, anim2.speedCalculation(car1), 0);
            });
        });

        tsk1.Start(TaskScheduler.FromCurrentSynchronizationContext());*/

        /*List<List<Animation>> lst = new List<List<Animation>>()
        {
           new List<Animation>() { new Animation(new Point(0, 0), new Point(-100, 0)), new Animation(new Point(-100, 0), new Point(-200, 0)) }
        };
        CountdownEvent cde = new CountdownEvent(2);

        void Consumer()
        {
            Task tsk2 = new Task(async () =>
            {
                foreach (var ls in lst)
                {
                    foreach (var anim in ls)
                    {
                        anim.startAnimation(car1, anim.speedCalculation(car1), 2);
                        anim.story.Completed += (async (o, args) =>
                        {
                            while (_intersection1._TrafficLights[0].isRed())
                            {
                                await Task.Delay(100);
                            }
                        });
                    }
                }
            });
            tsk2.Start();
            cde.Signal();
        }

        Task tsk1 = new Task(Consumer);
        tsk1.Start(TaskScheduler.FromCurrentSynchronizationContext());

        cde.Wait();*/

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