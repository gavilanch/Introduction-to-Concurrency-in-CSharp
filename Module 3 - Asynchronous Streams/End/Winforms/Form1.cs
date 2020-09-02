using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Winforms.Code.Module_3;

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

            // Video: Reviewing IEnumerable and yield
            //new Review_IEnumerable().btnStart_Click();

            // Video: Asynchronous Streams
            //await new Asynchronous_Streams().btnIniciar_Click();

            // Video: Cancelling Asynchronous Streams
            //cancellationTokenSource = new CancellationTokenSource();
            //await new Cancelling_Asynchronous_Stream(cancellationTokenSource).btnStart_Click();

            // Video: Cancelling Through IAsyncEnumerable - EnumeratorCancellation
            //cancellationTokenSource = new CancellationTokenSource();
            //await new Cancelling_Through_IAsyncEnumerable(cancellationTokenSource).btnStart_Click();

            cancellationTokenSource = null;
            loadingGIF.Visible = false;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cancellationTokenSource?.Cancel();
        }
    }
}
