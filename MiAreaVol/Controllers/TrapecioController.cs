using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiAreaVol.Models;
using MiAreaVol.Services;
using System.Threading.Tasks;

namespace MiAreaVol.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TrapecioController : ControllerBase
    {
        private readonly TrapecioService _service;
        public TrapecioController(TrapecioService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
            => await _service.GetByIdAsync(id) is Trapecio t ? Ok(t) : NotFound();

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Trapecio t) => Ok(await _service.CreateAsync(t));

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Trapecio t)
            => await _service.UpdateAsync(id, t) ? Ok() : NotFound();

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
            => await _service.DeleteAsync(id) ? Ok() : NotFound();
    }
} 