using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winforms.Code.Module_2
{
    public class Response_Of_Task_WhenAll
    {
        private readonly string apiURL;
        private readonly HttpClient httpClient;

        public Response_Of_Task_WhenAll(string apiURL)
        {
            this.apiURL = apiURL;
            httpClient = new HttpClient();
        }

        public async Task btnStart_Click()
        {
            var cards = await GetCards(2500);
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
            using var semaphore = new SemaphoreSlim(1000);

            var tasks = new List<Task<HttpResponseMessage>>();

            tasks = cards.Select(async card =>
            {
                var json = JsonConvert.SerializeObject(card);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                await semaphore.WaitAsync();
                try
                {
                    return await httpClient.PostAsync($"{apiURL}/cards", content);
                }
                finally
                {
                    semaphore.Release();
                }
            }).ToList();

            var responses = await Task.WhenAll(tasks);

            var rejectedCards = new List<string>();

            foreach (var response in responses)
            {
                var content = await response.Content.ReadAsStringAsync();
                var responseCard = JsonConvert
                    .DeserializeObject<CardResponse>(content);
                if (!responseCard.Approved)
                {
                    rejectedCards.Add(responseCard.Card);
                }
            }

            foreach (var card in rejectedCards)
            {
                Console.WriteLine(card);
            }
        }

        private async Task<List<string>> GetCards(int amountOfCards)
        {
            return await Task.Run(() =>
            {
                var cards = new List<string>();

                for (int i = 0; i < amountOfCards; i++)
                {
                    cards.Add(i.ToString().PadLeft(16, '0'));
                }

                return cards;
            });

        }

    }
}
