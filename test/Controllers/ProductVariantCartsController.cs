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
    public class ProductVariantCartsController : SuperController<ProductVariantCart>
    {
        public ProductVariantCartsController(MyContext context ) : base(context) { }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}/{logoUrl}/{quantity}/{selectedAttributItems}/{selectedVariants}/{discount}/{product}/{cartId}")]
        public async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir, string logoUrl, int quantity, string selectedAttributItems, string selectedVariants, string discount, string product, int cartId)
        {
            var q = _context.ProductVariantCarts
                .Where(e => logoUrl == "*" ? true : e.LogoUrl.ToLower().Contains(logoUrl.ToLower()))
.Where(e => quantity == 0 ? true : e.Quantity == quantity)
.Where(e => selectedAttributItems == "*" ? true : e.SelectedAttributItems.ToLower().Contains(selectedAttributItems.ToLower()))
.Where(e => selectedVariants == "*" ? true : e.SelectedVariants.ToLower().Contains(selectedVariants.ToLower()))
.Where(e => discount == "*" ? true : e.Discount.ToLower().Contains(discount.ToLower()))
.Where(e => product == "*" ? true : e.Product.ToLower().Contains(product.ToLower()))
.Where(e => cartId == 0 ? true : e.CartId == cartId)

                ;

            int count = await q.CountAsync();

            var list = await q.OrderByName<ProductVariantCart>(sortBy, sortDir == "desc")
                .Skip(startIndex)
                .Take(pageSize)
                
                .Select(e => new 
{
id = e.Id,
logoUrl = e.LogoUrl,
quantity = e.Quantity,
selectedAttributItems = e.SelectedAttributItems,
selectedVariants = e.SelectedVariants,
discount = e.Discount,
product = e.Product,
cartId = e.CartId,

})
                .ToListAsync()
                ;

            return Ok(new { list = list, count = count });
        }


        [HttpGet]
        public override async Task<IActionResult> Get()
        {
            var list = await _context.ProductVariantCarts.OrderByName<ProductVariantCart>("Id").ToListAsync();

            return Ok(list);
        }

        
        [HttpGet("{id}")]
        public override async Task<IActionResult> Get(int id)
        {
            var model = await _context.ProductVariantCarts.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpPost]
        public override async Task<IActionResult> Add(ProductVariantCart model)
        {
            _context.ProductVariantCarts.Add(model);

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
        public override async Task<IActionResult> Update([FromRoute] int id, [FromBody] ProductVariantCart model)
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
            var model = await _context.ProductVariantCarts.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            _context.ProductVariantCarts.Remove(model);
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