using Microsoft.AspNetCore.Mvc;
using MiAreaVol.Models;
using System.Threading.Tasks;

namespace MiAreaVol.Controllers
{
    [ApiController]
    [Route("api/triangulo")]
    public class TrianguloController : ControllerBase
    {
        [HttpPost("calcular")]
        public ActionResult<double> CalcularArea([FromBody] CalculoAreaRequest request)
        {
            if (request.Base == null || request.Altura == null)
                return BadRequest("Se requieren los parámetros 'base' y 'altura'.");

            double area = (request.Base.Value * request.Altura.Value) / 2;
            return Ok(area);
        }
    }
} 