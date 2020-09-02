using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winforms.Code.Module_2
{
    public class RetryPattern
    {
        private readonly string apiURL;
        private readonly HttpClient httpClient;

        public RetryPattern(string apiURL)
        {
            this.apiURL = apiURL;
            httpClient = new HttpClient();
        }

        public async Task btnStart_Click()
        {
            var retryTimes = 3;
            var waitTime = 500;
            for (int i = 0; i < retryTimes; i++)
            {
                try
                {
                    // operation
                    break;
                }
                catch (Exception ex)
                {
                    // log the exception
                    await Task.Delay(waitTime);
                }
            }

            await Retry(ProcessGreeting);

            try
            {
                var content = await Retry(async () =>
                {
                    using (var response = await httpClient.GetAsync($"{apiURL}/greetings2/Felipe"))
                    {
                        response.EnsureSuccessStatusCode();
                        return await response.Content.ReadAsStringAsync();
                    }
                });

                Console.WriteLine(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine("excepción atrapada");
            }

        }

        private async Task ProcessGreeting()
        {
            using (var response = await httpClient.GetAsync($"{apiURL}/greetings2/Felipe"))
            {
                response.EnsureSuccessStatusCode();
                var contenido = await response.Content.ReadAsStringAsync();
                Console.WriteLine(contenido);
            }
        }

        private async Task<string> ProcessGreetingReturns()
        {
            using (var response = await httpClient.GetAsync($"{apiURL}/greetings2/Felipe"))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        private async Task Retry(Func<Task> f, int retryTimes = 3, int waitTime = 500)
        {
            for (int i = 0; i < retryTimes; i++)
            {
                try
                {
                    await f();
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await Task.Delay(waitTime);
                }
            }
        }

        private async Task<T> Retry<T>(Func<Task<T>> f, int retryTimes = 3, int waitTime = 500)
        {
            for (int i = 0; i < retryTimes - 1; i++)
            {
                try
                {
                    return await f();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await Task.Delay(waitTime);
                }
            }

            return await f();
        }
    }
}
