using P9_UndaVerde;
using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace TrafficSimTM
{
    class Car : MainWindow
    {
        private string _imgSource { get; set; }
        private string _name { get; set; }
        private int _width { get; set; }
        private int _height { get; set; }

        public Car(string imgSource = "car.png", string name = "car", int width = 45, int height = 25)
        {
            _imgSource = imgSource;
            _name = name;
            _width = width;
            _height = height;
        }

        public void createImage()
        {
            // TODO -- not working
            Canvas canv = new Canvas();
            BitmapImage carBitmap = new BitmapImage();
            carBitmap.BeginInit();

            carBitmap.UriSource = new Uri(@"pack://application:,,,/Resources/car.png", UriKind.RelativeOrAbsolute);

            carBitmap.EndInit();
            Image carImg = new Image();
            carImg.Source = carBitmap;
            carImg.Width = _width;
            carImg.Height = _height;
            carImg.Name = _name;
            Canvas.SetLeft(carImg, 200);
            Canvas.SetTop(carImg, 200);
           
           // canv.Children.Add(carImg);
           
            mapGrid.Children.Add(carImg);
        }
    }
}
