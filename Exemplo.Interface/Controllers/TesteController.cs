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

        [HttpPut("trocarsenha")]
        public async Task<IActionResult> TrocarSenha(TrocarSenhaDto dto)
        {
            HttpContext.Request.Headers.TryGetValue("Authorization", out var token);

            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));

            var auth = await authProvider.ChangeUserPassword(token.ToString().Replace("Bearer ", ""), dto.NovaSenha);

            return Ok(auth.FirebaseToken);
        }

        [HttpDelete("excluir")]
        public async Task<IActionResult> Excluir()
        {
            HttpContext.Request.Headers.TryGetValue("Authorization", out var token);

            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));

            await authProvider.DeleteUserAsync(token.ToString().Replace("Bearer ", ""));

            return Ok();
        }
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }

    public class TrocarSenhaDto
    {
        public string NovaSenha { get; set; }
    }
}