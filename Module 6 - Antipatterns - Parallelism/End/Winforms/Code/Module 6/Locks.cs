using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winforms.Code.Module_6
{
    public class Locks
    {
        public async Task btnStart_Click()
        {
            Console.WriteLine("begin");

            var mutexA = new object();
            var mutexB = new object();

            var task1 = Task.Run(() =>
            {
                Parallel.For(1, 100000, i =>
                {
                    lock (mutexA)
                    {
                        lock (mutexB)
                        {
                            var value = i;
                        }
                    }
                });
            });

            var task2 = Task.Run(() =>
            {
                Parallel.For(1, 100000, i =>
                {
                    lock (mutexB)
                    {
                        lock (mutexA)
                        {
                            var value = i;
                        }
                    }
                });
            });

            await Task.WhenAll(task1, task2);

            // We'll never write "end" in the console
            Console.WriteLine("end");
        }
    }
}
