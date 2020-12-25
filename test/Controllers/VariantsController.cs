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
    public class VariantsController : SuperController<Variant>
    {
        public VariantsController(MyContext context ) : base(context) { }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}/{name}/{addedPrice}/{price}/{stock}/{ddOrder}/{items}/{attributId}")]
        public async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir, string name, int addedPrice, int price, int stock, int ddOrder, string items, int attributId)
        {
            var q = _context.Variants
                .Where(e => name == "*" ? true : e.Name.ToLower().Contains(name.ToLower()))
.Where(e => addedPrice == 0 ? true : e.AddedPrice == addedPrice)
.Where(e => price == 0 ? true : e.Price == price)
.Where(e => stock == 0 ? true : e.Stock == stock)
.Where(e => ddOrder == 0 ? true : e.DdOrder == ddOrder)
.Where(e => items == "*" ? true : e.Items.ToLower().Contains(items.ToLower()))
.Where(e => attributId == 0 ? true : e.AttributId == attributId)

                ;

            int count = await q.CountAsync();

            var list = await q.OrderByName<Variant>(sortBy, sortDir == "desc")
                .Skip(startIndex)
                .Take(pageSize)
                
                .Select(e => new 
{
id = e.Id,
name = e.Name,
description = e.Description,
addedPrice = e.AddedPrice,
price = e.Price,
available = e.Available,
stock = e.Stock,
ddOrder = e.DdOrder,
items = e.Items,
attributId = e.AttributId,

})
                .ToListAsync()
                ;

            return Ok(new { list = list, count = count });
        }


        [HttpGet]
        public override async Task<IActionResult> Get()
        {
            var list = await _context.Variants.OrderByName<Variant>("Id").ToListAsync();

            return Ok(list);
        }

        
        [HttpGet("{id}")]
        public override async Task<IActionResult> Get(int id)
        {
            var model = await _context.Variants.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpPost]
        public override async Task<IActionResult> Add(Variant model)
        {
            _context.Variants.Add(model);

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
        public override async Task<IActionResult> Update([FromRoute] int id, [FromBody] Variant model)
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
            var model = await _context.Variants.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            _context.Variants.Remove(model);
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