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
using Winforms.Code.Module_4;

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

            // Video: sync-over-async
            await new sync_over_async().btnStart_Click();

            // Video: Avoid Task.Factory.StartNew
            await new StartNew().btnStart_Click();

            // Video: Highly Dangerous - async void [see GreetingsController class in the WebAPI project]

            loadingGIF.Visible = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
        }
    }
}
