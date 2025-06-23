using Microsoft.AspNetCore.Mvc;
using MiAreaVol.Models;
using System;

namespace MiAreaVol.Controllers
{
    [ApiController]
    [Route("api/cono")]
    public class ConoController : ControllerBase
    {
        [HttpPost("calcular-volumen")]
        public ActionResult<double> CalcularVolumen([FromBody] CalculoVolumenRequest request)
        {
            if (request.Radio == null || request.Altura == null)
                return BadRequest("Se requieren los parámetros 'radio' y 'altura'.");

            double volumen = (1.0 / 3.0) * Math.PI * Math.Pow(request.Radio.Value, 2) * request.Altura.Value;
            return Ok(volumen);
        }
    }
} 