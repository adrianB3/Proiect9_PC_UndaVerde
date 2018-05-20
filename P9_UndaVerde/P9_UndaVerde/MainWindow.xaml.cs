using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
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

    // clasa folosita pentru stocarea elementelor selectate de utilizator din meniu
    public class selectedItem
    {
        public string carType { get; set; }
        public string path { get; set; }
        public bool isBad { get; set; }
        public int speed { get; set; }
    }

    // clasa folosita pentru stocarea Pathului si semafoarelor intalnite de o masinuta intr-o anumita animatie
    public class SelectedPath
    {
        public List<Animation> Anims;
        public List<ints> intersectionAndSemType;
        public int posFromTop;
        public int posFromRight;

        public SelectedPath(List<Animation> anm, List<ints> ints, int posT, int posR)
        {
            Anims = anm;
            intersectionAndSemType = ints;
            posFromTop = posT;
            posFromRight = posR;
        }
    }

    public partial class MainWindow : Window
    {
        // Coordonate Semafoare
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
            new Point (120,600),
            new Point (140,600)
        };
        private readonly List<Point> _intersection3SemPoints = new List<Point>() {
            new Point (40, 980 ),
            new Point (35, 1130 ),
            new Point (230, 1135 ),
            new Point (240, 987 ),
        };
        // Liste cu optiunile ce pot fi selectate de utilizator
        private List<string> AvailableVehicles = new List<string>() {"Grey car", "Red car", "Train"};
        private List<string> AvailablePaths = new List<string>()
        {
            "L.Rebreanu Mart B1 --> L.Rebreanu Sag B1", //0
            "L.Rebreanu Mart B2 --> L.Rebreanu Sag B2", //1
            "L.Rebreanu Mart B3 --> Drubeta 1", //2
            "L.Rebreanu Mart B2 --> C-tin Brancoveanu 1", //3
            "L.Rebreanu Sag B1 --> L.Rebreanu Mart B1", //4
            "L.Rebreanu Sag B2 --> L.Rebreanu Mart B2", //5
            "L.Rebreanu Sag B1 --> Drubeta 1", //6
            "Drubeta 1 --> Drubeta 2", //7
            "Cosminului 1 --> Drubeta 1", //8
            "Cosminului 1 --> Drubeta 2", //9
        };
        private List<Intersection> Intersections = new List<Intersection>();
        private ObservableCollection<Sensor> Sensors = new ObservableCollection<Sensor>();

        private char name = 'A';
        // Current Scenario data structures
        private List<Car> carsList = new List<Car>();
        private List<Task> listOfTasks = new List<Task>();
        ObservableCollection<Car> listOfBadCars = new ObservableCollection<Car>();
        ObservableCollection<selectedItem> selectedThings = new ObservableCollection<selectedItem>();
        CancellationTokenSource tokenSource = new CancellationTokenSource(); // token folosit pentru oprirea unui anumit Task

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

            Sensors.Add(new Sensor("Sensor1",0,0,90,155));
            Sensors.Add(new Sensor("Sensor2", 1,0,450,155));
            Sensors.Add(new Sensor("Sensor3", 2,0,980,155));
            Sensors.Add(new Sensor("Sensor4", 2,2,1150,250));
            Sensors.Add(new Sensor("Sensor5", 1,2,650,250));
            Sensors.Add(new Sensor("Sensor6", 0,2,300,250));

            AvailableCarTypes.ItemsSource = AvailableVehicles;
            AvailablePathsListBox.ItemsSource = AvailablePaths;
            selectedItemsListView.ItemsSource = selectedThings;
            listBoxOfBadCars.ItemsSource = listOfBadCars;

        }
         
        // Event apelat la inchiderea ferestri
        private void ApplicationExit(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
        
        // Event apelat la apasarea butonului "Start Simulation"
        private void startAnimation(object sender, RoutedEventArgs e)
        {
            // Creare task pentru fiecare masinuta
            foreach (var car in carsList)
            {
                if (tokenSource.IsCancellationRequested)
                {
                    break;
                }
                car.createImage();
                // Lista de taskuri care asigura deplasarea unei masinute in mod paralel fata de celelalte masinute
                listOfTasks.Add(new Task(async () =>
                {
                    int i = 0;
                    foreach (var animation in car._animationsList)
                    {
                        // Declansare secventa de animatii pentru fiecare masinuta
                        animation.startAnimation(car, animation.speedCalculation(car), 0);
                        foreach (var sensor in Sensors)
                        {
                            sensor.startSensor();
                            if (sensor._indexIntersectie == car.intSem[i].intersection &&
                                sensor._indexSemafor == car.intSem[i].semType
                            )
                            {
                                sensor._Signal();
                            }                                             
                        }
                        if (Sensors[0]._isCrowded() && Sensors[3]._isCrowded())
                        {
                            ActivateGreenWave(new object(), new RoutedEventArgs());
                        }
                        await Task.Delay(animation.speedCalculation(car)*1000);  
                        // daca semaforul la care a ajuns masinuta la un anumit moment este rosu, taskul asteapta ca acel semafor sa devina verde
                        while (Intersections[car.intSem[i].intersection]._TrafficLights[car.intSem[i].semType].isRed() && car._isABadCar == false)
                        {
                            await Task.Delay(100);
                        }
                        foreach (var sensor in Sensors)
                        {
                            if (sensor._indexIntersectie == car.intSem[i].intersection &&                               
                                sensor._indexSemafor == car.intSem[i].semType
                            )
                            {
                                sensor._Reset();
                            }
                        }

                        if (i < car.intSem.Count - 1)
                        {
                            i++;
                            // daca o anumita masinuta trece pe rosu este adaugata in lista masinutelor ce au trecut pe rosu
                            if (car._isABadCar && Intersections[car.intSem[i].intersection]._TrafficLights[car.intSem[i].semType].isRed())
                            {
                                listOfBadCars.Add(car);
                            }
                        }
                    }
                    
                },tokenSource.Token));
            }

            // pornire taskuri ptr fiecare masinuta
            foreach (var task in listOfTasks)
            {
                task.Start(TaskScheduler.FromCurrentSynchronizationContext());
            }

            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    ProgressBar1.Value = Sensors[0]._numberOfCars;
                    label1.Content = Sensors[0]._numberOfCars;
                    ProgressBar2.Value = Sensors[1]._numberOfCars;
                    label2.Content = Sensors[1]._numberOfCars;
                    ProgressBar3.Value = Sensors[2]._numberOfCars;
                    label3.Content = Sensors[2]._numberOfCars;
                    ProgressBar4.Value = Sensors[3]._numberOfCars;
                    label4.Content = Sensors[3]._numberOfCars;
                    ProgressBar5.Value = Sensors[4]._numberOfCars;
                    label5.Content = Sensors[4]._numberOfCars;
                    ProgressBar6.Value = Sensors[5]._numberOfCars;
                    label6.Content = Sensors[5]._numberOfCars;
                    await Task.Delay(100);
                }                
                
            },tokenSource.Token, TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
        }
        

        private void windowLoaded(object sender, RoutedEventArgs e)
        {
                     
        }

        private void stopAnimation(object sender, RoutedEventArgs e)
        {
          //  tokenSource.Cancel();
        }

        // Event declansat la apasare butonului de aprindere a semafoarelor
        private void StartTrafficLightsSync(object sender, RoutedEventArgs e)
        {
            // Task ce asigura pornirea sincronizarii fiecarei intersectii in paralel
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

        // Event declansat la apasare butonului Add din Meniu prin care este adaugata o noua masinuta in cadrul scenariului de simulare
        private void AddCar(object sender, RoutedEventArgs e)
        {
            // Creare lista de animatii ptr fiecare traseu disponibil
            string imgSource = "car.png";
            int posT = 160;
            int posR = 0;
            List<Animation> Animations = new List<Animation>();
            Animations.Add(new Animation(new Point(0, 0), new Point(-90, 0),0));
            Animations.Add(new Animation(new Point(-90, 0), new Point(-480, 0),0));
            Animations.Add(new Animation(new Point(-480, 0), new Point(-985, 0),0));
            Animations.Add(new Animation(new Point(-985, 0), new Point(-1200, 0),0));

            List<ints> dict = new List<ints>()
            {
                new ints() {intersection = 0, semType = 0},
                new ints() {intersection = 1, semType = 0},
                new ints() {intersection = 2, semType = 0},
            };


            SelectedPath pth = new SelectedPath(Animations, dict, posT, posR);
            
            if (AvailablePathsListBox.SelectedIndex == 0)
            {
                // L.Rebreanu Mart B1 --> L.Rebreanu Sag B1
                posT = 160;
                posR = 0;
                if (AvailableCarTypes.SelectedItem.ToString() == "Grey car")
                {
                    imgSource = "car.png";
                }
                if (AvailableCarTypes.SelectedItem.ToString() == "Red car")
                {
                    imgSource = "redcar.png";
                }
                if (AvailableCarTypes.SelectedItem.ToString() == "Train")
                {
                    imgSource = "Train.png";
                }
                pth = new SelectedPath(Animations, dict, posT, posR);
            }


            if (AvailablePathsListBox.SelectedIndex == 1)
            {
                // L.Rebreanu Mart B2 --> L.Rebreanu Sag B2
                if (AvailableCarTypes.SelectedItem.ToString() == "Grey car")
                {
                    imgSource = "car.png";
                }
                if (AvailableCarTypes.SelectedItem.ToString() == "Red car")
                {
                    imgSource = "redcar.png";
                }
                if (AvailableCarTypes.SelectedItem.ToString() == "Train")
                {
                    imgSource = "Train.png";
                }
                posT = 140;
                posR = 0;
                pth = new SelectedPath(Animations, dict, posT, posR);
            }

            if (AvailablePathsListBox.SelectedIndex == 2)
            {
                // L.Rebreanu Mart B3 --> Drubeta 1
                posT = 140;
                posR = 0;
                if (AvailableCarTypes.SelectedItem.ToString() == "Grey car")
                {
                    imgSource = "car.png";
                }
                if (AvailableCarTypes.SelectedItem.ToString() == "Red car")
                {
                    imgSource = "redcar.png";
                }
                if (AvailableCarTypes.SelectedItem.ToString() == "Train")
                {
                    imgSource = "Train.png";
                }
                List<Animation> Animations1 = new List<Animation>();
                Animations1.Add(new Animation(new Point(0, 0), new Point(-90, 0),0));
                Animations1.Add(new Animation(new Point(-90, 0), new Point(-280, 0),0));
                Animations1.Add(new Animation(new Point(-280, 0), new Point(-290, -25),0));
                Animations1.Add(new Animation(new Point(-290, -5), new Point(-400, -5),1));
                Animations1.Add(new Animation(new Point(-400, -5), new Point(-400, -20),1));
                Animations1.Add(new Animation(new Point(-500, -20), new Point(-500, -100),1));

                List<ints> dict1 = new List<ints>()
                {
                    new ints() {intersection = 0, semType = 0},
                    new ints() {intersection = 1, semType = 0},
                };
                pth = new SelectedPath(Animations1, dict1, posT, posR);
            }

            if (AvailablePathsListBox.SelectedIndex == 3)
            {
                if (AvailableCarTypes.SelectedItem.ToString() == "Grey car")
                {
                    imgSource = "car.png";
                }
                if (AvailableCarTypes.SelectedItem.ToString() == "Red car")
                {
                    imgSource = "redcar.png";
                }
                if (AvailableCarTypes.SelectedItem.ToString() == "Train")
                {
                    imgSource = "Train.png";
                }
                // L.Rebreanu Mart B2 --> C-tin Brancoveanu 1
                posT = 140;
                posR = 0;
                List<Animation> Animations2 = new List<Animation>();
                Animations2.Add(new Animation(new Point(0, 0), new Point(-90, 0), 0));
                Animations2.Add(new Animation(new Point(-90, 0), new Point(-470, 0), 0));
                Animations2.Add(new Animation(new Point(-470, 0), new Point(-985, 0), 0));
                Animations2.Add(new Animation(new Point(-1015, 0), new Point(-1015, -100), 1));

                List<ints> dict2 = new List<ints>()
                {
                    new ints() {intersection = 0, semType = 0},
                    new ints() {intersection = 1, semType = 0},
                    new ints() {intersection = 2, semType = 0},
                };
                pth = new SelectedPath(Animations2, dict2, posT, posR);
            }

           
            if (AvailablePathsListBox.SelectedIndex == 4)
            {
                // L.Rebreanu Sag B1 --> L.Rebreanu Mart B1
                if (AvailableCarTypes.SelectedItem.ToString() == "Grey car")
                {
                    imgSource = "car180.png";
                }
                if (AvailableCarTypes.SelectedItem.ToString() == "Red car")
                {
                    imgSource = "redcar180.png";
                }
                if (AvailableCarTypes.SelectedItem.ToString() == "Train")
                {
                    imgSource = "Train.png";
                }
                
                posT = 260;
                posR = 1200;
                List<Animation> Animations3 = new List<Animation>();
                Animations3.Add(new Animation(new Point(0, 0), new Point(60, 0), 0));
                Animations3.Add(new Animation(new Point(60, 0), new Point(580, 0), 0));
                Animations3.Add(new Animation(new Point(580, 0), new Point(950, 0), 0));
                Animations3.Add(new Animation(new Point(950, 0), new Point(1200, 0), 0));

                List<ints> dict3 = new List<ints>()
                {
                    new ints() {intersection = 2, semType = 2},
                    new ints() {intersection = 1, semType = 2},
                    new ints() {intersection = 0, semType = 2},
                };
                pth = new SelectedPath(Animations3, dict3, posT, posR);
            }

            if (AvailablePathsListBox.SelectedIndex == 5)
            {
                // L.Rebreanu Sag B2 --> L.Rebreanu Mart B2
                if (AvailableCarTypes.SelectedItem.ToString() == "Grey car")
                {
                    imgSource = "car180.png";
                }
                if (AvailableCarTypes.SelectedItem.ToString() == "Red car")
                {
                    imgSource = "redcar180.png";
                }
                if (AvailableCarTypes.SelectedItem.ToString() == "Train")
                {
                    imgSource = "Train.png";
                }
                posT = 260;
                posR = 1200;
                List<Animation> Animations4 = new List<Animation>();
                Animations4.Add(new Animation(new Point(0, 0), new Point(60, 0), 1));
                Animations4.Add(new Animation(new Point(60, 0), new Point(580, 0), 1));
                Animations4.Add(new Animation(new Point(580, 0), new Point(950, 0), 1));
                Animations4.Add(new Animation(new Point(950, 0), new Point(1200, 0), 1));

                List<ints> dict4 = new List<ints>()
                {
                    new ints() {intersection = 2, semType = 2},
                    new ints() {intersection = 1, semType = 2},
                    new ints() {intersection = 0, semType = 2},
                };
                pth = new SelectedPath(Animations4, dict4, posT, posR);
            }

            if (AvailablePathsListBox.SelectedIndex == 6)
            {
                // L.Rebreanu Sag B1 --> Drubeta 1
                if (AvailableCarTypes.SelectedItem.ToString() == "Grey car")
                {
                    imgSource = "car180.png";
                }
                if (AvailableCarTypes.SelectedItem.ToString() == "Red car")
                {
                    imgSource = "redcar180.png";
                }
                if (AvailableCarTypes.SelectedItem.ToString() == "Train")
                {
                    imgSource = "Train.png";
                }
                posT = 230;
                posR = 1200;
                List<Animation> Animations5 = new List<Animation>();
                Animations5.Add(new Animation(new Point(0, 0), new Point(60, 0), 0));
                Animations5.Add(new Animation(new Point(60, 0), new Point(580, 0), 0));
                Animations5.Add(new Animation(new Point(655, 0), new Point(655, -200), 1));

                List<ints> dict5 = new List<ints>()
                {
                    new ints() {intersection = 2, semType = 2},
                    new ints() {intersection = 1, semType = 2},
                };
                pth = new SelectedPath(Animations5, dict5, posT, posR);
            }

            selectedThings.Add(new selectedItem()
            {
                carType = AvailableCarTypes.SelectedItem.ToString(),
                path = AvailablePathsListBox.SelectedItem.ToString(),
                isBad = isBadCheckBox.IsChecked.Value,
                speed = Int32.Parse(speedTextBox.Text)
            });


            carsList.Add(new Car(pth.intersectionAndSemType,pth.Anims,imgSource,"Vehicle" + name++,45,25,pth.posFromTop, pth.posFromRight, Int32.Parse(speedTextBox.Text), isBadCheckBox.IsChecked.Value));
     
        }

        // Event ce asigura reinitializarea unui scenariu
        private void clearScenario(object sender, RoutedEventArgs e)
        {
            foreach (var car in carsList)
            {
                car.removeImage();
            }
            carsList.Clear();
            listOfTasks.Clear();
            selectedThings.Clear();
            listOfBadCars.Clear(); // TODO: After the first simulation the bad cars are no longer added to the list of bad cars
        }

        // Event ce asigura pornirea senzorilor de monitorizare a traficului
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

        // Event ce asigura pornirea sistemului de Unda Verde
        private void ActivateGreenWave(object sender, RoutedEventArgs e)
        {
            foreach (var intersection in Intersections)
            {
                intersection._TrafficLights[0].increaseGreenTime();
                intersection._TrafficLights[2].increaseGreenTime();
                // TODO: there are situations in witch all the traffic lights in an intersection are green
            }
        }

        private void DeactivateGreenWave(object sender, RoutedEventArgs e)
        {
            foreach (var intersection in Intersections)
            {
                intersection._TrafficLights[0].decreaseGreenTime();
                intersection._TrafficLights[2].decreaseGreenTime();
            }
        }
    }
}