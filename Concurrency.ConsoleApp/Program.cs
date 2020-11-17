using System;
using System.Threading.Tasks;

namespace Concurrency.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var chapter01 = new Chapter01();
            chapter01.Deadlock();
            Console.WriteLine("After deadlock");
        }
    }
}
