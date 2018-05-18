using P9_UndaVerde;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TrafficSimTM
{
    class Sensor
    {

        MainWindow mainWin = Application.Current.Windows[0] as MainWindow;

        public int _numberOfCars;

        public int _indexIntersectie;
        public int _indexSemafor;

        public bool _isCrowded()
        {
            if (_numberOfCars > 5)
                return true;
            else
                return false;
        }
        public Sensor(int indexIntersectie, int indexSemafor)
        {
            _indexIntersectie = indexIntersectie;
            _indexSemafor = indexSemafor;
            _numberOfCars = 0;
        }
        public void _Signal()
        {
            _numberOfCars++;
        }    
        public void _Reset()
        {
            _numberOfCars = 0;
        }


    }
}
