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

             new Point (40, 70 ),
             new Point (30, 90 ),
             new Point (100, 130 ),
             new Point (200, 150 ),

        };
        List<Point> _intersection2 = new List<Point>() {
            new Point (40, 70 ),
            new Point (30, 90 ),
            new Point (100, 130 ),
            new Point (200, 150 ),

        };
        List<Point> _intersection3 = new List<Point>() {
            new Point (40, 70 ),
            new Point (30, 90 ),
            new Point (100, 130 ),
            new Point (200, 150 ),

        };
        List<Point> _intersection4 = new List<Point>() {
            new Point (40, 70 ),
            new Point (30, 90 ),
            new Point (100, 130 ),
            new Point (200, 150 ),

        };
        List<Point> _intersection5 = new List<Point>() {

            new Point (40, 70 ),
            new Point (30, 90 ),
            new Point (100, 130 ),
            new Point (200, 150 ),

        };
        


       // private Stopwatch clk;
        private MainWindow mainWin = Application.Current.Windows[0] as MainWindow;

       
        
        
        public SemaphoreSystem()
        {
            var intersection1 = new Intersction(_intersection1);
            var intersection2 = new Intersction(_intersection2);
            var intersection3 = new Intersction(_intersection3);
            var intersection4 = new Intersction(_intersection4);
            var intersection5 = new Intersction(_intersection5);

        }

        public void StartSystem()
        {
            // ----TODO: syncronization with semaphoreSlim----
            var t = new CancellationTokenSource();
            var ct = t.Token;

            
        }
    }
}
