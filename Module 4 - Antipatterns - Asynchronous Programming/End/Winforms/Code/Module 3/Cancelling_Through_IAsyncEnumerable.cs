using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Winforms.Code.Module_3
{
    public class Cancelling_Through_IAsyncEnumerable
    {
        private CancellationTokenSource cancellationTokenSource;

        public Cancelling_Through_IAsyncEnumerable(CancellationTokenSource cancellationTokenSource)
        {
            this.cancellationTokenSource = cancellationTokenSource;
        }

        public async Task btnStart_Click()
        {
            var namesGenerator = GenerateNames();
            await ProcessNames(namesGenerator);
        }

        private async Task ProcessNames(IAsyncEnumerable<string> namesGenerator)
        {

            cancellationTokenSource = new CancellationTokenSource();

            try
            {
                await foreach (var name in namesGenerator.WithCancellation(cancellationTokenSource.Token))
                {
                    Console.WriteLine(name);
                }
            }
            catch (TaskCanceledException ex)
            {
                Console.WriteLine("operation cancelled");
            }
            finally
            {
                cancellationTokenSource?.Dispose();
            }

        }

        private async IAsyncEnumerable<string> GenerateNames(
            [EnumeratorCancellation] CancellationToken token = default)
        {
            yield return "Felipe";
            await Task.Delay(2000, token);
            yield return "Claudia";
            await Task.Delay(2000, token);
            yield return "Antonio";
        }
    }
}
