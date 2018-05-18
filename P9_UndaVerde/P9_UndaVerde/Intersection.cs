using P9_UndaVerde;
using System.Collections.Generic;
using System.Threading;
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
            string[] directions = new[] {"90left", "inverse", "90right", "normal", "90left"};
            int j = 0;
            foreach (var point in coordinates1)
            {
                _TrafficLights.Add(new TrafficLight("sem" + i++, (int)point.X, (int)point.Y,3000,directions[j++]));
            }
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
                _TrafficLights[2]._color = _TrafficLights[0]._color;
                _TrafficLights[3]._color = !_TrafficLights[0]._color;
                tsk.Start(TaskScheduler.FromCurrentSynchronizationContext());
            }            
        } 
    }
}
