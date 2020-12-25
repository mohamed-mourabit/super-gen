using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Api.Providers;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OpeningTimesController : SuperController<OpeningTime>
    {
        public OpeningTimesController(MyContext context ) : base(context) { }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}/{day}/{shopId}")]
        public async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir, string day, int shopId)
        {
            var q = _context.OpeningTimes
                .Where(e => day == "*" ? true : e.Day.ToLower().Contains(day.ToLower()))
.Where(e => shopId == 0 ? true : e.ShopId == shopId)

                ;

            int count = await q.CountAsync();

            var list = await q.OrderByName<OpeningTime>(sortBy, sortDir == "desc")
                .Skip(startIndex)
                .Take(pageSize)
                
                .Select(e => new 
{
id = e.Id,
day = e.Day,
startTime = e.StartTime,
endTime = e.EndTime,
shopId = e.ShopId,

})
                .ToListAsync()
                ;

            return Ok(new { list = list, count = count });
        }


        [HttpGet]
        public override async Task<IActionResult> Get()
        {
            var list = await _context.OpeningTimes.OrderByName<OpeningTime>("Id").ToListAsync();

            return Ok(list);
        }

        
        [HttpGet("{id}")]
        public override async Task<IActionResult> Get(int id)
        {
            var model = await _context.OpeningTimes.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpPost]
        public override async Task<IActionResult> Add(OpeningTime model)
        {
            _context.OpeningTimes.Add(model);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return Ok(model);
        }

        
        [HttpPut("{id}")]
        public override async Task<IActionResult> Update([FromRoute] int id, [FromBody] OpeningTime model)
        {
            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            var model = await _context.OpeningTimes.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            _context.OpeningTimes.Remove(model);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return Ok();
        }
    }
}