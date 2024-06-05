
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fleet_Managment.Contex;


namespace Fleet_Managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LastLocationController : ControllerBase
    {
        private readonly ContexBD _context;
        private const int DefaultPageSize = 10; 

        public LastLocationController(ContexBD context)
        {
            _context = context;
        }

        // GET: api/LastLocation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetLastLocations([FromQuery] int page = 1, [FromQuery] int pageSize = DefaultPageSize)
        {
            try
            {
                // Validar los parámetros de paginación
                if (page < 1)
                {
                    page = 1;
                }

                if (pageSize < 1)
                {
                    pageSize = DefaultPageSize;
                }

                var query = _context.Trajectories
                    .Include(t => t.Taxi)
                    .GroupBy(t => t.TaxiId)
                    .Select(g => g.OrderByDescending(t => t.Date).FirstOrDefault());

                var totalItems = await query.CountAsync();
                var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

                var latestTaxis = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var lastLocationResponse = latestTaxis.Select(t => new
                {
                    Id = t.Taxi.Id,
                    Plate = t.Taxi.Plate,
                    Latitude = t.Latitude,
                    Longitude = t.Longitude,
                    Timestamp = t.Date
                }).ToList();

                // Devolver una respuesta con información de paginación
                var response = new
                {
                    Items = lastLocationResponse,
                    TotalItems = totalItems,
                    TotalPages = totalPages,
                    Page = page,
                    PageSize = pageSize
                    
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}
