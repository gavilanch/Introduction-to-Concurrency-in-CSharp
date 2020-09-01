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
            Console.WriteLine($"Thread before the await: {Thread.CurrentThread.ManagedThreadId}");
            await Task.Delay(500);
            Console.WriteLine($"Thread after the await: {Thread.CurrentThread.ManagedThreadId}");

            var waitingTime = RandomGen.NextDouble() * 10 + 1;
            await Task.Delay((int)waitingTime * 1000);
            return $"Hello, {name}!";
        }

        [HttpGet("goodbye/{name}")]
        public async Task<ActionResult<string>> GetGoodbye(string name)
        {
            var waitingTime = RandomGen.NextDouble() * 10 + 1;
            await Task.Delay((int)waitingTime * 1000);
            return $"Bye, {name}!";
        }
    }
}
