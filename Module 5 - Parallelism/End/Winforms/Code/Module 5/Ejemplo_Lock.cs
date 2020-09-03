using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Winforms.Code.Module_5
{
    public class Lock_Example
    {
        public void btnStart_Click()
        {
            Console.WriteLine("begin");

            var increment = 0;
            var sum = 0;

            var mutex = new object();

            Parallel.For(0, 10000, _ =>
            {
                // There's a race condition
                Interlocked.Increment(ref increment);
                Interlocked.Add(ref sum, increment);

                // parallel...

                lock (mutex)
                {
                    increment++;
                    sum += increment;
                }

                // parallel...

            });

            Console.WriteLine("---");

            Console.WriteLine($"Increment value: {increment}");
            Console.WriteLine($"Sum value: {sum}");

            Console.WriteLine("end");
        }

    }
}
