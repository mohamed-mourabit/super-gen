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
    public class StoresController : SuperController<Store>
    {
        public StoresController(MyContext context ) : base(context) { }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}/{name}/{phone}/{email}/{address}/{gps}/{shopId}/{cityId}")]
        public async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir, string name, string phone, string email, string address, string gps, int shopId, int cityId)
        {
            var q = _context.Stores
                .Where(e => name == "*" ? true : e.Name.ToLower().Contains(name.ToLower()))
.Where(e => phone == "*" ? true : e.Phone.ToLower().Contains(phone.ToLower()))
.Where(e => email == "*" ? true : e.Email.ToLower().Contains(email.ToLower()))
.Where(e => address == "*" ? true : e.Address.ToLower().Contains(address.ToLower()))
.Where(e => gps == "*" ? true : e.Gps.ToLower().Contains(gps.ToLower()))
.Where(e => shopId == 0 ? true : e.ShopId == shopId)
.Where(e => cityId == 0 ? true : e.CityId == cityId)

                ;

            int count = await q.CountAsync();

            var list = await q.OrderByName<Store>(sortBy, sortDir == "desc")
                .Skip(startIndex)
                .Take(pageSize)
                
                .Select(e => new 
{
id = e.Id,
name = e.Name,
phone = e.Phone,
email = e.Email,
address = e.Address,
gps = e.Gps,
shopId = e.ShopId,
cityId = e.CityId,

})
                .ToListAsync()
                ;

            return Ok(new { list = list, count = count });
        }


        [HttpGet]
        public override async Task<IActionResult> Get()
        {
            var list = await _context.Stores.OrderByName<Store>("Id").ToListAsync();

            return Ok(list);
        }

        
        [HttpGet("{id}")]
        public override async Task<IActionResult> Get(int id)
        {
            var model = await _context.Stores.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpPost]
        public override async Task<IActionResult> Add(Store model)
        {
            _context.Stores.Add(model);

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
        public override async Task<IActionResult> Update([FromRoute] int id, [FromBody] Store model)
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
            var model = await _context.Stores.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            _context.Stores.Remove(model);
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