using Microsoft.AspNetCore.Mvc;
using MiAreaVol.Models;

namespace MiAreaVol.Controllers
{
    [ApiController]
    [Route("api/rectangulo")]
    public class RectanguloController : ControllerBase
    {
        [HttpPost("calcular")]
        public ActionResult<double> CalcularArea([FromBody] CalculoAreaRequest request)
        {
            if (request.Largo == null || request.Ancho == null)
                return BadRequest("Se requieren los parámetros 'largo' y 'ancho'.");

            double area = request.Largo.Value * request.Ancho.Value;
            return Ok(area);
        }
    }
} 