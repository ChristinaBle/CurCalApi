using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CurCalApi.Models;
using System.Data;
using Microsoft.AspNetCore.Authorization;

namespace CurCalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeRatesController : ControllerBase
    {
        private readonly CurrencyCalculatorContext _context;

        public ExchangeRatesController(CurrencyCalculatorContext context)
        {
            _context = context;
        }

        // GET: api/ExchangeRates
        [HttpGet]
        public ActionResult GetResult(string bc,string tc,decimal? amount)
        
        {         
            var final1 = _context.ExchangeRates.Where(i => i.BasicCurrency == bc && i.TargetCurrency == tc).Select(u => u.ExchangeRate).FirstOrDefault();
            var final2 = _context.ExchangeRates.Where(i => i.BasicCurrency == tc && i.TargetCurrency == bc).Select(u => u.ExchangeRate).FirstOrDefault();
            decimal? result;
            if (final1 != null)
            {
                result = final1 * amount;
            }
            else if (final2 != null)
            {
                result = amount / final2;
            }
            else
            {
                return Content("Please fill out all required fields.");
            }
            return Ok(result);
        }

        /*region get by id
        // GET: api/ExchangeRates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExchangeRates>> GetExchangeRates(int id)
        {
            var exchangeRates = await _context.ExchangeRates.FindAsync(id);

            if (exchangeRates == null)
            {
                return NotFound();
            }

            return exchangeRates;
        }
        */

        // PUT: api/ExchangeRates/5
        [HttpPut("{id}")]
        [Authorize]
        public ActionResult PutExchangeRates(Guid id,[FromBody] ExchangeRates exchangeRates)
        {
            //if (id != exchangeRates.Id)
            //{
            //    return Content("Please give a valid id.");
            //}
            //else
            //{
                var cur = _context.ExchangeRates.FirstOrDefault(s => s.Id.Equals(id));
                cur.BasicCurrency = exchangeRates.BasicCurrency;
                cur.TargetCurrency = exchangeRates.TargetCurrency;
                cur.ExchangeRate = exchangeRates.ExchangeRate;

                _context.SaveChanges();

                return Content("Successfully updated!");
            //}
        }

        // POST: api/ExchangeRates
        [HttpPost]
        [Authorize]
        public IActionResult PostExchangeRates([FromBody]ExchangeRates exchangeRates)
          {
                if (exchangeRates == null)
                    return BadRequest();
            
                if (!ModelState.IsValid)
                    return BadRequest();

                _context.Add(exchangeRates);
                _context.SaveChanges();

                return Created("URI of the created entity", exchangeRates);
          }

        // DELETE: api/ExchangeRates/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<ExchangeRates>> DeleteExchangeRates(int id)
        {
            var exchangeRates = await _context.ExchangeRates.FindAsync(id);
            if (exchangeRates == null)
            {
                return NotFound();
            }
            _context.ExchangeRates.Remove(exchangeRates);
            await _context.SaveChangesAsync();

            return exchangeRates;
        }

        // GET: api/ExchangeRates
        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<ExchangeRates>>> GetExchangeRates()
        {
            var exRates = _context.ExchangeRates.AsQueryable();
            return await exRates.ToListAsync();
        }
    }
}
