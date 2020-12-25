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
    public class ProductsController : SuperController<Product>
    {
        public ProductsController(MyContext context ) : base(context) { }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}/{name}/{price}/{stock}/{ddOrder}/{parentProductId}/{slug}/{metaTitle}/{metaDescription}/{saleCount}/{publisher}/{externalId}/{discountPrice}/{brandId}/{discountId}/{shopId}")]
        public async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir, string name, int price, int stock, int ddOrder, int parentProductId, string slug, string metaTitle, string metaDescription, int saleCount, int publisher, int externalId, int discountPrice, int brandId, int discountId, int shopId)
        {
            var q = _context.Products
                .Where(e => name == "*" ? true : e.Name.ToLower().Contains(name.ToLower()))
.Where(e => price == 0 ? true : e.Price == price)
.Where(e => stock == 0 ? true : e.Stock == stock)
.Where(e => ddOrder == 0 ? true : e.DdOrder == ddOrder)
.Where(e => parentProductId == 0 ? true : e.ParentProductId == parentProductId)
.Where(e => slug == "*" ? true : e.Slug.ToLower().Contains(slug.ToLower()))
.Where(e => metaTitle == "*" ? true : e.MetaTitle.ToLower().Contains(metaTitle.ToLower()))
.Where(e => metaDescription == "*" ? true : e.MetaDescription.ToLower().Contains(metaDescription.ToLower()))
.Where(e => saleCount == 0 ? true : e.SaleCount == saleCount)
.Where(e => publisher == 0 ? true : e.Publisher == publisher)
.Where(e => externalId == 0 ? true : e.ExternalId == externalId)
.Where(e => discountPrice == 0 ? true : e.DiscountPrice == discountPrice)
.Where(e => brandId == 0 ? true : e.BrandId == brandId)
.Where(e => discountId == 0 ? true : e.DiscountId == discountId)
.Where(e => shopId == 0 ? true : e.ShopId == shopId)

                ;

            int count = await q.CountAsync();

            var list = await q.OrderByName<Product>(sortBy, sortDir == "desc")
                .Skip(startIndex)
                .Take(pageSize)
                
                .Select(e => new 
{
id = e.Id,
name = e.Name,
description = e.Description,
price = e.Price,
available = e.Available,
stock = e.Stock,
suggestOnly = e.SuggestOnly,
ddOrder = e.DdOrder,
parentProductId = e.ParentProductId,
frontLine = e.FrontLine,
slug = e.Slug,
metaTitle = e.MetaTitle,
metaDescription = e.MetaDescription,
saleCount = e.SaleCount,
creationDate = e.CreationDate,
editDate = e.EditDate,
published = e.Published,
publisher = e.Publisher,
externalId = e.ExternalId,
discountPrice = e.DiscountPrice,
brandId = e.BrandId,
discountId = e.DiscountId,
shopId = e.ShopId,

})
                .ToListAsync()
                ;

            return Ok(new { list = list, count = count });
        }


        [HttpGet]
        public override async Task<IActionResult> Get()
        {
            var list = await _context.Products.OrderByName<Product>("Id").ToListAsync();

            return Ok(list);
        }

        
        [HttpGet("{id}")]
        public override async Task<IActionResult> Get(int id)
        {
            var model = await _context.Products.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpPost]
        public override async Task<IActionResult> Add(Product model)
        {
            _context.Products.Add(model);

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
        public override async Task<IActionResult> Update([FromRoute] int id, [FromBody] Product model)
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
            var model = await _context.Products.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            _context.Products.Remove(model);
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