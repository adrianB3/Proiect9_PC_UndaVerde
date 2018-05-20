using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using P9_UndaVerde;

namespace TrafficSimTM
{
    public class Sensor
    {

        MainWindow mainWin = Application.Current.Windows[0] as MainWindow; // referinta catre fereastra principala
        public string _name { get; set; } // id senzor
        public int _numberOfCars { get; set; } // numar curent de masini citite
        public int _indexIntersectie { get; set; } // retine indexul intersectiei unde se afla senzorul
        public int _indexSemafor { get; set; } 
        public bool _isActivated { get; set; } // variabila ce retine daca un senzor e activat sau nu
        Canvas canv = new Canvas(); // container pentru alte forme
        Ellipse blueLight = new Ellipse(); // imaginea senzorului
        ImageBrush colorBrush = new ImageBrush(); // culoarea senzorului
        
        // Constructor clasa Senzor
        public Sensor(string name,int indexIntersectie, int indexSemafor, int positionFromRight, int positionFromTop)
        {
            _isActivated = false;
            _indexIntersectie = indexIntersectie;
            _indexSemafor = indexSemafor;
            _numberOfCars = 0;
            _name = name;
            colorBrush.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Images/blueLed.png", UriKind.RelativeOrAbsolute));
            blueLight.Fill = new SolidColorBrush(Color.FromArgb(100,255,255,255));
            blueLight.Width = 15;
            blueLight.Height = 15;

            Canvas.SetRight(blueLight, positionFromRight);
            Canvas.SetTop(blueLight, positionFromTop);
            canv.Children.Add(blueLight);
            mainWin.mapGrid.Children.Add(canv); // adaugare canvas la fereastra principala

        }

        // Functie care returneaza daca o intersectie e prea aglomerata
        public bool _isCrowded()
        {
            return _numberOfCars > 5 ? true : false;
        }
        
        // Functie care incrementeaza nr de masini care trec de senzor
        public void _Signal()
        {
            _numberOfCars++;
            
        }

        // Functie care reseteaza senzorul
        public void _Reset()
        {
            _numberOfCars = 0;
        }

        // Functie care porneste un senzor
        public void startSensor()
        {
            blueLight.Fill = colorBrush;
            _isActivated = true;
        }

        // Functie care opreste un senzor
        public void stopSensor()
        {
            blueLight.Fill = new SolidColorBrush(Color.FromArgb(100, 255, 255, 255));
            _isActivated = false;
        }
    }
}
