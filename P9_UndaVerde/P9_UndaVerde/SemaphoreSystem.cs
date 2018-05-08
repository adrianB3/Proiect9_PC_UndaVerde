using P9_UndaVerde;
using System.Collections.Generic;
using System.Windows;

namespace TrafficSimTM
{
    class SemaphoreSystem
    {
        MainWindow mainWin = Application.Current.Windows[0] as MainWindow;
        private List<Point> _coordinates;
        private List<SemaphoreUI> _semaphores;
        public SemaphoreSystem(List<Point> coordinates)
        {
            char i = 'a';
            _semaphores = new List<SemaphoreUI>();
            _coordinates = coordinates;
            foreach (var item in _coordinates)
            {
                _semaphores.Add(new SemaphoreUI(i++.ToString(), (int)item.X, (int)item.Y));
            }
        }


    }
}
