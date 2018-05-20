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

        MainWindow mainWin = Application.Current.Windows[0] as MainWindow;
        public string _name { get; set; }
        public int _numberOfCars { get; set; }
        public int _indexIntersectie { get; set; }
        public int _indexSemafor { get; set; }
        Canvas canv = new Canvas();
        Ellipse blueLight = new Ellipse();
        ImageBrush colorBrush = new ImageBrush();
        

        public Sensor(string name,int indexIntersectie, int indexSemafor, int positionFromRight, int positionFromTop)
        {
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
            mainWin.mapGrid.Children.Add(canv);

        }
        public bool _isCrowded()
        {
            return _numberOfCars > 5 ? true : false;
        }
        
        public void _Signal()
        {
            _numberOfCars++;
            
        }

        public void _Reset()
        {
            _numberOfCars = 0;
        }

        public void startSensor()
        {
            blueLight.Fill = colorBrush;
        }

        public void stopSensor()
        {
            blueLight.Fill = new SolidColorBrush(Color.FromArgb(100, 255, 255, 255));
        }
    }
}
