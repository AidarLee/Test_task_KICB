using CardsApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CardsApi.Controllers
{
    [ApiController]
    public class CheckClientsController : ControllerBase
    {
        private readonly Test_taskContext _context;

        public CheckClientsController(Test_taskContext context)
        {
            _context = context;
        }

        [Route("api/[controller]")]
        [HttpPost]
        public async Task<IActionResult> CheckClients(decimal number)
        {
            try
            {
                var client = await (from c in _context.Clients.Include(x => x.CardNavigation)
                                    where c.PhoneNumber == number
                                    select new Client
                                    {
                                        Id = c.Id,
                                        FirstName = c.FirstName,
                                        LastName = c.LastName,
                                        PhoneNumber = c.PhoneNumber,
                                        Card = c.Card,
                                        CardNavigation = new Card { 
                                            Id = c.CardNavigation.Id, 
                                            CardName = c.CardNavigation.CardName,
                                            Currency = c.CardNavigation.Currency,
                                            CurrencyNavigation = new Currency
                                            {
                                                Id = c.CardNavigation.CurrencyNavigation.Id,
                                                CurrencyName = c.CardNavigation.CurrencyNavigation.CurrencyName
                                            }
                                        }
                                    }).FirstOrDefaultAsync();

                if (client != null)
                {
                    return Ok(client);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred: " + ex.Message);
            }
        }

        [Route("api/[controller]/registration")]
        [HttpPost]
        public async Task<IActionResult> RegistrationClients([FromBody] Client clientRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                using (Test_taskContext db = new Test_taskContext())
                {
                    var client = new Client
                    {
                        FirstName = clientRequest.FirstName,
                        LastName = clientRequest.LastName,
                        PhoneNumber = Convert.ToInt64(clientRequest.PhoneNumber),
                        Card = clientRequest.Card
                    };

                    db.Clients.Add(client);
                    await db.SaveChangesAsync();
                }

                return Ok("Success!");
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred: " + ex.Message);
            }
        }


    }
}

