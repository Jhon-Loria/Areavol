using Microsoft.AspNetCore.Mvc;

namespace MiAreaVol.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public ActionResult<string> Get()
    {
        return Ok("¡MiAreaVol API está funcionando correctamente! 🎉");
    }

    [HttpGet("health")]
    public ActionResult<object> Health()
    {
        return Ok(new
        {
            status = "OK",
            message = "API funcionando correctamente",
            timestamp = DateTime.Now,
            version = "1.0.0"
        });
    }
} 