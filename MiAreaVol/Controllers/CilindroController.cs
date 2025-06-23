using Microsoft.AspNetCore.Mvc;
using MiAreaVol.Models;
using System;

namespace MiAreaVol.Controllers
{
    [ApiController]
    [Route("api/cilindro")]
    public class CilindroController : ControllerBase
    {
        [HttpPost("calcular-volumen")]
        public ActionResult<double> CalcularVolumen([FromBody] CalculoVolumenRequest request)
        {
            if (request.Radio == null || request.Altura == null)
                return BadRequest("Se requieren los parámetros 'radio' y 'altura'.");

            double volumen = Math.PI * Math.Pow(request.Radio.Value, 2) * request.Altura.Value;
            return Ok(volumen);
        }
    }
} 