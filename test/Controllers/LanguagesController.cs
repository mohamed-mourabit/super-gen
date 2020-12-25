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
    public class LanguagesController : SuperController<Language>
    {
        public LanguagesController(MyContext context ) : base(context) { }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}/{value}")]
        public async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir, string value)
        {
            var q = _context.Languages
                .Where(e => value == "*" ? true : e.Value.ToLower().Contains(value.ToLower()))

                ;

            int count = await q.CountAsync();

            var list = await q.OrderByName<Language>(sortBy, sortDir == "desc")
                .Skip(startIndex)
                .Take(pageSize)
                
                .Select(e => new 
{
id = e.Id,
value = e.Value,

})
                .ToListAsync()
                ;

            return Ok(new { list = list, count = count });
        }


        [HttpGet]
        public override async Task<IActionResult> Get()
        {
            var list = await _context.Languages.OrderByName<Language>("Id").ToListAsync();

            return Ok(list);
        }

        
        [HttpGet("{id}")]
        public override async Task<IActionResult> Get(int id)
        {
            var model = await _context.Languages.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpPost]
        public override async Task<IActionResult> Add(Language model)
        {
            _context.Languages.Add(model);

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
        public override async Task<IActionResult> Update([FromRoute] int id, [FromBody] Language model)
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
            var model = await _context.Languages.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            _context.Languages.Remove(model);
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