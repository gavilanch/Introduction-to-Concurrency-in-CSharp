using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebAPI.Helpers;

namespace WebAPI.Controllers
{
    [Route("api/greetings")]
    [ApiController]
    public class GreetingsController : ControllerBase
    {
        [HttpGet("{name}")]
        public ActionResult<string> GetGreeting(string name)
        {
            return $"Hello, {name}!";
        }

        [HttpGet("delay/{name}")]
        public async Task<ActionResult<string>> GetGreetingAsync(string name)
        {
            var waitingTime = RandomGen.NextDouble() * 10 + 1;
            await Task.Delay(TimeSpan.FromSeconds(waitingTime));

            // The try catch does not avoid the crashing of the app
            //try
            //{
            //    AsyncVoidMethod();
            //}
            //catch (Exception ex)
            //{

            //}

            // Don't!
            //AsyncTaskMethod();
            SyncVoidMethod();

            return $"Hello, {name}!";
        }

        // Antipattern: do not use async void
        private async void AsyncVoidMethod()
        {
            await Task.Delay(1);
            throw new ApplicationException();
        }

        private void SyncVoidMethod()
        {
            throw new ApplicationException();
        }

        private async Task AsyncTaskMethod()
        {
            await Task.Delay(1);
            throw new ApplicationException();
        }

        [HttpGet("goodbye/{name}")]
        public async Task<ActionResult<string>> ObtenerAdiosConDelay(string name)
        {
            var waitingTime = RandomGen.NextDouble() * 10 + 1;
            await Task.Delay((int)waitingTime * 1000);
            return $"Bye, {name}!";
        }
    }
}
