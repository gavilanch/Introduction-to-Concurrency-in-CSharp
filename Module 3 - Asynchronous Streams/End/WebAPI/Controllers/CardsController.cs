using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/cards")]
    public class CardsController: ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> ProcesarCard([FromBody] string card)
        {
            var randomValue = RandomGen.NextDouble();
            var approved = randomValue > 0.1;
            await Task.Delay(1000);
            Console.WriteLine($"Card {card} processed");
            return Ok(new { Card = card, Approved = approved });
        }
    }
}
