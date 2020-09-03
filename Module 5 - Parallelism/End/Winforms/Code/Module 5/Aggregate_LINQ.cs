using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winforms.Code.Module_5
{
    public class Aggregate_LINQ
    {
        public void btnStart_Click()
        {
            Console.WriteLine("begin");

            // Sum and Avegate examples
            //var source = Enumerable.Range(1, 1000);
            //var sum = source.AsParallel().Sum();
            //var average = source.AsParallel().Average();

            //Console.WriteLine("The sum is " + sum);
            //Console.WriteLine("The avegate is " + average);

            // Example custom aggregate
            var matrices = Enumerable.Range(1, 500).Select(x => Matrices.InitializeMatrix(1000, 1000)).ToList();

            Console.WriteLine("matrices generated");

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var sequentialSum = matrices.Aggregate(Matrices.AddMatricesSequential);

            var sequentialTime = stopwatch.ElapsedMilliseconds / 1000.0;

            Console.WriteLine("Sequential - duration in seconds: {0}",
                    sequentialTime);

            stopwatch.Restart();

            var parallelSum = matrices.AsParallel().Aggregate(Matrices.AddMatricesSequential);

            var parallelTime = stopwatch.ElapsedMilliseconds / 1000.0;

            Console.WriteLine("Parallel - duration in seconds: {0}",
                    parallelTime);

            Utils.WriteComparison(sequentialTime, parallelTime);

            Console.WriteLine("end");
        }

    }
}
