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
    public class AddressesController : SuperController<_Address>
    {
        public AddressesController(MyContext context ) : base(context) { }
        
        [HttpGet("{id}")]
        public override async Task<IActionResult> GetById(int id)
        {
            var model = await _context.Addresses.Where(e => e.Id == id)
                .FirstOrDefaultAsync();
                ;

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }
    }
}