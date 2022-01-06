using System;
using System.Threading.Tasks;

namespace Async_Await_Demo
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Task t1 = AsyncMethods.MethodAsync1();
            Console.WriteLine("Wating 1 second in main");
            await t1;


            Task t2 = AsyncMethods.MethodAsync2();
            Console.WriteLine("Waiting 2 seconds in main");
            await t2;

            Task t3 = AsyncMethods.MethodAsync3();
            Console.WriteLine("Waiting 3 seconds in main");
            await t3;
        }
    }
}
