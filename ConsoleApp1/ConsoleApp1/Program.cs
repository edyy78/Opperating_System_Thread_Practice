// See https://aka.ms/new-console-template for more information
using System;
using System.Threading;

public class Class1
{
    static void Main(string[] args)
    {
        Thread counterThread = new Thread(new ThreadStart(countUp));
        counterThread.Start();

    }

    static void countUp()
    {
        for(int i = 0; i < 20; i++)
        {
            Console.WriteLine("Thread Work " + i);
            Thread.Sleep(1000);
        }
    }
}
