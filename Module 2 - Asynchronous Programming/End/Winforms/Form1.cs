using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Winforms.Code.Module_2;

namespace Winforms
{
    public partial class Form1 : Form
    {
        private string apiURL;
        private CancellationTokenSource cancellationTokenSource;
        public Form1()
        {
            InitializeComponent();
            // Change the apiURL for your own
            apiURL = "https://localhost:44313";
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            loadingGIF.Visible = true;

            // Video: Non-Freezing UI
            //new UI_Not_Freezes().SyncVersion();
            //await new UI_Not_Freezes().AsyncVersion();

            // Video: Task and Task That Returns a Value
            //await new Task_And_TaskOfT(apiURL).btnStart_Click(txtInput);

            // Video: Task with Errors
            //await new Task_With_Errors(apiURL).btnStart_Click(txtInput);

            // Video: Executing Multiple Tasks - Task.WhenAll
            //await new Task_WhenAll(apiURL).btnStart_Click();

            // Video: Offloading the Current Thread - Task.Run
            //await new Task_Run(apiURL).btnStart_Click();

            // Video: Limiting the Amount of Concurrent Tasks - SemaphoreSlim
            //await new SemaphoreExample(apiURL).btnStart_Click();

            // Video: Using the Response Task.WhenAll
            //await new Response_Of_Task_WhenAll(apiURL).btnStart_Click();

            // Video: Reporting Progress with IProgress
            //await new Reporting_Progress(pgCards, apiURL).btnStart_Click();

            // Video: Reporting Progress in Intervals - Task.WhenAny
            //await new Reporting_Progress_Intervals(pgCards, apiURL).btnStart_Click();

            // Video: Cancelling Tasks
            //cancellationTokenSource = new CancellationTokenSource();
            //await new Cancelling_Tasks(pgCards, apiURL, cancellationTokenSource).btnStart_Click();

            // Video: Cancelling Loops
            //cancellationTokenSource = new CancellationTokenSource();
            //await new Cancelling_Loops(pgCards, apiURL, cancellationTokenSource).btnStart_Click();

            // Video: Cancelling with a Timeout
            //cancellationTokenSource = new CancellationTokenSource();
            //cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(5));
            //await new Cancelling_Timeout(pgCards, apiURL, cancellationTokenSource).btnStart_Click();

            // Video: Creating Finished Tasks - Task.FromResult and Friends
            //var creating_Finished_Tasks = new Creating_Finished_Tasks();
            //var cards = await creating_Finished_Tasks.GetCardsMock(1);
            //await creating_Finished_Tasks.ProcessCardsMock(cards);
            //await creating_Finished_Tasks.GetCancelledTask();
            //await creating_Finished_Tasks.GetTaskWithError();  // This throws an error is awaited

            // Video: Ignoring the SynchronizationContext - ConfigureAwait
            //CheckForIllegalCrossThreadCalls = true; // Only necessary in WinForms apps
            //await new ConfigureAwait().btnStart_Click(btnCancelar);

            // Video: Retry Pattern
            //await new RetryPattern(apiURL).btnStart_Click();

            // Video: Only-One Pattern
            //cancellationTokenSource = new CancellationTokenSource();
            //await new Only_One_Pattern(apiURL, cancellationTokenSource).btnIniciar_Click();

            // Video: Controlling the Result of a Task - TaskCompletionSource
            //await new TaskCompletionSource_Example().btnStart_Click(txtInput);

            // Video: Cancelling Non-Cancellable Tasks with TaskCompletionSource
            //cancellationTokenSource = new CancellationTokenSource();
            //await new Cancelling_Non_Cancellable_Tasks(cancellationTokenSource).btnStart_Click();

            loadingGIF.Visible = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cancellationTokenSource?.Cancel();
        }
    }
}
