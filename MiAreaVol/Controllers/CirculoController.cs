using Microsoft.AspNetCore.Mvc;
using MiAreaVol.Models;
using System;

namespace MiAreaVol.Controllers
{
    [ApiController]
    [Route("api/circulo")]
    public class CirculoController : ControllerBase
    {
        [HttpPost("calcular")]
        public ActionResult<double> CalcularArea([FromBody] CalculoAreaRequest request)
        {
            if (request.Radio == null)
                return BadRequest("Se requiere el parámetro 'radio'.");

            double area = Math.PI * Math.Pow(request.Radio.Value, 2);
            return Ok(area);
        }
    }
} 