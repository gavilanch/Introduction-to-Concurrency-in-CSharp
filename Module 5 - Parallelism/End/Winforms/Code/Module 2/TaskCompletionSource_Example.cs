using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winforms.Code.Module_2
{
    public class TaskCompletionSource_Example
    {
        public async Task btnStart_Click(TextBox txtInput)
        {
            var task = EvaluateValue(txtInput.Text);

            Console.WriteLine("begin");
            Console.WriteLine($"Is Completed: {task.IsCompleted}");
            Console.WriteLine($"Is Canceled: {task.IsCanceled}");
            Console.WriteLine($"Is Faulted: {task.IsFaulted}");

            try
            {
                await task;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }

            Console.WriteLine("end");
            Console.WriteLine("");
        }

        public Task EvaluateValue(string value)
        {
            var tcs = new TaskCompletionSource<object>
                (TaskCreationOptions.RunContinuationsAsynchronously);

            if (value == "1")
            {
                tcs.SetResult(null);
            }
            else if (value == "2")
            {
                tcs.SetCanceled();
            }
            else
            {
                tcs.SetException(new ApplicationException($"Invalid value: {value}"));
            }

            return tcs.Task;
        }
    }
}
