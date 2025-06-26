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
    public class CilindroController : ControllerBase
    {
        private readonly CilindroService _service;
        public CilindroController(CilindroService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
            => await _service.GetByIdAsync(id) is Cilindro c ? Ok(c) : NotFound();

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Cilindro c) => Ok(await _service.CreateAsync(c));

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Cilindro c)
            => await _service.UpdateAsync(id, c) ? Ok() : NotFound();

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
            => await _service.DeleteAsync(id) ? Ok() : NotFound();
    }
} 