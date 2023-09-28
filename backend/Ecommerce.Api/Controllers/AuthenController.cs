using Ecommerce.Core.DTOs;
using Ecommerce.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenController : ControllerBase
    {
        private readonly IAuthenService _service;

        public AuthenController(IAuthenService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            return Ok(await _service.Register(request));
        }
    }
}
