using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace staticServer
{
    class Program
    {
        static void Main(string[] args)
        {
            const int readerTasks = 5;
            const int writerTasks = 5;

            const int writeIterations = 50;

            const int readIterations = 100;

            Task[] tasks = new Task[readerTasks + writerTasks];

            for (int i = 0; i < readerTasks; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    for (int j = 0; j < readIterations; j++)
                    {
                        int currentCount = Server.GetCount();
                        Console.WriteLine($"Чтение: {currentCount}");
                        Thread.Sleep(10);
                    }
                });
            }

            for (int i = 0; i < writerTasks; i++)
            {
                tasks[readerTasks + i] = Task.Run(() =>
                {
                    for (int j = 0; j < writeIterations; j++)
                    {
                        Server.AddToCount(1);
                        Console.WriteLine("Запись: +1");
                        // Имитация задержки
                        Thread.Sleep(20);
                    }
                });
            }

            Task.WaitAll();

            int expected = writerTasks * writeIterations;
            int finalCount = Server.GetCount();
            Console.WriteLine($"\nИтоговое значение: {finalCount} (ожидалось {expected})");
        }
    }
}
