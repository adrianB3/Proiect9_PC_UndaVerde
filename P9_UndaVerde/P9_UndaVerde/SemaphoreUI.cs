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
    class SemaphoreUI
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
        Ellipse greenLight = new Ellipse();

        public SemaphoreUI(string name = "", int positionFromTop = 0, int positionFromRight = 0, int delay = 0,string orientation = "normal", bool color = false, int greenWaitTime = 20, int redWaitTime = 20)
        {
            _color = color;
            _greenWaitTime = greenWaitTime;
            _redWaitTime = redWaitTime;
            _delay = delay;
            _positionFromTop = positionFromTop;
            _positionFromRight = positionFromRight;
            _name = name;
            _orientation = orientation;
            
            BitmapImage semBitmap = new BitmapImage();
            semBitmap.BeginInit();
            if(orientation == "normal")
                semBitmap.UriSource = new Uri(@"pack://application:,,,/Images/semaphore.png", UriKind.RelativeOrAbsolute);
            if(orientation == "90left")
                semBitmap.UriSource = new Uri(@"pack://application:,,,/Images/semaphore90l.png", UriKind.RelativeOrAbsolute);
            if(orientation == "90right")
            {
                semBitmap.UriSource=new Uri(@"pack://application:,,,/Images/semaphore_90r.png", UriKind.RelativeOrAbsolute);
            }
            if (orientation == "inverse")
            {
                semBitmap.UriSource = new Uri(@"pack://application:,,,/Images/semaphore_inverse.png", UriKind.RelativeOrAbsolute);
            }
            semBitmap.EndInit();
            Image semImage = new Image();
            semImage.Source = semBitmap;
            semImage.Height = 150;
            semImage.Width = 40;
            semImage.Name = _name;
            Canvas.SetRight(semImage, _positionFromRight);
            Canvas.SetTop(semImage, _positionFromTop);           
            canv.Children.Add(semImage);
                      
            mainWin.mapGrid.Children.Add(canv);
            
        }

        public void increaseGreenTime()
        {
            this._greenWaitTime += 10;
        }

        public bool isGreen()
        {
            return _color ? true : false;
        }

        public bool isRed()
        {
            return _color ? false : true;
        }

        public void lightUp() {

                SolidColorBrush colorBrush = new SolidColorBrush();
                colorBrush.Color = Color.FromArgb(255, 255, 0, 0);
                redLight.Fill = colorBrush;
                redLight.Width = 13;
                redLight.Height = 13;

                Canvas.SetRight(redLight, _positionFromRight + 13);
                Canvas.SetTop(redLight, _positionFromTop + 55);


                SolidColorBrush colorBrush1 = new SolidColorBrush();
                colorBrush1.Color = Color.FromArgb(255, 0, 255, 0);
                greenLight.Fill = colorBrush1;
                greenLight.Width = 13;
                greenLight.Height = 13;

                Canvas.SetRight(greenLight, _positionFromRight + 13);
                Canvas.SetTop(greenLight, _positionFromTop + 80);

                if (_color == false)
                {
                    canv.Children.Remove(redLight);
                    canv.Children.Remove(greenLight);
                    canv.Children.Add(redLight);
                }
                else
                {
                    canv.Children.Remove(greenLight);
                    canv.Children.Remove(redLight);
                    canv.Children.Add(greenLight);
                }        
        }

        public Task StartSemaphoreTsk(CancellationToken ct)
        {
            var tsk = new Task(async () =>
            {
                while (true)
                {
                    lightUp();
                    await Task.Delay(3000);
                    _color = true;
                    await Task.Delay(_delay * 1000);
                    lightUp();
                    await Task.Delay(3000);
                    _color = false;
                }
            },ct);

            return tsk;
        }
    }
}
