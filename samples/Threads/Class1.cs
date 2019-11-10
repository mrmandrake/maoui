using System;
using System.Threading;
using System.Runtime.InteropServices;

namespace Threads
{
    public class Program
    {
        public static void Main()
        {
            var t = new Thread(delegate () {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("In thread.");
                    Thread.Sleep(1000);
                }
            });
            t.Start();

            for (int j = 0; j < 10; j++)
            {
                Console.WriteLine("Hello, World!");
                Thread.Sleep(677);
            }

            t.Join();
            Console.WriteLine("bye bye cruel world");
        }
    }
}
