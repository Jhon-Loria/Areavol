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
    public class RectanguloController : ControllerBase
    {
        private readonly RectanguloService _service;
        public RectanguloController(RectanguloService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
            => await _service.GetByIdAsync(id) is Rectangulo r ? Ok(r) : NotFound();

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Rectangulo r) => Ok(await _service.CreateAsync(r));

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Rectangulo r)
            => await _service.UpdateAsync(id, r) ? Ok() : NotFound();

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
            => await _service.DeleteAsync(id) ? Ok() : NotFound();

        [HttpGet("paginado")] public IActionResult GetPaged(int pageNumber = 1, int pageSize = 10)
            => Ok(_service.GetPaged(pageNumber, pageSize));
    }
} 