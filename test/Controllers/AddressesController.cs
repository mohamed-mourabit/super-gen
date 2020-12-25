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
    public class AddressesController : SuperController<_Address>
    {
        public AddressesController(MyContext context ) : base(context) { }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}/{name}/{gps}/{value}/{zipcode}/{customerId}/{cityId}")]
        public async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir, string name, string gps, string value, string zipcode, int customerId, int cityId)
        {
            var q = _context.Addresses
                .Where(e => name == "*" ? true : e.Name.ToLower().Contains(name.ToLower()))
.Where(e => gps == "*" ? true : e.Gps.ToLower().Contains(gps.ToLower()))
.Where(e => value == "*" ? true : e.Value.ToLower().Contains(value.ToLower()))
.Where(e => zipcode == "*" ? true : e.Zipcode.ToLower().Contains(zipcode.ToLower()))
.Where(e => customerId == 0 ? true : e.CustomerId == customerId)
.Where(e => cityId == 0 ? true : e.CityId == cityId)

                ;

            int count = await q.CountAsync();

            var list = await q.OrderByName<_Address>(sortBy, sortDir == "desc")
                .Skip(startIndex)
                .Take(pageSize)
                
                .Select(e => new 
{
id = e.Id,
name = e.Name,
gps = e.Gps,
value = e.Value,
zipcode = e.Zipcode,
customerId = e.CustomerId,
cityId = e.CityId,

})
                .ToListAsync()
                ;

            return Ok(new { list = list, count = count });
        }


        [HttpGet]
        public override async Task<IActionResult> Get()
        {
            var list = await _context.Addresses.OrderByName<_Address>("Id").ToListAsync();

            return Ok(list);
        }

        
        [HttpGet("{id}")]
        public override async Task<IActionResult> Get(int id)
        {
            var model = await _context.Addresses.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpPost]
        public override async Task<IActionResult> Add(_Address model)
        {
            _context.Addresses.Add(model);

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
        public override async Task<IActionResult> Update([FromRoute] int id, [FromBody] _Address model)
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
            var model = await _context.Addresses.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            _context.Addresses.Remove(model);
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