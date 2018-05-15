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
        public List<TrafficLight> _TrafficLights = new List<TrafficLight>();
        
        public Intersection(List<Point> coordinates)
        {
            var coordinates1 = coordinates;
            char i = 'a';           
                _TrafficLights.Add(new TrafficLight("sem" + i++, (int)coordinates1[0].X, (int)coordinates1[0].Y,3000,"90left" ));
                _TrafficLights.Add(new TrafficLight("sem" + i++, (int)coordinates1[1].X, (int)coordinates1[1].Y,3000,"inverse" ));
                _TrafficLights.Add(new TrafficLight("sem" + i++, (int)coordinates1[2].X, (int)coordinates1[2].Y,3000,"90right" ));
                _TrafficLights.Add(new TrafficLight("sem" + i++, (int)coordinates1[3].X, (int)coordinates1[3].Y,3000,"normal" ));
        }

        public void StartIntersectionSync()
        {
            var listOfTasks = new List<Task>();
            foreach (var trafficLight in _TrafficLights)
            {
                listOfTasks.Add(trafficLight.LightUp());
            }

            foreach (var tsk in listOfTasks)
            {
                _TrafficLights[0]._color = false;
                _TrafficLights[1]._color = !_TrafficLights[0]._color;
                _TrafficLights[2]._color = !_TrafficLights[0]._color;
                _TrafficLights[3]._color = _TrafficLights[3]._color;
                tsk.Start(TaskScheduler.FromCurrentSynchronizationContext());
            }
            
        
        } 
    }
}
