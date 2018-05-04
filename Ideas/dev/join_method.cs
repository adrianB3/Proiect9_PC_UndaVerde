using System;
using System.Threading; // added threading namespace

namespace threadingBasics
{
    class Program
    {
        static void Main(string[] args)
        { 
            Thread threadOne = new Thread(Work1); // create thread
       
            threadOne.Start();

            threadOne.Join(); // forces main thread to wait for threadOne to finish -- threadOne joines the main thread

            Console.WriteLine("Work completed.."); // exec on main thread
        }

        static void Work1()
        {
            for(int i = 0; i < 10; i++)
            {
                Console.WriteLine("Thread one working...");
            }
        }
    }
}
