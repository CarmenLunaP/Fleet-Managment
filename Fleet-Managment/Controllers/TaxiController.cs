using Fleet_Managment.Contex;
using Microsoft.AspNetCore.Mvc;


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
        public IActionResult GetTaxis()
        {
            var taxis = _context.Taxis
                .Select(t => new { id = t.Id, plate = t.Plate })
                .ToList();

            return Ok(taxis);
        }
    }
}

