using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fleet_Managment.Contex;


namespace Fleet_Managment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrajectoriesController : ControllerBase
    {
        private readonly ContexBD _context;
        public TrajectoriesController(ContexBD context)
        {
            _context = context;
        }
        [HttpGet("historial")]
        public async Task<IActionResult> GetTrajectories(
            [FromQuery] int taxiId,
            [FromQuery] DateTime date,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            // Convertir la fecha proporcionada a UTC
            var dateUtc = DateTime.SpecifyKind(date, DateTimeKind.Utc);
            // Obtener la consulta base de trayectorias del taxi para la fecha especificada
            var query = _context.Trajectories
                .Where(t => t.TaxiId == taxiId && t.Date.Date == dateUtc.Date);
            // Obtener el número total de trayectorias
            var totalRecords = await query.CountAsync();
            // Obtener las trayectorias paginadas
            var trajectories = await query
                .OrderBy(t => t.Date)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(t => new {
                    t.Latitude,
                    t.Longitude,
                    Timestamp = t.Date
                })
                .ToListAsync();
            // Verificar si se encontraron trayectorias
            if (!trajectories.Any())
            {
                return NotFound(new { Message = "No data found for the given taxi ID and date." });
            }
            // Devolver las trayectorias encontradas junto con la información de paginación
            var response = new
            {
                Data = trajectories,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
                
            };
            return Ok(response);
        }
    }
}