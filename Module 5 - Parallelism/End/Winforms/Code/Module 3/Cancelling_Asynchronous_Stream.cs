using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winforms.Code.Module_3
{
    class Cancelling_Asynchronous_Stream
    {
        private CancellationTokenSource cancellationTokenSource;

        public Cancelling_Asynchronous_Stream(CancellationTokenSource cancellationTokenSource)
        {
            this.cancellationTokenSource = cancellationTokenSource;
        }

        public async Task btnStart_Click()
        {
            try
            {
                // Option 2: Cancellation Token
                await foreach (var name in GenerateNames(cancellationTokenSource.Token))
                {
                    Console.WriteLine(name);
                    // Option 1: a break
                    // break;
                }
            }
            catch (TaskCanceledException ex)
            {
                Console.WriteLine("operation cancelled");
            }
            finally
            {
                cancellationTokenSource?.Dispose();
                cancellationTokenSource = null;
            }
        }

        private async IAsyncEnumerable<string> GenerateNames(CancellationToken token = default)
        {
            yield return "Felipe";
            await Task.Delay(2000, token);
            yield return "Claudia";
            await Task.Delay(2000, token);
            yield return "Antonio";
        }

    }
}
