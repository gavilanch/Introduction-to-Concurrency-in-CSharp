using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Winforms.Code.Module_5;

namespace Winforms
{
    public partial class Form1 : Form
    {
        private string apiURL;
        private HttpClient httpClient;
        private CancellationTokenSource cancellationTokenSource;
        public Form1()
        {
            InitializeComponent();
            apiURL = "https://localhost:44313";
            httpClient = new HttpClient();
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            loadingGIF.Visible = true;

            //Video: Simultaneous Tasks - Task.WhenAll
            await new TaskWhenAll().btnStart_Click();

            //Video: Understanding Parallel.For
            new Intro_Parallel_For().btnStart_Click();

            //Video: Parallel.For - Speed Improvement
            await new Parallel_For_Matrices().btnStart_Click();

            //Video: Iterating Collections - Parallel.ForEach
            new Parallel_ForEach().btnStart_Click();

            //Video: Parallelizing Different Methods - Parallel.Invoke
            new Parallel_Invoke().btnStart_Click();

            //Video: Cancelling Parallel Operations and Maximum Degree of Parallelism
            cancellationTokenSource = new CancellationTokenSource();
            await new Max_Degree_Parallelism(cancellationTokenSource).btnStart_Click();
            cancellationTokenSource = null;

            //Video: Updating a Variable Atomically - Interlocked
            new Interlocked_Example().btnStart_Click();

            //Video: One Thread at a Time - Locks
            new Lock_Example().btnStart_Click();

            //Video: Introduction to PLINQ
            cancellationTokenSource = new CancellationTokenSource();
            new Intro_LINQ(cancellationTokenSource).btnStart_Click();
            cancellationTokenSource = null;

            //Video: Doing Aggregates in PLINQ
            new Aggregate_LINQ().btnStart_Click();

            //Video: Processing as Soon as it is Ready - ForAll
            new LINQ_ForAll().btnStart_Click();

            loadingGIF.Visible = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cancellationTokenSource?.Cancel();
        }
    }
}
