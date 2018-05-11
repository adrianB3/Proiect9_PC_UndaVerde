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

             new Point (40, 100 ),
             new Point (60, 110 ),
             new Point (100, 130 ),
             new Point (200, 150 ),

        };
        List<Point> _intersection2 = new List<Point>() {


        };
        List<Point> _intersection3 = new List<Point>() {


        };
        List<Point> _intersection4 = new List<Point>() {


        };
        List<Point> _intersection5 = new List<Point>() {



        };
        


       // private Stopwatch clk;
        private MainWindow mainWin = Application.Current.Windows[0] as MainWindow;

       
        
        
        public SemaphoreSystem()
        {
            var intersection1 = new Intersction(_intersection1);

        }

        public void StartSystem()
        {
            // ----TODO: syncronization with semaphoreSlim----
            var t = new CancellationTokenSource();
            var ct = t.Token;

            
        }
    }
}
