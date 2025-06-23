using Microsoft.AspNetCore.Mvc;
using MiAreaVol.Models;

namespace MiAreaVol.Controllers
{
    [ApiController]
    [Route("api/piramide")]
    public class PiramideController : ControllerBase
    {
        [HttpPost("calcular-volumen")]
        public ActionResult<double> CalcularVolumen([FromBody] CalculoVolumenRequest request)
        {
            if (request.Base == null || request.Altura == null)
                return BadRequest("Se requieren los parámetros 'base' y 'altura'.");

            double volumen = (1.0 / 3.0) * request.Base.Value * request.Altura.Value;
            return Ok(volumen);
        }
    }
} 