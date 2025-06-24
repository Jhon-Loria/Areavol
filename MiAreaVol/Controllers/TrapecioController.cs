using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiAreaVol.Models;
using MiAreaVol.Services;

namespace MiAreaVol.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TrapecioController : ControllerBase
    {
        private readonly TrapecioService _service;
        public TrapecioController(TrapecioService service) => _service = service;

        [HttpGet] public IActionResult Get() => Ok(_service.GetAll());
        [HttpGet("{id}")] public IActionResult Get(int id)
            => _service.GetById(id) is Trapecio t ? Ok(t) : NotFound();
        [HttpPost] public IActionResult Post(Trapecio t) => Ok(_service.Create(t));
        [HttpPut("{id}")] public IActionResult Put(int id, Trapecio t)
            => _service.Update(id, t) ? Ok() : NotFound();
        [HttpDelete("{id}")] public IActionResult Delete(int id)
            => _service.Delete(id) ? Ok() : NotFound();
    }
} 