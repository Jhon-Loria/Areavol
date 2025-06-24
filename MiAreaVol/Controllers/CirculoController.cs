using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiAreaVol.Models;
using MiAreaVol.Services;

namespace MiAreaVol.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CirculoController : ControllerBase
    {
        private readonly CirculoService _service;
        public CirculoController(CirculoService service) => _service = service;

        [HttpGet] public IActionResult Get() => Ok(_service.GetAll());
        [HttpGet("{id}")] public IActionResult Get(int id)
            => _service.GetById(id) is Circulo c ? Ok(c) : NotFound();
        [HttpPost] public IActionResult Post([FromBody] Circulo c) => Ok(_service.Create(c));
        [HttpPut("{id}")] public IActionResult Put(int id, [FromBody] Circulo c)
            => _service.Update(id, c) ? Ok() : NotFound();
        [HttpDelete("{id}")] public IActionResult Delete(int id)
            => _service.Delete(id) ? Ok() : NotFound();
    }
} 