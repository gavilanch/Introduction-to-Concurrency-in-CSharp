using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winforms.Code.Module_2
{
    public class Task_With_Errors
    {
        private readonly string apiURL;
        private readonly HttpClient httpClient;

        public Task_With_Errors(string apiURL)
        {
            this.apiURL = apiURL;
            httpClient = new HttpClient();
        }

        public async Task btnStart_Click(TextBox txtInput)
        {
            await Wait();
            var name = txtInput.Text;
            try
            {
                var greeting = await GetGreeting(name);
                MessageBox.Show(greeting);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task Wait()
        {
            await Task.Delay(TimeSpan.FromSeconds(0));
        }

        private async Task<string> GetGreeting(string nombre)
        {
            using (var response = await httpClient.GetAsync($"{apiURL}/greetings2/{nombre}"))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
