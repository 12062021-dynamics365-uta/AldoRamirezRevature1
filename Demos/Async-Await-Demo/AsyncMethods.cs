using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async_Await_Demo
{
    internal static class AsyncMethods
    {
        //don't need properties

        public static async Task MethodAsync1()
        {
            Console.WriteLine("In MethodAsync1");
            Task t1 = Task.Delay(1000);
            Console.WriteLine("Waiting 1 second");
            await t1;
            Console.WriteLine("After waiting 1 second\n");
        }

        public static async Task MethodAsync2()
        {
            Console.WriteLine("In MethodAsync2");
            Task t2 = Task.Delay(2000);
            Console.WriteLine("Waiting 2 seconds");
            await t2;
            Console.WriteLine("After waiting 2 second\n");
        }

        public static async Task MethodAsync3()
        {
            Console.WriteLine("In MethodAsync3");
            Task t3 = Task.Delay(3000);
            Console.WriteLine("Waiting 3 second");
            await t3;
            Console.WriteLine("After waiting 3 second\n");
        }
    }
}
