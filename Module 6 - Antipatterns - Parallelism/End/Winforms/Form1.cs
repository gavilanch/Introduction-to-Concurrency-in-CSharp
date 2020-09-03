using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
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
using Winforms.Code.Module_6;

namespace Winforms
{
    public partial class Form1 : Form
    {
        private CancellationTokenSource cancellationTokenSource;
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            loadingGIF.Visible = true;

            // Video: Unnecessary Parallelism
            //await new Unnecessary_Parallelism().btnStart_Click();

            // Video: Race Condition
            //new Race_Condition().btnStart_Click();

            // Video: Too Much Can Be Bad - Oversaturation
            //new Oversaturation().btnStart_Click();

            // Video: Using a Non-Thread-Safe Class
            //new Thread_Safety().btnStart_Click();

            // Video: Misuse of Locks
            //await new Locks().btnStart_Click();

            loadingGIF.Visible = false;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cancellationTokenSource?.Cancel();
        }
    }
}
