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
        MainWindow mainWin = Application.Current.Windows[0] as MainWindow; // referinta la fereastra principala

        public int _positionFromTop { get; set; } // pozitii fata de fereastra
        public int _positionFromRight { get; set; }
        public int speed { get; set; } // viteza curenta citita de senzor
        public int _indexIntersectie { get; set; } 
        public int _indexSem { get; set; }

        public Canvas canv;
        public Rectangle speedBoard; // panoul unde se afiseaza viteza
        public Label speedLabel; 
        SolidColorBrush whiteBrush = new SolidColorBrush();

        // Constructor clasa speedpanel
        public SpeedPanel(int positionFromTop, int positionFromRight, int indexIntersectie, int indexSem)
        {
            whiteBrush.Color = Color.FromArgb(150,255,255,255);
            speed = 0;
            _positionFromTop = positionFromTop;
            _positionFromRight = positionFromRight;
            _indexIntersectie = indexIntersectie;
            _indexSem = indexSem;
            canv = new Canvas();
            speedBoard = new Rectangle()
            {
                Width = 70,
                Height = 30,
                Fill = whiteBrush
            };

            speedLabel = new Label()
            {
                Content = (speed + "pixel/s").ToString(), // afisare viteza in interfata
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
