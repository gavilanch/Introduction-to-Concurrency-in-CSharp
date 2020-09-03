using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winforms.Code.Module_2
{
    public class Task_And_TaskOfT
    {
        private readonly string apiURL;
        private readonly HttpClient httpClient;

        public Task_And_TaskOfT(string apiURL)
        {
            this.apiURL = apiURL;
            httpClient = new HttpClient();
        }

        public async Task btnStart_Click(TextBox txtInput)
        {
            await Wait();
            var name = txtInput.Text;
            var greeting = await GetGreeting(name);
            MessageBox.Show(greeting);
        }

        private async Task Wait()
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
        }

        private async Task<string> GetGreeting(string nombre)
        {
            using (var response = await httpClient.GetAsync($"{apiURL}/greetings/{nombre}"))
            {
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
