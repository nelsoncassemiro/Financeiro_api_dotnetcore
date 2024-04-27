using Entities.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http;
using System.Text;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UsersController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/AdicionarUsuario")]
        public async Task <IActionResult> AdicionarUsuario([FromBody] Login login)
        {
            if(string.IsNullOrWhiteSpace(login.email) ||
               string.IsNullOrWhiteSpace(login.senha) ||
               string.IsNullOrWhiteSpace(login.cpf))
            {
                return Ok("Falta Alguns dados");
            }

            var user = new ApplicationUser
            {
                Email = login.email,
                UserName = login.email,
                CPF = login.cpf
            };

            var result = await _userManager.CreateAsync(user, login.senha);

            if (result.Errors.Any())
            {
                return Ok(result.Errors);
            }

            //Geração de confirmação caso precise

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            // retorno email
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));

            var response_Retorn = await _userManager.ConfirmEmailAsync(user, code);

            if(response_Retorn.Succeeded)
            {
                return Ok("Usuario Adicionado!");
            }
            else
            {
                return Ok("Erro ao confirmar cadastro de usuario!");
            }
        }
    }
}
