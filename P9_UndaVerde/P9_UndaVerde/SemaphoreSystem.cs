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

        List<Point> _intersection1 = new List<Point>() {

             new Point (45, 90),
             new Point (35, 230),
             new Point (230, 230 ),
             new Point (235, 90 ),

        };
        List<Point> _intersection2 = new List<Point>() {
            new Point (45, 290 ),
            new Point (33, 430),
            new Point (230, 435 ),
            new Point (235, 300 ),

        };
        List<Point> _intersection3 = new List<Point>() {
            new Point (45, 465 ),
            new Point (35, 630 ),
            new Point (230, 635 ),
            new Point (235, 470 ),

        };
        List<Point> _intersection4 = new List<Point>() {
            new Point (45, 675 ),
            new Point (35, 810 ),
            new Point (230, 815 ),
            new Point (235, 680 ),

        };
        List<Point> _intersection5 = new List<Point>() {

            new Point (45, 985 ),
            new Point (35, 1120 ),
            new Point (230, 1125 ),
            new Point (235, 991 ),

        };
        


       // private Stopwatch clk;
        private MainWindow mainWin = Application.Current.Windows[0] as MainWindow;

       
        
        
        public SemaphoreSystem()
        {
            var intersection1 = new Intersection(_intersection1);
           // var intersection2 = new Intersection(_intersection2);
            var intersection3 = new Intersection(_intersection3);
           // var intersection4 = new Intersection(_intersection4);
            var intersection5 = new Intersection(_intersection5);

        }

        public void StartSystem()
        {
            // ----TODO: syncronization with semaphoreSlim----
            var t = new CancellationTokenSource();
            var ct = t.Token;

            
        }
    }
}
