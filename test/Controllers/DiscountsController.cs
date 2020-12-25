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
    public class DiscountsController : SuperController<Discount>
    {
        public DiscountsController(MyContext context ) : base(context) { }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}/{name}/{amount}/{percent}/{code}/{affectations}/{affectationTypeId}/{discountTypeId}")]
        public async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir, string name, int amount, int percent, string code, string affectations, int affectationTypeId, int discountTypeId)
        {
            var q = _context.Discounts
                .Where(e => name == "*" ? true : e.Name.ToLower().Contains(name.ToLower()))
.Where(e => amount == 0 ? true : e.Amount == amount)
.Where(e => percent == 0 ? true : e.Percent == percent)
.Where(e => code == "*" ? true : e.Code.ToLower().Contains(code.ToLower()))
.Where(e => affectations == "*" ? true : e.Affectations.ToLower().Contains(affectations.ToLower()))
.Where(e => affectationTypeId == 0 ? true : e.AffectationTypeId == affectationTypeId)
.Where(e => discountTypeId == 0 ? true : e.DiscountTypeId == discountTypeId)

                ;

            int count = await q.CountAsync();

            var list = await q.OrderByName<Discount>(sortBy, sortDir == "desc")
                .Skip(startIndex)
                .Take(pageSize)
                
                .Select(e => new 
{
id = e.Id,
name = e.Name,
description = e.Description,
amount = e.Amount,
percent = e.Percent,
startDate = e.StartDate,
endDate = e.EndDate,
code = e.Code,
affectations = e.Affectations,
affectationTypeId = e.AffectationTypeId,
discountTypeId = e.DiscountTypeId,

})
                .ToListAsync()
                ;

            return Ok(new { list = list, count = count });
        }


        [HttpGet]
        public override async Task<IActionResult> Get()
        {
            var list = await _context.Discounts.OrderByName<Discount>("Id").ToListAsync();

            return Ok(list);
        }

        
        [HttpGet("{id}")]
        public override async Task<IActionResult> Get(int id)
        {
            var model = await _context.Discounts.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpPost]
        public override async Task<IActionResult> Add(Discount model)
        {
            _context.Discounts.Add(model);

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
        public override async Task<IActionResult> Update([FromRoute] int id, [FromBody] Discount model)
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
            var model = await _context.Discounts.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            _context.Discounts.Remove(model);
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