//63010377_63010495_63010569_63010608_63010615_63010749

using System;
using System.Threading;

namespace Ex04
{
    class Program
    {
        private static string x = "";
        private static int exitflag = 0;
        private static int print = 1;
        private static int key = 0;
        private static Semaphore s;
        static void ThReadX()
        {

            while (exitflag == 0)
            {
                s.WaitOne();
                if (print == 0) Console.WriteLine("X = {0}", x);
                print = 1;
                key = 0;
                s.Release();
            }
        }

        static void ThWriteX()
        {
            string ex;
            while (exitflag == 0)
            {
                if (key == 0)
                {
                    Console.Write("Input: ");
                    ex = Console.ReadLine();
                    if (ex == "exit")
                    {
                        s.WaitOne();
                        exitflag = 1;
                        Console.WriteLine("Thread 1 exit");
                        s.Release();
                    }
                    else if (ex != "")
                    {
                        s.WaitOne();
                        x = ex;
                        key = 1;
                        print = 0;
                        s.Release();
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Thread A = new Thread(ThReadX);
            Thread B = new Thread(ThWriteX);
            s = new Semaphore(1, 1);

            A.Start();
            B.Start();
        }
    }
}