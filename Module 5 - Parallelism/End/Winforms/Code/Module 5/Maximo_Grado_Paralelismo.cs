using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Winforms.Code.Module_5
{
    public class Max_Degree_Parallelism
    {
        private CancellationTokenSource cancellationTokenSource;

        public Max_Degree_Parallelism(CancellationTokenSource cancellationTokenSource)
        {
            this.cancellationTokenSource = cancellationTokenSource;
        }

        public async Task btnStart_Click()
        {
            Console.WriteLine("begin");

            for (int i = 1; i <= 13; i++)
            {
                await RealizarPruebaMatrices(i);
            }

            Console.WriteLine("end");
        }

        private async Task RealizarPruebaMatrices(int maxDegreeOfParalellism)
        {
            int colCount = 2508;
            int rowCount = 1300;
            int colCount2 = 1850;
            double[,] m1 = Matrices.InitializeMatrix(rowCount, colCount);
            double[,] m2 = Matrices.InitializeMatrix(colCount, colCount2);
            double[,] result = new double[rowCount, colCount2];

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                await Task.Run(() => {
                    Matrices.MultiplyMatricesParallel(m1, m2, result,
                        cancellationTokenSource.Token, maxDegreeOfParalellism);
                });

                Console.WriteLine($"Max degree: {maxDegreeOfParalellism}; time: {stopwatch.ElapsedMilliseconds / 1000.0}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Operation cancelled");
            }
            finally
            {
                cancellationTokenSource.Dispose();
            }

        }
    }
}
