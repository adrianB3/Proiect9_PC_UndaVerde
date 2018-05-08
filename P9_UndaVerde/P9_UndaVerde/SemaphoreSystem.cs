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
            _semaphores = new List<SemaphoreUI>();
            _coordinates = coordinates;
            foreach (var item in _coordinates)
            {
                _semaphores.Add(new SemaphoreUI("sem" + i++.ToString(), (int)item.X, (int)item.Y));
            }
        }

        public void StartSystem()
        {
            List<Task> semTsk = new List<Task>();
            foreach (var item in _semaphores)
            {
                semTsk.Add(new Task(async () =>
            {
                item.lightUp();
                await Task.Delay(3000);
                item._color = true;
                item.lightUp();
                await Task.Delay(3000);
                item._color = false;
                item.lightUp();
            }));
            }

                
            foreach (var item in semTsk)
            {
                item.Start(TaskScheduler.FromCurrentSynchronizationContext());
            }
                        
        }
    }
}
