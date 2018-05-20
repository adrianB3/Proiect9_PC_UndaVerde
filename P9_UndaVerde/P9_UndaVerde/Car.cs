using P9_UndaVerde;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace TrafficSimTM
{
    public struct ints
    {
        public int intersection;
        public int semType;
    }
    public class Car
    {
        MainWindow mainWin = Application.Current.Windows[0] as MainWindow; // referinta catre fereastra principala

        private string _imgSource { get; set; } // sursa imaginii cu masinuta
        public string _name { get; set; } // nume masinuta
        private int _width { get; set; } // dimensiuni
        private int _height { get; set; }
        private int _positionFromTop { get; set; } // positii in cadrul ferestri
        private int _positionFromRight { get; set; }
        public Image _carImg;
        public float _speed { get; set; } // _speed is a car statistic that represents the rate at which a car travels across a map. One _speed point translates to one hundred distance units traveled per second 
        public List<Animation> _animationsList; // lista cu animatiile ce vor fi executate de masinuta
        public List<ints> intSem; // semafoarele prin care va trece masinuta in timpul animatiilor
        private Canvas canv;
        public bool _isABadCar; // var ce va retine daca masinuta incalca legea

        // Constuctor clasa masina
        public Car(
            List<ints> intS, // lista intersectii si semafoare 
            List<Animation> animationsList, // secventa animatii
            string imgSource = "car.png", 
            string name = "car", 
            int width = 45, 
            int height = 25, 
            int positionFromTop = 0, 
            int positionFromRight = 0, 
            float speed = 50,
            bool isBad = false
            )
        {
            _imgSource = imgSource;
            _name = name;
            _width = width;
            _height = height;
            _positionFromTop = positionFromTop;
            _positionFromRight = positionFromRight;
            _speed = speed;
            _carImg = new Image();
            intSem = intS;
            _isABadCar = isBad;
            BitmapImage carBitmap = new BitmapImage();
            carBitmap.BeginInit();
            carBitmap.UriSource = new Uri(@"pack://application:,,,/Images/" + _imgSource, UriKind.RelativeOrAbsolute);
            carBitmap.EndInit();
            _carImg.Source = carBitmap;
            _carImg.Width = _width;
            _carImg.Height = _height;
            _carImg.Name = _name;
            ToolTip tl = new ToolTip
            {
                Content = _name
            };
            _carImg.ToolTip = tl;
            _animationsList = animationsList;
            canv = new Canvas();
        }

        // Functie de adaugare a masinutei in fereastra principala
        public void createImage()
        {      
            // Task ce asigura crearea in paralel a tuturor masinutelor
            Task tsk = new Task(() =>
            {                
                Canvas.SetRight(_carImg, _positionFromRight);
                Canvas.SetTop(_carImg, _positionFromTop);
                canv.Children.Add(_carImg);
                mainWin.mapGrid.Children.Add(canv);
            });
            tsk.Start(TaskScheduler.FromCurrentSynchronizationContext());
        }

        // Functie folosita pentru inlaturarea masinutei din fereastra principala
        public void  removeImage()
        {
            canv.Children.Remove(_carImg);
        }
    }
}
