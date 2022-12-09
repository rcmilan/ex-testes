using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using testesapi.Services;

namespace testesapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("FuncaoA")]
        public IActionResult FuncaoA(int num, string txt)
        {
            var s = new ServiceService();

            var result = s.FuncaoA(new DTOs.AInput { Num = num, Txt = txt });

            return Ok(result);
        }
    }
}
