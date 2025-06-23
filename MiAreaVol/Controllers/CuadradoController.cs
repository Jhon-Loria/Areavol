using Microsoft.AspNetCore.Mvc;
using MiAreaVol.Models;

namespace MiAreaVol.Controllers
{
    [ApiController]
    [Route("api/cuadrado")]
    public class CuadradoController : ControllerBase
    {
        [HttpPost("calcular")]
        public ActionResult<double> CalcularArea([FromBody] CalculoAreaRequest request)
        {
            if (request.Lado == null)
                return BadRequest("Se requiere el parámetro 'lado'.");

            double area = request.Lado.Value * request.Lado.Value;
            return Ok(area);
        }
    }
} 