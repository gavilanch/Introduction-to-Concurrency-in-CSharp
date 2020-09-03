using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Winforms.Code.Module_5
{
    public class Interlocked_Example
    {
        public void btnStart_Click()
        {
            Console.WriteLine("begin");

            var valueWithoutInterlocked = 0;

            Parallel.For(0, 1000000, _ => valueWithoutInterlocked++);

            Console.WriteLine($"Sum without interlocked: {valueWithoutInterlocked}");

            var valueWithInterlocked = 0;

            Parallel.For(0, 1000000, _ => Interlocked.Increment(ref valueWithInterlocked));

            Console.WriteLine($"Sum with interlocked: {valueWithInterlocked}");

            Console.WriteLine("end");
        }
    }
}
