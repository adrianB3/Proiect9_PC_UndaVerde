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
    class Car
    {
        MainWindow mainWin = Application.Current.Windows[0] as MainWindow;

        private string _imgSource { get; set; }
        private string _name { get; set; }
        private int _width { get; set; }
        private int _height { get; set; }
        private int _positionFromTop { get; set; }
        private int _positionFromRight { get; set; }
        public Image _carImg;
        public float _speed { get; set; } // _speed is a car statistic that represents the rate at which a car travels across a map. One _speed point translates to one hundred distance units traveled per second 
        public List<Animation> _animationsList;
        public Car(
            List<Animation> animationsList, 
            string imgSource = "car.png", 
            string name = "car", 
            int width = 45, 
            int height = 25, 
            int positionFromTop = 0, 
            int positionFromRight = 0, 
            float speed = 1)
        {
            _imgSource = imgSource;
            _name = name;
            _width = width;
            _height = height;
            _positionFromTop = positionFromTop;
            _positionFromRight = positionFromRight;
            _speed = speed;
            _carImg = new Image();

            BitmapImage carBitmap = new BitmapImage();
            carBitmap.BeginInit();
            carBitmap.UriSource = new Uri(@"pack://application:,,,/Images/" + _imgSource, UriKind.RelativeOrAbsolute);
            carBitmap.EndInit();
            _carImg.Source = carBitmap;
            _carImg.Width = _width;
            _carImg.Height = _height;
            _carImg.Name = _name;
            _animationsList = animationsList;
        }

        public void createImage()
        {            
            Task tsk = new Task(() =>
            {
                Canvas canv = new Canvas();
                Canvas.SetRight(_carImg, _positionFromRight);
                Canvas.SetTop(_carImg, _positionFromTop);
                canv.Children.Add(_carImg);
                mainWin.mapGrid.Children.Add(canv);
            });
            tsk.Start(TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
