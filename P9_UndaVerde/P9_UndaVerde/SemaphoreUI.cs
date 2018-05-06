using P9_UndaVerde;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace TrafficSimTM
{
    class SemaphoreUI
    {
        MainWindow mainWin = Application.Current.Windows[0] as MainWindow;
        private bool _color { get; set; }
        private int _greenWaitTime { get; set; }
        private int _redWaitTime { get; set; }
        private int _delay { get; set; }
        private int _positionFromTop { get; set; }
        private int _positionFromRight { get; set; }
        private string _name { get; set; }

        public SemaphoreUI(string name = "",bool color = false, int greenWaitTime = 20, int redWaitTime = 20, int delay = 0, int positionFromTop = 0, int positionFromRight = 0)
        {
            _color = color;
            _greenWaitTime = greenWaitTime;
            _redWaitTime = redWaitTime;
            _delay = delay;
            _positionFromTop = positionFromTop;
            _positionFromRight = positionFromRight;
            _name = name;

            Canvas canv = new Canvas();

            BitmapImage semBitmap = new BitmapImage();
            semBitmap.BeginInit();
            semBitmap.UriSource = new Uri(@"pack://application:,,,/Images/semaphore.png", UriKind.RelativeOrAbsolute);
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
    }
}
