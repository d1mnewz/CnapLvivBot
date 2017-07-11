using System;
using System.IO;
using CnapLvivBot.Seeding.DocumentDb;
using CnapLvivBot.Seeding.MongoDb;
using static System.Console;

namespace CnapLvivBot.Seeding
{
    public static class Program
    {
        /// <exception cref="IOException">An I/O error occurred. </exception>
        /// <exception cref="OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string. </exception>
        /// <exception cref="ArgumentOutOfRangeException">The number of characters in the next line of characters is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
        /// <exception cref="NotSupportedException">The collection is read-only and the operation attempts to modify the collection. </exception>
        /// <exception cref="AggregateException">The task was canceled. The <see cref="P:System.AggregateException.InnerExceptions" /> collection contains a <see cref="T:System.Threading.Tasks.TaskCanceledException" /> object. -or-An exception was thrown during the execution of the task. The <see cref="P:System.AggregateException.InnerExceptions" /> collection contains information about the exception or exceptions. </exception>
        /// <exception cref="ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.</exception>
        public static void Main()
        {
            WriteLine("Switch function: mongo - use MongoDB at local server, document - use cloud DocumentDb");
            var selection = ReadLine();
            IDriver driver = null;
            switch (selection)
            {
                case "mongo":
                    driver = new MongoDbDriver();
                    break;
                case "document":
                    driver = new DocumentDbDriver();
                    break;
                default:
                    WriteLine("Driver not found");
                    break;
            }
            driver?.RunAsync().Wait();

            WriteLine("Done. press any key to exit");
            ReadLine();
        }
    }
}
