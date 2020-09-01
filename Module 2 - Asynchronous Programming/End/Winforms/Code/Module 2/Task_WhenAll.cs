using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winforms.Code.Module_2
{
    public class Task_WhenAll
    {
        private readonly string apiURL;
        private readonly HttpClient httpClient;

        public Task_WhenAll(string apiURL)
        {
            this.apiURL = apiURL;
            httpClient = new HttpClient();
        }

        public async Task btnStart_Click()
        {
            var cards = GetCards(250);
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                await ProcessCards(cards);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
            }

            MessageBox.Show($"Operation finalized in {stopwatch.ElapsedMilliseconds / 1000.0} seconds");
        }

        private async Task ProcessCards(List<string> cards)
        {
            var tasks = new List<Task<HttpResponseMessage>>();

            foreach (var card in cards)
            {
                var json = JsonConvert.SerializeObject(card);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var responseTask = httpClient.PostAsync($"{apiURL}/cards", content);
                tasks.Add(responseTask);
            }

            await Task.WhenAll(tasks);
        }

        private List<string> GetCards(int amountOfCards)
        {
            var cards = new List<string>();

            for (int i = 0; i < amountOfCards; i++)
            {
                cards.Add(i.ToString().PadLeft(16, '0'));
            }

            return cards;
        }
    }
}
