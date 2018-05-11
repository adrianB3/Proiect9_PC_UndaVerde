using P9_UndaVerde;
using System.Collections.Generic;
using System.Windows;
using System.Diagnostics;
using System.Threading;
using System;
using System.Threading.Tasks;

namespace TrafficSimTM
{
    class SemaphoreSystem
    {
        private Stopwatch clk;
        private MainWindow mainWin = Application.Current.Windows[0] as MainWindow;
        private List<Point> _coordinates;
        public List<SemaphoreUI> _semaphores;
        public SemaphoreSystem(List<Point> coordinates)
        {
            char i = 'a';
            int delay = 1;
            _semaphores = new List<SemaphoreUI>();
            _coordinates = coordinates;
            foreach (var item in _coordinates)
            {
                _semaphores.Add(new SemaphoreUI("sem" + i++, (int)item.X, (int)item.Y,delay++,"90left"));
            }
        }

        public void StartSystem()
        {
            // ----TODO: syncronization with semaphoreSlim----
            var t = new CancellationTokenSource();
            var ct = t.Token;

            foreach (var item in _semaphores)
            {
                item.StartSemaphoreTsk(ct);
            } 
        }
    }
}
