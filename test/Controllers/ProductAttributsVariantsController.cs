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
    public class ProductAttributsVariantsController : SuperController<ProductAttributsVariant>
    {
        public ProductAttributsVariantsController(MyContext context ) : base(context) { }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}/{attributids}/{productId}/{variantId}")]
        public async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir, string attributids, int productId, int variantId)
        {
            var q = _context.ProductAttributsVariants
                .Where(e => attributids == "*" ? true : e.Attributids.ToLower().Contains(attributids.ToLower()))
.Where(e => productId == 0 ? true : e.ProductId == productId)
.Where(e => variantId == 0 ? true : e.VariantId == variantId)

                ;

            int count = await q.CountAsync();

            var list = await q.OrderByName<ProductAttributsVariant>(sortBy, sortDir == "desc")
                .Skip(startIndex)
                .Take(pageSize)
                
                .Select(e => new 
{
id = e.Id,
attributids = e.Attributids,
productId = e.ProductId,
variantId = e.VariantId,

})
                .ToListAsync()
                ;

            return Ok(new { list = list, count = count });
        }


        [HttpGet]
        public override async Task<IActionResult> Get()
        {
            var list = await _context.ProductAttributsVariants.OrderByName<ProductAttributsVariant>("Id").ToListAsync();

            return Ok(list);
        }

        
        [HttpGet("{id}")]
        public override async Task<IActionResult> Get(int id)
        {
            var model = await _context.ProductAttributsVariants.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpPost]
        public override async Task<IActionResult> Add(ProductAttributsVariant model)
        {
            _context.ProductAttributsVariants.Add(model);

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
        public override async Task<IActionResult> Update([FromRoute] int id, [FromBody] ProductAttributsVariant model)
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
            var model = await _context.ProductAttributsVariants.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            _context.ProductAttributsVariants.Remove(model);
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