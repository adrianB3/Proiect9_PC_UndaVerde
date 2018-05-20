
using System.Windows;
using System.Windows.Controls;
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
        public int _indexIntersectie { get; set; } // indexul intersectiei unde se afla senzorul
        public int _indexSem { get; set; }

        public Canvas canv;
        public Rectangle speedBoard; // panoul unde se afiseaza viteza
        public Label speedLabel; 
        SolidColorBrush whiteBrush = new SolidColorBrush(); // culoare panoului

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

            canv.Children.Add(speedBoard); // se adauga panelul la canvas
            canv.Children.Add(speedLabel); // se adauga labelul la canvas
            mainWin.mapGrid.Children.Add(canv); // se adauga canvasul la fereastra principala
        }
    }
}
