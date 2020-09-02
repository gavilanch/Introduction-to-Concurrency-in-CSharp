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
    public class Reporting_Progress_Intervals
    {
        private readonly ProgressBar pgCards;
        private readonly string apiURL;
        private readonly HttpClient httpClient;

        public Reporting_Progress_Intervals(ProgressBar pgCards, string apiURL)
        {
            this.pgCards = pgCards;
            this.apiURL = apiURL;
            httpClient = new HttpClient();
        }

        public async Task btnStart_Click()
        {
            pgCards.Visible = true;
            var reportProgress = new Progress<int>(ReportCardProgress);

            var cards = await GetCards(20);
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                await ProcessCards(cards, reportProgress);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
            }

            MessageBox.Show($"Operation finalized in {stopwatch.ElapsedMilliseconds / 1000.0} seconds");

            pgCards.Value = 0;
            pgCards.Visible = false;
        }

        private void ReportCardProgress(int percentage)
        {
            pgCards.Value = percentage;
        }

        private async Task ProcessCards(List<string> cards, IProgress<int> progress = null)
        {
            using var semaphore = new SemaphoreSlim(2);

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

            var responsesTasks = Task.WhenAll(tasks);

            if (progress != null)
            {
                while (await Task.WhenAny(responsesTasks, Task.Delay(3000)) != responsesTasks)
                {
                    var completedTasks = tasks.Where(x => x.IsCompleted).Count();
                    var percentage = (double)completedTasks / cards.Count;
                    percentage = percentage * 100;
                    var porcentajeInt = (int)Math.Round(percentage, 0);
                    progress.Report(porcentajeInt);
                }
            }

            var responses = await responsesTasks;

            var rejectedCards = new List<string>();

            foreach (var response in responses)
            {
                var content = await response.Content.ReadAsStringAsync();
                var cardResponse = JsonConvert
                    .DeserializeObject<CardResponse>(content);
                if (!cardResponse.Approved)
                {
                    rejectedCards.Add(cardResponse.Card);
                }
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
