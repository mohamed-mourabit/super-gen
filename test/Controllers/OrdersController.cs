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
    public class OrdersController : SuperController<Order>
    {
        public OrdersController(MyContext context ) : base(context) { }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}/{price}/{deliveryPrice}/{number}/{cashback}/{usedCashback}/{usedDiscountCode}/{cartId}/{addressId}/{deliveryManId}/{paymentStatusId}/{orderStatusId}/{paymentMethodId}")]
        public async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir, int price, int deliveryPrice, string number, int cashback, int usedCashback, string usedDiscountCode, int cartId, int addressId, int deliveryManId, int paymentStatusId, int orderStatusId, int paymentMethodId)
        {
            var q = _context.Orders
                .Where(e => price == 0 ? true : e.Price == price)
.Where(e => deliveryPrice == 0 ? true : e.DeliveryPrice == deliveryPrice)
.Where(e => number == "*" ? true : e.Number.ToLower().Contains(number.ToLower()))
.Where(e => cashback == 0 ? true : e.Cashback == cashback)
.Where(e => usedCashback == 0 ? true : e.UsedCashback == usedCashback)
.Where(e => usedDiscountCode == "*" ? true : e.UsedDiscountCode.ToLower().Contains(usedDiscountCode.ToLower()))
.Where(e => cartId == 0 ? true : e.CartId == cartId)
.Where(e => addressId == 0 ? true : e.AddressId == addressId)
.Where(e => deliveryManId == 0 ? true : e.DeliveryManId == deliveryManId)
.Where(e => paymentStatusId == 0 ? true : e.PaymentStatusId == paymentStatusId)
.Where(e => orderStatusId == 0 ? true : e.OrderStatusId == orderStatusId)
.Where(e => paymentMethodId == 0 ? true : e.PaymentMethodId == paymentMethodId)

                ;

            int count = await q.CountAsync();

            var list = await q.OrderByName<Order>(sortBy, sortDir == "desc")
                .Skip(startIndex)
                .Take(pageSize)
                
                .Select(e => new 
{
id = e.Id,
orderDate = e.OrderDate,
deliveryDate = e.DeliveryDate,
price = e.Price,
deliveryPrice = e.DeliveryPrice,
active = e.Active,
number = e.Number,
editDate = e.EditDate,
cashback = e.Cashback,
usedCashback = e.UsedCashback,
usedDiscountCode = e.UsedDiscountCode,
cartId = e.CartId,
addressId = e.AddressId,
deliveryManId = e.DeliveryManId,
paymentStatusId = e.PaymentStatusId,
orderStatusId = e.OrderStatusId,
paymentMethodId = e.PaymentMethodId,

})
                .ToListAsync()
                ;

            return Ok(new { list = list, count = count });
        }


        [HttpGet]
        public override async Task<IActionResult> Get()
        {
            var list = await _context.Orders.OrderByName<Order>("Id").ToListAsync();

            return Ok(list);
        }

        
        [HttpGet("{id}")]
        public override async Task<IActionResult> Get(int id)
        {
            var model = await _context.Orders.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpPost]
        public override async Task<IActionResult> Add(Order model)
        {
            _context.Orders.Add(model);

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
        public override async Task<IActionResult> Update([FromRoute] int id, [FromBody] Order model)
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
            var model = await _context.Orders.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(model);
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