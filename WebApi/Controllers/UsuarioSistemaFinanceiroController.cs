using Domain.Interfaces.IServicos;
using Domain.Interfaces.IUsuarioSistemaFinanceiro;
using Entities.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioSistemaFinanceiroController : ControllerBase
    {
        private readonly IUsuarioSistemaFinanceiro _IusuarioSistemaFinanceiro;
        private readonly IUsuarioSistemaFinanceiroServico _IusuarioSistemaFinanceiroServico;

        public UsuarioSistemaFinanceiroController(IUsuarioSistemaFinanceiro IusuarioSistemaFinanceiro, IUsuarioSistemaFinanceiroServico IusuarioSistemaFinanceiroServico)
        {
            _IusuarioSistemaFinanceiro = IusuarioSistemaFinanceiro;
            _IusuarioSistemaFinanceiroServico = IusuarioSistemaFinanceiroServico;
        }

        [HttpGet("/api/ListarUsuariosSistema")]
        [Produces("application/json")]
        public async Task<object> ListaSistemasUsuario(int idSistema)
        {
            return await _IusuarioSistemaFinanceiro.ListarUsuariosSistema(idSistema);
        }

        [HttpPost("/api/CadastrarUsuarioNoSistema")]
        [Produces("application/json")]
        public async Task<object> CadastrarUsuarioNoSistema(int idSistema, string emailUsuario)
        {
            try
            {
                await _IusuarioSistemaFinanceiroServico.CadastraUsuarioNoSistema(
                new UsuarioSistemaFinanceiro
                {
                    IdSistema = idSistema,
                    EmailUsuario = emailUsuario,
                    Administrador = false,
                    SistemaAtual = true
                });
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }

        [HttpDelete("/api/DeleteUsuarioSistemaFinanceiro")]
        [Produces("application/json")]
        public async Task<object> DeleteUsuarioSistemaFinanceiro(int idSistema)
        {
            try
            {
                var usuarioSistemaFinanceiro = await _IusuarioSistemaFinanceiro.GetEntityById(idSistema);
                await _IusuarioSistemaFinanceiro.Delete(usuarioSistemaFinanceiro);

            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }
    }
}
