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
    public class TicketsController : SuperController<Ticket>
    {
        public TicketsController(MyContext context ) : base(context) { }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}/{subject}/{message}/{department}/{priority}/{status}/{shopId}")]
        public async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir, string subject, string message, string department, string priority, string status, string shopId)
        {
            var q = _context.Tickets
                .Where(e => subject == "*" ? true : e.Subject.ToLower().Contains(subject.ToLower()))
.Where(e => message == "*" ? true : e.Message.ToLower().Contains(message.ToLower()))
.Where(e => department == "*" ? true : e.Department.ToLower().Contains(department.ToLower()))
.Where(e => priority == "*" ? true : e.Priority.ToLower().Contains(priority.ToLower()))
.Where(e => status == "*" ? true : e.Status.ToLower().Contains(status.ToLower()))
.Where(e => shopId == "*" ? true : e.ShopId.ToLower().Contains(shopId.ToLower()))

                ;

            int count = await q.CountAsync();

            var list = await q.OrderByName<Ticket>(sortBy, sortDir == "desc")
                .Skip(startIndex)
                .Take(pageSize)
                
                .Select(e => new 
{
id = e.Id,
subject = e.Subject,
message = e.Message,
department = e.Department,
priority = e.Priority,
status = e.Status,
unread = e.Unread,
shopId = e.ShopId,

})
                .ToListAsync()
                ;

            return Ok(new { list = list, count = count });
        }


        [HttpGet]
        public override async Task<IActionResult> Get()
        {
            var list = await _context.Tickets.OrderByName<Ticket>("Id").ToListAsync();

            return Ok(list);
        }

        
        [HttpGet("{id}")]
        public override async Task<IActionResult> Get(int id)
        {
            var model = await _context.Tickets.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpPost]
        public override async Task<IActionResult> Add(Ticket model)
        {
            _context.Tickets.Add(model);

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
        public override async Task<IActionResult> Update([FromRoute] int id, [FromBody] Ticket model)
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
            var model = await _context.Tickets.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            _context.Tickets.Remove(model);
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