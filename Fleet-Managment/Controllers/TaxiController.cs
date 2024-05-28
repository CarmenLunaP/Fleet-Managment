using Fleet_Managment.Contex;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Fleet_Managment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaxiController : ControllerBase
    {
        private readonly ContexBD _context;
        public TaxiController(ContexBD context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetTaxis(int pageNumber = 1, int pageSize = 10)
        {
            var query = _context.Taxis
                .Select(t => new { id = t.Id, plate = t.Plate })
                .OrderBy(t => t.id);

            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return Ok(new { items, totalCount });
        }

    }
}
