using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winforms.Code.Module_5
{
    public class LINQ_ForAll
    {
        public void btnStart_Click()
        {
            Console.WriteLine("begin");

            var parallelQuery = Enumerable.Range(1, 10).AsParallel()
                .WithDegreeOfParallelism(2).Select(x => Matrices.InitializeMatrix(100, 100));

            foreach (var matrix in parallelQuery)
            {
                Console.WriteLine(matrix[0, 0]);
            }

            parallelQuery.ForAll(matriz => Console.WriteLine(matriz[0, 0]));

            Console.WriteLine("end");
        }
    }
}
