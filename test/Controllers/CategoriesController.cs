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
    public class CategoriesController : SuperController<Category>
    {
        public CategoriesController(MyContext context ) : base(context) { }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}/{name}/{ddOrder}/{discountId}/{shopId}/{additionalFeeId}")]
        public async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir, string name, int ddOrder, int discountId, int shopId, int additionalFeeId)
        {
            var q = _context.Categories
                .Where(e => name == "*" ? true : e.Name.ToLower().Contains(name.ToLower()))
.Where(e => ddOrder == 0 ? true : e.DdOrder == ddOrder)
.Where(e => discountId == 0 ? true : e.DiscountId == discountId)
.Where(e => shopId == 0 ? true : e.ShopId == shopId)
.Where(e => additionalFeeId == 0 ? true : e.AdditionalFeeId == additionalFeeId)

                ;

            int count = await q.CountAsync();

            var list = await q.OrderByName<Category>(sortBy, sortDir == "desc")
                .Skip(startIndex)
                .Take(pageSize)
                
                .Select(e => new 
{
id = e.Id,
name = e.Name,
variant = e.Variant,
active = e.Active,
ddOrder = e.DdOrder,
brand = e.Brand,
discountId = e.DiscountId,
shopId = e.ShopId,
additionalFeeId = e.AdditionalFeeId,

})
                .ToListAsync()
                ;

            return Ok(new { list = list, count = count });
        }


        [HttpGet]
        public override async Task<IActionResult> Get()
        {
            var list = await _context.Categories.OrderByName<Category>("Id").ToListAsync();

            return Ok(list);
        }

        
        [HttpGet("{id}")]
        public override async Task<IActionResult> Get(int id)
        {
            var model = await _context.Categories.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpPost]
        public override async Task<IActionResult> Add(Category model)
        {
            _context.Categories.Add(model);

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
        public override async Task<IActionResult> Update([FromRoute] int id, [FromBody] Category model)
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
            var model = await _context.Categories.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(model);
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