using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Winforms.Code.Module_6
{
    public class Race_Condition
    {
        public void btnStart_Click()
        {
            Console.WriteLine("begin");
            var value = 0;

            // Antipattern: Race condition
            Parallel.For(0, 1000000, numero => value++);

            // Solution: Use a synchronization mechanism
            //Parallel.For(0, 1000000, numero => Interlocked.Increment(ref value));

            Console.WriteLine($"Sum without interlocked: {value}");

            Console.WriteLine("end");
        }
    }
}
