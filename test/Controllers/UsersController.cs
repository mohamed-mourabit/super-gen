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
    public class UsersController : SuperController<User>
    {
        public UsersController(MyContext context ) : base(context) { }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}/{email}/{username}/{lastname}/{firstname}/{token}/{languageId}/{roleId}")]
        public async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir, string email, string username, string lastname, string firstname, string token, int languageId, int roleId)
        {
            var q = _context.Users
                .Where(e => email == "*" ? true : e.Email.ToLower().Contains(email.ToLower()))
.Where(e => username == "*" ? true : e.Username.ToLower().Contains(username.ToLower()))
.Where(e => lastname == "*" ? true : e.Lastname.ToLower().Contains(lastname.ToLower()))
.Where(e => firstname == "*" ? true : e.Firstname.ToLower().Contains(firstname.ToLower()))
.Where(e => token == "*" ? true : e.Token.ToLower().Contains(token.ToLower()))
.Where(e => languageId == 0 ? true : e.LanguageId == languageId)
.Where(e => roleId == 0 ? true : e.RoleId == roleId)

                ;

            int count = await q.CountAsync();

            var list = await q.OrderByName<User>(sortBy, sortDir == "desc")
                .Skip(startIndex)
                .Take(pageSize)
                
                .Select(e => new 
{
id = e.Id,
email = e.Email,
password = e.Password,
username = e.Username,
lastname = e.Lastname,
firstname = e.Firstname,
token = e.Token,
languageId = e.LanguageId,
roleId = e.RoleId,

})
                .ToListAsync()
                ;

            return Ok(new { list = list, count = count });
        }


        [HttpGet]
        public override async Task<IActionResult> Get()
        {
            var list = await _context.Users.OrderByName<User>("Id").ToListAsync();

            return Ok(list);
        }

        
        [HttpGet("{id}")]
        public override async Task<IActionResult> Get(int id)
        {
            var model = await _context.Users.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpPost]
        public override async Task<IActionResult> Add(User model)
        {
            _context.Users.Add(model);

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
        public override async Task<IActionResult> Update([FromRoute] int id, [FromBody] User model)
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
            var model = await _context.Users.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            _context.Users.Remove(model);
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