using Microsoft.AspNetCore.Mvc;
using MiAreaVol.Models;

namespace MiAreaVol.Controllers
{
    [ApiController]
    [Route("api/trapecio")]
    public class TrapecioController : ControllerBase
    {
        [HttpPost("calcular")]
        public ActionResult<double> CalcularArea([FromBody] CalculoAreaRequest request)
        {
            if (request.Base == null || request.Lado == null || request.Altura == null)
                return BadRequest("Se requieren los parámetros 'base', 'lado' (base menor) y 'altura'.");

            double area = ((request.Base.Value + request.Lado.Value) * request.Altura.Value) / 2;
            return Ok(area);
        }
    }
} 