using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winforms.Code.Module_2
{
    public class Cancelling_Non_Cancellable_Tasks
    {

        private CancellationTokenSource cancellationTokenSource;

        public Cancelling_Non_Cancellable_Tasks(CancellationTokenSource cancellationTokenSource)
        {
            this.cancellationTokenSource = cancellationTokenSource;
        }

        public async Task btnStart_Click()
        {
            try
            {
                var result = await Task.Run(async () =>
                {
                    await Task.Delay(5000);
                    return 7;
                }).WithCancellation(cancellationTokenSource.Token);

                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                cancellationTokenSource.Dispose();
                cancellationTokenSource = null;
            }
        }
    }

    public static class TaskExtensionMethods
    {
        public static async Task<T> WithCancellation<T>(this Task<T> task,
            CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<object>(TaskCreationOptions.RunContinuationsAsynchronously);

            using (cancellationToken.Register(state =>
            {
                ((TaskCompletionSource<object>)state).TrySetResult(null);
            }, tcs))
            {
                var taskResult = await Task.WhenAny(task, tcs.Task);
                if (taskResult == tcs.Task)
                {
                    throw new OperationCanceledException(cancellationToken);
                }

                return await task;
            }

        }
    }
}
