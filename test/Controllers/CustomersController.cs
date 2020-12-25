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
    public class CustomersController : SuperController<Customer>
    {
        public CustomersController(MyContext context ) : base(context) { }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}/{firstname}/{lastname}/{email}/{phone}/{cashback}")]
        public async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir, string firstname, string lastname, string email, string phone, int cashback)
        {
            var q = _context.Customers
                .Where(e => firstname == "*" ? true : e.Firstname.ToLower().Contains(firstname.ToLower()))
.Where(e => lastname == "*" ? true : e.Lastname.ToLower().Contains(lastname.ToLower()))
.Where(e => email == "*" ? true : e.Email.ToLower().Contains(email.ToLower()))
.Where(e => phone == "*" ? true : e.Phone.ToLower().Contains(phone.ToLower()))
.Where(e => cashback == 0 ? true : e.Cashback == cashback)

                ;

            int count = await q.CountAsync();

            var list = await q.OrderByName<Customer>(sortBy, sortDir == "desc")
                .Skip(startIndex)
                .Take(pageSize)
                
                .Select(e => new 
{
id = e.Id,
firstname = e.Firstname,
lastname = e.Lastname,
email = e.Email,
phone = e.Phone,
subscriptionDate = e.SubscriptionDate,
cashback = e.Cashback,

})
                .ToListAsync()
                ;

            return Ok(new { list = list, count = count });
        }


        [HttpGet]
        public override async Task<IActionResult> Get()
        {
            var list = await _context.Customers.OrderByName<Customer>("Id").ToListAsync();

            return Ok(list);
        }

        
        [HttpGet("{id}")]
        public override async Task<IActionResult> Get(int id)
        {
            var model = await _context.Customers.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpPost]
        public override async Task<IActionResult> Add(Customer model)
        {
            _context.Customers.Add(model);

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
        public override async Task<IActionResult> Update([FromRoute] int id, [FromBody] Customer model)
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
            var model = await _context.Customers.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(model);
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