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
        private List<Point> _coordinates;

        List<SemaphoreUI> _semaphores = new List<SemaphoreUI>();
        
        public Intersection(List<Point> coordinates)
        {
            _coordinates = coordinates;
            char i = 'a';
           
            
                _semaphores.Add(new SemaphoreUI("sem" + i++, (int)_coordinates[0].X, (int)_coordinates[0].Y,3000,"90left" ));
                _semaphores.Add(new SemaphoreUI("sem" + i++, (int)_coordinates[1].X, (int)_coordinates[1].Y,3000,"inverse" ));
                _semaphores.Add(new SemaphoreUI("sem" + i++, (int)_coordinates[2].X, (int)_coordinates[2].Y,3000,"90right" ));
                _semaphores.Add(new SemaphoreUI("sem" + i++, (int)_coordinates[3].X, (int)_coordinates[3].Y,3000,"normal" ));

            
      
        }


    }
}
