using API.Security.YourNamespace.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.DTOs.Request;
using Shared.Models.DTOs.Response;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AuthRequestDTO request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
                return BadRequest("Username e senha são obrigatórios");

            string token = await _authService.Register(request.Username, request.Password);
            if (token == "")
                return BadRequest("Usuário já existe");

            return Ok(new AuthResponseDTO(request.Username, token));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthRequestDTO request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
                return BadRequest("Username e senha são obrigatórios");

            string token = await _authService.Authenticate(request.Username, request.Password);
            if (token == null)
                return Unauthorized("Credenciais inválidas");

            return Ok(new AuthResponseDTO(request.Username, token));
        }
    }
}

