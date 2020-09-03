using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winforms.Code.Module_5;

namespace Winforms.Code.Module_6
{
    public class Oversaturation
    {
        public void btnStart_Click()
        {
            Console.WriteLine("start");

            var stopwatch = new Stopwatch();

            stopwatch.Start();
            var matrices = Enumerable.Range(1, 1000).Select(x => Matrices.InitializeMatrix(750, 750)).ToList();

            var parallelTime = stopwatch.ElapsedMilliseconds / 1000.0;
            Console.WriteLine($"Paralellism - took {parallelTime} seconds");

            stopwatch.Restart();
            var matrices2 = Enumerable.Range(1, 1000).Select(x => Matrices.InitializeMatrixSaturated(750, 750)).ToList();

            var overSaturationTime = stopwatch.ElapsedMilliseconds / 1000.0;

            Console.WriteLine($"Over Saturation - took {overSaturationTime} seconds");
            Utils.WriteComparison(parallelTime, overSaturationTime);

            Console.WriteLine("end");
        }
    }
}
