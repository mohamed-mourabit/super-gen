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
    public class ShopsController : SuperController<Shop>
    {
        public ShopsController(MyContext context ) : base(context) { }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}/{companyName}/{socialName}/{subdomain}/{email}/{logoUrl}/{headerUrl}/{reimbursmentPolitic}/{satisfactionPolitic}/{warranty}/{productLimit}/{pictureLimit}/{phone}/{facebook}/{whatsapp}/{activityId}/{deliveryTypeId}/{cashbackId}/{discountId}")]
        public async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir, string companyName, string socialName, string subdomain, string email, string logoUrl, string headerUrl, string reimbursmentPolitic, string satisfactionPolitic, string warranty, int productLimit, int pictureLimit, string phone, string facebook, string whatsapp, int activityId, int deliveryTypeId, int cashbackId, int discountId)
        {
            var q = _context.Shops
                .Where(e => companyName == "*" ? true : e.CompanyName.ToLower().Contains(companyName.ToLower()))
.Where(e => socialName == "*" ? true : e.SocialName.ToLower().Contains(socialName.ToLower()))
.Where(e => subdomain == "*" ? true : e.Subdomain.ToLower().Contains(subdomain.ToLower()))
.Where(e => email == "*" ? true : e.Email.ToLower().Contains(email.ToLower()))
.Where(e => logoUrl == "*" ? true : e.LogoUrl.ToLower().Contains(logoUrl.ToLower()))
.Where(e => headerUrl == "*" ? true : e.HeaderUrl.ToLower().Contains(headerUrl.ToLower()))
.Where(e => reimbursmentPolitic == "*" ? true : e.ReimbursmentPolitic.ToLower().Contains(reimbursmentPolitic.ToLower()))
.Where(e => satisfactionPolitic == "*" ? true : e.SatisfactionPolitic.ToLower().Contains(satisfactionPolitic.ToLower()))
.Where(e => warranty == "*" ? true : e.Warranty.ToLower().Contains(warranty.ToLower()))
.Where(e => productLimit == 0 ? true : e.ProductLimit == productLimit)
.Where(e => pictureLimit == 0 ? true : e.PictureLimit == pictureLimit)
.Where(e => phone == "*" ? true : e.Phone.ToLower().Contains(phone.ToLower()))
.Where(e => facebook == "*" ? true : e.Facebook.ToLower().Contains(facebook.ToLower()))
.Where(e => whatsapp == "*" ? true : e.Whatsapp.ToLower().Contains(whatsapp.ToLower()))
.Where(e => activityId == 0 ? true : e.ActivityId == activityId)
.Where(e => deliveryTypeId == 0 ? true : e.DeliveryTypeId == deliveryTypeId)
.Where(e => cashbackId == 0 ? true : e.CashbackId == cashbackId)
.Where(e => discountId == 0 ? true : e.DiscountId == discountId)

                ;

            int count = await q.CountAsync();

            var list = await q.OrderByName<Shop>(sortBy, sortDir == "desc")
                .Skip(startIndex)
                .Take(pageSize)
                
                .Select(e => new 
{
id = e.Id,
companyName = e.CompanyName,
socialName = e.SocialName,
description = e.Description,
subdomain = e.Subdomain,
email = e.Email,
logoUrl = e.LogoUrl,
headerUrl = e.HeaderUrl,
reimbursmentPolitic = e.ReimbursmentPolitic,
alwaysOpen = e.AlwaysOpen,
preorder = e.Preorder,
review = e.Review,
satisfactionPolitic = e.SatisfactionPolitic,
warranty = e.Warranty,
active = e.Active,
list = e.List,
grid = e.Grid,
takeAway = e.TakeAway,
productLimit = e.ProductLimit,
pictureLimit = e.PictureLimit,
creationDate = e.CreationDate,
editDate = e.EditDate,
phone = e.Phone,
facebook = e.Facebook,
whatsapp = e.Whatsapp,
activityId = e.ActivityId,
deliveryTypeId = e.DeliveryTypeId,
cashbackId = e.CashbackId,
discountId = e.DiscountId,

})
                .ToListAsync()
                ;

            return Ok(new { list = list, count = count });
        }


        [HttpGet]
        public override async Task<IActionResult> Get()
        {
            var list = await _context.Shops.OrderByName<Shop>("Id").ToListAsync();

            return Ok(list);
        }

        
        [HttpGet("{id}")]
        public override async Task<IActionResult> Get(int id)
        {
            var model = await _context.Shops.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpPost]
        public override async Task<IActionResult> Add(Shop model)
        {
            _context.Shops.Add(model);

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
        public override async Task<IActionResult> Update([FromRoute] int id, [FromBody] Shop model)
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
            var model = await _context.Shops.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            _context.Shops.Remove(model);
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