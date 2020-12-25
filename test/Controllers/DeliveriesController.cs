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
    public class DeliveriesController : SuperController<Delivery>
    {
        public DeliveriesController(MyContext context ) : base(context) { }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}/{from}/{to}/{freeFrom}/{radius}/{minOrderPrice}/{price}/{deliveryTime}/{deliveryModeId}/{shopId}")]
        public async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir, int from, int to, int freeFrom, int radius, int minOrderPrice, int price, int deliveryTime, int deliveryModeId, int shopId)
        {
            var q = _context.Deliveries
                .Where(e => from == 0 ? true : e.From == from)
.Where(e => to == 0 ? true : e.To == to)
.Where(e => freeFrom == 0 ? true : e.FreeFrom == freeFrom)
.Where(e => radius == 0 ? true : e.Radius == radius)
.Where(e => minOrderPrice == 0 ? true : e.MinOrderPrice == minOrderPrice)
.Where(e => price == 0 ? true : e.Price == price)
.Where(e => deliveryTime == 0 ? true : e.DeliveryTime == deliveryTime)
.Where(e => deliveryModeId == 0 ? true : e.DeliveryModeId == deliveryModeId)
.Where(e => shopId == 0 ? true : e.ShopId == shopId)

                ;

            int count = await q.CountAsync();

            var list = await q.OrderByName<Delivery>(sortBy, sortDir == "desc")
                .Skip(startIndex)
                .Take(pageSize)
                
                .Select(e => new 
{
id = e.Id,
downtown = e.Downtown,
from = e.From,
to = e.To,
freeFrom = e.FreeFrom,
radius = e.Radius,
minOrderPrice = e.MinOrderPrice,
price = e.Price,
deliveryTime = e.DeliveryTime,
deliveryModeId = e.DeliveryModeId,
shopId = e.ShopId,

})
                .ToListAsync()
                ;

            return Ok(new { list = list, count = count });
        }


        [HttpGet]
        public override async Task<IActionResult> Get()
        {
            var list = await _context.Deliveries.OrderByName<Delivery>("Id").ToListAsync();

            return Ok(list);
        }

        
        [HttpGet("{id}")]
        public override async Task<IActionResult> Get(int id)
        {
            var model = await _context.Deliveries.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpPost]
        public override async Task<IActionResult> Add(Delivery model)
        {
            _context.Deliveries.Add(model);

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
        public override async Task<IActionResult> Update([FromRoute] int id, [FromBody] Delivery model)
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
            var model = await _context.Deliveries.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            _context.Deliveries.Remove(model);
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