using P9_UndaVerde;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TrafficSimTM
{
    class Intersection
    {
        private MainWindow mainWin = Application.Current.Windows[0] as MainWindow;
        public List<SemaphoreUI> _semaphores = new List<SemaphoreUI>();
        
        public Intersection(List<Point> coordinates)
        {
            var coordinates1 = coordinates;
            char i = 'a';           
                _semaphores.Add(new SemaphoreUI("sem" + i++, (int)coordinates1[0].X, (int)coordinates1[0].Y,3000,"90left" ));
                _semaphores.Add(new SemaphoreUI("sem" + i++, (int)coordinates1[1].X, (int)coordinates1[1].Y,3000,"inverse" ));
                _semaphores.Add(new SemaphoreUI("sem" + i++, (int)coordinates1[2].X, (int)coordinates1[2].Y,3000,"90right" ));
                _semaphores.Add(new SemaphoreUI("sem" + i++, (int)coordinates1[3].X, (int)coordinates1[3].Y,3000,"normal" ));
        }

        public void StartIntersectionSync()
        {
            /* TODO: Synchronize using a pair of semaphores as the critical resource with a SemaphoreSlim or other mehtod */
            foreach (var semaphore in _semaphores)
            {
                var tsk = new Task(async () =>
                {
                    while (true)
                    {
                        semaphore.LightUp();
                        await Task.Delay(3000);
                        semaphore._color = true;
                        semaphore.LightUp();
                        await Task.Delay(3000);
                        semaphore._color = false;
                    }
                });

                tsk.Start(TaskScheduler.FromCurrentSynchronizationContext());
            }
        }
    }
}
