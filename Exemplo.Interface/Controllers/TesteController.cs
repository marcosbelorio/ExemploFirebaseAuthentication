using System.Linq;
using System.Threading.Tasks;
using Firebase.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exemplo.Interface.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class TesteController : ControllerBase
    {
        private const string ApiKey = "<ApiKey-Firebase>";

        public TesteController()
        { }

        [HttpGet("autorizado")]
        public IActionResult MetodoAutorizado()
        {
            return Ok(User.Claims.Where(x => x.Type == "firebase").FirstOrDefault().Value);
        }

        [HttpPost("cadastrar")]
        [AllowAnonymous]
        public async Task<IActionResult> Cadastrar(LoginDto dto)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));

            var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(dto.Email, dto.Senha);

            return Ok(auth.FirebaseToken);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));

            var auth = await authProvider.SignInWithEmailAndPasswordAsync(dto.Email, dto.Senha);

            return Ok(auth.FirebaseToken);
        }
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}