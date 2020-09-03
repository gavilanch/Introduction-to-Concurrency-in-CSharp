using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Winforms.Code.Module_5
{
    public class Intro_LINQ
    {
        private readonly CancellationTokenSource cancellationTokenSource;

        public Intro_LINQ(CancellationTokenSource cancellationTokenSource)
        {
            this.cancellationTokenSource = cancellationTokenSource;
        }

        public void btnStart_Click()
        {
            Console.WriteLine("begin");

            var source = Enumerable.Range(1, 20);

            var evenNumbers = source
                .AsParallel()
                .WithDegreeOfParallelism(2)
                .WithCancellation(cancellationTokenSource.Token)
                .AsOrdered()
                .Where(x => x % 2 == 0)
                .ToList();

            foreach (var number in evenNumbers)
            {
                Console.WriteLine(number);
            }

            Console.WriteLine("fin");
        }
    }
}
