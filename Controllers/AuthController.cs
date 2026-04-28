using Microsoft.AspNetCore.Mvc;
using taskManagerApi.Dtos;
using taskManagerApi.Service;

namespace taskManagerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly TokenService _tokenService;

        public AuthController(UserService userService, TokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            await _userService.RegisterAsync(dto);
            return Ok("Usuário criado com sucesso");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            var user = await _userService.LoginAsync(dto.Username, dto.Password);

            if (user == null)
            {
                return Unauthorized("Usuário ou senha inválidos");
            }

            var token = _tokenService.GenerateToken(user);
            return Ok(
                new
                {
                    token,
                    user = new { user.Id, user.Username, user.FullName }
                }
            );
        }

    }
}