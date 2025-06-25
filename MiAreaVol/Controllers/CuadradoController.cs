using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiAreaVol.Models;
using MiAreaVol.Services;

namespace MiAreaVol.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CuadradoController : ControllerBase
    {
        private readonly CuadradoService _service;
        public CuadradoController(CuadradoService service) => _service = service;

        [HttpGet] public IActionResult Get() => Ok(_service.GetAll());
        [HttpGet("{id}")] public IActionResult Get(int id)
            => _service.GetById(id) is Cuadrado c ? Ok(c) : NotFound();
        [HttpPost] public IActionResult Post([FromBody] Cuadrado c) => Ok(_service.Create(c));
        [HttpPut("{id}")] public IActionResult Put(int id, [FromBody] Cuadrado c)
            => _service.Update(id, c) ? Ok() : NotFound();
        [HttpDelete("{id}")] public IActionResult Delete(int id)
            => _service.Delete(id) ? Ok() : NotFound();
        [HttpGet("paginado")] public IActionResult GetPaged(int pageNumber = 1, int pageSize = 10)
            => Ok(_service.GetPaged(pageNumber, pageSize));
    }
} 