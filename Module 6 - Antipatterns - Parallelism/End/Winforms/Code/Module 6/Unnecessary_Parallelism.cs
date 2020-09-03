using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winforms.Code.Module_6
{
    public class Unnecessary_Parallelism
    {
        public async Task btnStart_Click()
        {
            Console.WriteLine("begin");

            var stopwatch = new Stopwatch();

            var max = int.MaxValue / 3;
            var numbers = Enumerable.Range(0, max);

            stopwatch.Start();

            await Task.Run(() =>
            {
                foreach (var number in numbers)
                {
                    var result = number + number;
                }
            });

            Console.WriteLine("Sequential - duration in seconds: {0}",
                stopwatch.ElapsedMilliseconds / 1000.0);

            stopwatch.Restart();

            await Task.Run(() =>
            {
                Parallel.ForEach(numbers, number => { var result = number + number; });
            });

            Console.WriteLine("Parallel - duration in seconds: {0}",
               stopwatch.ElapsedMilliseconds / 1000.0);

            Console.WriteLine("end");
        }
    }
}
