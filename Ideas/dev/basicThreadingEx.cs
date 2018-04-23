using System;
using System.Threading; // added threading namespace

namespace threadingBasics
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread threadOne = new Thread(Work1); // create threads
            Thread threadTwo = new Thread(Work2);

            threadOne.Start(); // start threads
            threadTwo.Start();

        }

        static void Work1()
        {
            for(int i = 0; i < 10; i++)
            {
                Console.WriteLine("Work 1 is called: " + i.ToString());
            }
        }

        static void Work2()
        {
            for(int i = 0; i < 10; i++)
            {
                Console.WriteLine("Work 2 is called: " + i.ToString());
            }
        }
    }
}
