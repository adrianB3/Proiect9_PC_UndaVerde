using System;
using System.Threading;
using System.Threading.Tasks;

namespace tpl
{
    class Program
    {
        static void Main(string[] args)
        {
            var arr = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var task1 = new Task(() => DoWork(1, 1000));
            task1.Start();
            var task2 = new Task(() => DoWork(2, 3000));
            task2.Start();
            var task3 = new Task(() => DoWork(3, 500));
            task3.Start();
            Task.Factory.StartNew(() => DoWork(4, 5000))
                .ContinueWith((prev) => DoOtherWork(1, 3000))
                .ContinueWith((prev) => {
                    Parallel.For(0, arr.Length, i =>
                    {
                        Console.WriteLine("i = {0}",i);
                        Thread.Sleep(500);
                    });
                });

            Console.WriteLine("Main thread");

            Console.ReadKey();
        }

        private static void DoWork(int id, int sleep)
        {
            Console.WriteLine("Task {0} started!", id);
            Thread.Sleep(sleep);
            Console.WriteLine("Task {0} ended!", id);
        }

        private static void DoOtherWork(int id, int sleep)
        {
            Console.WriteLine("Task of other {0} started!", id);
            Thread.Sleep(sleep);
            Console.WriteLine("Task of other {0} ended!", id);
        }
    }
}
