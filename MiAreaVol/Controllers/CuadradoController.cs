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
    public class CuadradoController : ControllerBase
    {
        private readonly CuadradoService _service;
        public CuadradoController(CuadradoService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
            => await _service.GetByIdAsync(id) is Cuadrado c ? Ok(c) : NotFound();

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Cuadrado c) => Ok(await _service.CreateAsync(c));

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Cuadrado c)
            => await _service.UpdateAsync(id, c) ? Ok() : NotFound();

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
            => await _service.DeleteAsync(id) ? Ok() : NotFound();

        [HttpGet("paginado")] public IActionResult GetPaged(int pageNumber = 1, int pageSize = 10)
            => Ok(_service.GetPaged(pageNumber, pageSize));
    }
} 