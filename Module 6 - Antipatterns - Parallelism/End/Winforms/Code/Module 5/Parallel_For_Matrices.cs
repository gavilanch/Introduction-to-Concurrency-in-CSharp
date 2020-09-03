using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winforms.Code.Module_5
{
    public class Parallel_For_Matrices
    {
        public async Task btnStart_Click()
        {
            var colsMatrixA = 1100;
            var rows = 1000;
            var colsMatrixB = 1750;

            var matrixA = Matrices.InitializeMatrix(rows, colsMatrixA);
            var matrixB = Matrices.InitializeMatrix(colsMatrixA, colsMatrixB);
            var result = new double[rows, colsMatrixB];

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            await Task.Run(() => Matrices.MultiplyMatricesSequential(matrixA, matrixB, result));

            var sequentialTime = stopwatch.ElapsedMilliseconds / 1000.0;

            Console.WriteLine("Sequential - duration in seconds: {0}",
                    sequentialTime);

            result = new double[rows, colsMatrixB];

            stopwatch.Restart();

            await Task.Run(() => Matrices.MultiplyMatricesParallel(matrixA, matrixB, result));

            var parallelTime = stopwatch.ElapsedMilliseconds / 1000.0;

            Console.WriteLine("Parallel - duration in seconds: {0}",
                   parallelTime);

            Utils.WriteComparison(sequentialTime, parallelTime);

            Console.WriteLine("fin");
        }
    }
}
