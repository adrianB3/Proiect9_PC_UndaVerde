using System;
using System.Threading; // added threading namespace
using System.Diagnostics; // for Stopwatch

namespace threadingBasics
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch st = new Stopwatch(); // executes on main thread
            st.Start(); // starts counting

            Thread justAThread = new Thread(ProcessSleep);

            justAThread.Start();
            justAThread.Join(); // main thread now waits for justAThread to finish

            st.Stop();

            TimeSpan ts = st.Elapsed; // getting the elapsed time as a timespan

            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds); // formatting the timespan

            Console.WriteLine("Total time : " + elapsedTime);
            Console.WriteLine("Work completed!");
        }

        static void ProcessSleep()
        {
            for(int i = 0; i < 2; i++)
            {
                Console.WriteLine("Thread one working...");
                Thread.Sleep(4000);  // justAThread will sleep for 4000ms
            }
        }
    }
}

/* OUTPUT: 
Thread one working...
Thread one working...
Total time : 00:00:08
Work completed!
Press any key to continue . . .
*/

