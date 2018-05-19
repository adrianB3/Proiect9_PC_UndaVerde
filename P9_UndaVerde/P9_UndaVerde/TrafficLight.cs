using P9_UndaVerde;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TrafficSimTM
{
    class TrafficLight
    {
        MainWindow mainWin = Application.Current.Windows[0] as MainWindow;

        public bool _color { get; set; }
        public int _greenWaitTime { get; set; }
        public int _redWaitTime { get; set; }
        public int _delay { get; set; }
        private int _positionFromTop { get; set; }
        private int _positionFromRight { get; set; }
        private string _name { get; set; }
        private string _orientation { get; set; }

        Canvas canv = new Canvas();
        Ellipse redLight = new Ellipse();
        SolidColorBrush colorBrush = new SolidColorBrush();
        
        Ellipse greenLight = new Ellipse();
        SolidColorBrush colorBrush1 = new SolidColorBrush();

        public TrafficLight(string name = "", int positionFromTop = 0, int positionFromRight = 0, int delay = 0,string orientation = "normal", bool color = false, int greenWaitTime = 5, int redWaitTime = 5)
        {
            _color = color;
            _greenWaitTime = greenWaitTime;
            _redWaitTime = redWaitTime;
            _delay = delay;
            _positionFromTop = positionFromTop;
            _positionFromRight = positionFromRight;
            _name = name;
            _orientation = orientation;

            colorBrush.Color = Color.FromArgb(255, 255, 0, 0);
            redLight.Fill = colorBrush;
            redLight.Width = 13;
            redLight.Height = 13;

            colorBrush1.Color = Color.FromArgb(255, 0, 255, 0);
            greenLight.Fill = colorBrush1;
            greenLight.Width = 13;
            greenLight.Height = 13;

            BitmapImage semBitmap = new BitmapImage();
            semBitmap.BeginInit();
            
            if (orientation == "normal")
            {
                semBitmap.UriSource = new Uri(@"pack://application:,,,/Images/semaphore.png", UriKind.RelativeOrAbsolute);
                Canvas.SetRight(redLight, _positionFromRight + 13);
                Canvas.SetTop(redLight, _positionFromTop + 55);
                Canvas.SetRight(greenLight, _positionFromRight + 13);
                Canvas.SetTop(greenLight, _positionFromTop + 80);
            }
            if (orientation == "90left")
            {
                semBitmap.UriSource = new Uri(@"pack://application:,,,/Images/semaphore90l.png", UriKind.RelativeOrAbsolute);
                Canvas.SetRight(redLight, _positionFromRight + 25);
                Canvas.SetTop(redLight, _positionFromTop + 68);
                Canvas.SetRight(greenLight, _positionFromRight);
                Canvas.SetTop(greenLight, _positionFromTop + 68);
            }
            if (orientation == "90right")
            {
                semBitmap.UriSource=new Uri(@"pack://application:,,,/Images/semaphore_90r.png", UriKind.RelativeOrAbsolute);
                Canvas.SetRight(redLight, _positionFromRight);
                Canvas.SetTop(redLight, _positionFromTop + 68);
                Canvas.SetRight(greenLight, _positionFromRight + 25);
                Canvas.SetTop(greenLight, _positionFromTop + 68);
            }
            if (orientation == "inverse")
            {
                semBitmap.UriSource = new Uri(@"pack://application:,,,/Images/semaphore_inverse.png", UriKind.RelativeOrAbsolute);
                Canvas.SetRight(redLight, _positionFromRight + 13);
                Canvas.SetTop(redLight, _positionFromTop + 80);
                Canvas.SetRight(greenLight, _positionFromRight + 13);
                Canvas.SetTop(greenLight, _positionFromTop + 55);
            }
            semBitmap.EndInit();
            Image semImage = new Image
            {
                Source = semBitmap,
                Height = 150,
                Width = 40,
                Name = _name
            };

            Canvas.SetRight(semImage, _positionFromRight);
            Canvas.SetTop(semImage, _positionFromTop);
            canv.Children.Add(semImage);
            mainWin.mapGrid.Children.Add(canv);            
        }

        public void increaseGreenTime()
        {
            _greenWaitTime += 2;
        }
        public void decreaseGreenTime()
        {
            _greenWaitTime -= 2;
        }

        public bool isGreen()
        {
            return _color ? true : false;
        }

        public bool isRed()
        {
            return _color ? false : true;
        }

        public Task LightUp()
        {
            var tsk = new Task(async () =>
            {
                while (true)
                {
                    canv.Children.Remove(redLight);
                    canv.Children.Remove(greenLight);

                    if (isGreen())
                    {
                        canv.Children.Add(greenLight);
                        await Task.Delay(_greenWaitTime*1000);
                        _color = false;
                    }
                    else
                    {                      
                        canv.Children.Add(redLight);
                        await Task.Delay(_redWaitTime*1000);
                        _color = true;
                    }
                }
            });

            return tsk;
        }
    }
}
