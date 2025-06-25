using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiAreaVol.Models;
using MiAreaVol.Services;

namespace MiAreaVol.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RectanguloController : ControllerBase
    {
        private readonly RectanguloService _service;
        public RectanguloController(RectanguloService service) => _service = service;

        [HttpGet] public IActionResult Get() => Ok(_service.GetAll());
        [HttpGet("{id}")] public IActionResult Get(int id)
            => _service.GetById(id) is Rectangulo r ? Ok(r) : NotFound();
        [HttpPost] public IActionResult Post([FromBody] Rectangulo r) => Ok(_service.Create(r));
        [HttpPut("{id}")] public IActionResult Put(int id, [FromBody] Rectangulo r)
            => _service.Update(id, r) ? Ok() : NotFound();
        [HttpDelete("{id}")] public IActionResult Delete(int id)
            => _service.Delete(id) ? Ok() : NotFound();
        [HttpGet("paginado")] public IActionResult GetPaged(int pageNumber = 1, int pageSize = 10)
            => Ok(_service.GetPaged(pageNumber, pageSize));
    }
} 