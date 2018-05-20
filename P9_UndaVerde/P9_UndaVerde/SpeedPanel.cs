using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Shapes;
using P9_UndaVerde;

namespace TrafficSimTM
{
    class SpeedPanel
    {
        MainWindow mainWin = Application.Current.Windows[0] as MainWindow;

        public int _positionFromTop { get; set; }
        public int _positionFromRight { get; set; }
        public int speed { get; set; }

        public Canvas canv;
        public Rectangle speedBoard;
        public Label speedLabel;
        SolidColorBrush whiteBrush = new SolidColorBrush();
        public SpeedPanel(int positionFromTop, int positionFromRight)
        {
            whiteBrush.Color = Color.FromArgb(150,255,255,255);
            speed = 155;
            _positionFromTop = positionFromTop;
            _positionFromRight = positionFromRight;
            canv = new Canvas();
            speedBoard = new Rectangle()
            {
                Width = 70,
                Height = 30,
                Fill = whiteBrush
            };

            speedLabel = new Label()
            {
                Content = (speed + "pixel/s").ToString(),
            };
            Canvas.SetTop(speedBoard, _positionFromTop);
            Canvas.SetRight(speedBoard, positionFromRight);
            Canvas.SetTop(speedLabel, _positionFromTop);
            Canvas.SetRight(speedLabel, positionFromRight);
            canv.Children.Add(speedBoard);
            canv.Children.Add(speedLabel);
            mainWin.mapGrid.Children.Add(canv);
        }
    }
}
