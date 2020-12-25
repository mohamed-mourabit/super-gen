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
    public class OrderStatusesController : SuperController<OrderStatus>
    {
        public OrderStatusesController(MyContext context ) : base(context) { }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}/{value}/{sequence}")]
        public async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir, string value, int sequence)
        {
            var q = _context.OrderStatuses
                .Where(e => value == "*" ? true : e.Value.ToLower().Contains(value.ToLower()))
.Where(e => sequence == 0 ? true : e.Sequence == sequence)

                ;

            int count = await q.CountAsync();

            var list = await q.OrderByName<OrderStatus>(sortBy, sortDir == "desc")
                .Skip(startIndex)
                .Take(pageSize)
                
                .Select(e => new 
{
id = e.Id,
value = e.Value,
sequence = e.Sequence,

})
                .ToListAsync()
                ;

            return Ok(new { list = list, count = count });
        }


        [HttpGet]
        public override async Task<IActionResult> Get()
        {
            var list = await _context.OrderStatuses.OrderByName<OrderStatus>("Id").ToListAsync();

            return Ok(list);
        }

        
        [HttpGet("{id}")]
        public override async Task<IActionResult> Get(int id)
        {
            var model = await _context.OrderStatuses.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpPost]
        public override async Task<IActionResult> Add(OrderStatus model)
        {
            _context.OrderStatuses.Add(model);

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
        public override async Task<IActionResult> Update([FromRoute] int id, [FromBody] OrderStatus model)
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
            var model = await _context.OrderStatuses.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            _context.OrderStatuses.Remove(model);
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