using Domain.Interfaces.IServicos;
using Domain.Interfaces.ISistemaFinanceiro;
using Entities.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SistemaFinanceirosController : ControllerBase
    {
        private readonly ISistemaFinanceiro _IsistemaFinanceiro;
        private readonly ISistemaFinanceiroServico _IsistemaFinanceiroServico;
        public SistemaFinanceirosController(ISistemaFinanceiro IsistemaFinanceiro, ISistemaFinanceiroServico IsistemaFinanceiroServico)
        {
                _IsistemaFinanceiro = IsistemaFinanceiro;
                _IsistemaFinanceiroServico = IsistemaFinanceiroServico;
        }

        [HttpGet("/api/ListaSistemasUsuario")]
        [Produces("application/json")]
        public async Task<object> ListaSistemasUsuario(string emailUsuario) 
        {
            return await _IsistemaFinanceiro.ListaSistemasUsuario(emailUsuario);
        }

        [HttpPost("/api/AdicionarSistemaFinanceiro")]
        [Produces("application/json")]
        public async Task<object> AdicionarSistemaFinanceiro(SistemaFinanceiro sistemaFinanceiro)
        {
            await _IsistemaFinanceiroServico.AdicionarSistemaFinanceiro(sistemaFinanceiro);

            return Task.FromResult(sistemaFinanceiro);
        }

        [HttpPut("/api/AtualizarSistemaFinanceiro")]
        [Produces("application/json")]
        public async Task<object> AtualizarSistemaFinanceiro(SistemaFinanceiro sistemaFinanceiro)
        {
            await _IsistemaFinanceiroServico.AtualizarSistemaFinanceiro(sistemaFinanceiro);

            return Task.FromResult(sistemaFinanceiro);
        }

        [HttpPut("/api/ObterSistemaFinanceiro")]
        [Produces("application/json")]
        public async Task<object> ObterSistemaFinanceiro(int id)
        {
            return await _IsistemaFinanceiro.GetEntityById(id);

        }

        [HttpDelete("/api/DeleteSistemaFinanceiro")]
        [Produces("application/json")]
        public async Task<object> DeleteSistemaFinanceiro(int id)
        {
            try
            {
                var sistemaFinanceiro = await _IsistemaFinanceiro.GetEntityById(id);
                await _IsistemaFinanceiro.Delete(sistemaFinanceiro);
            }
            catch (Exception ex)
            {

                return false;
            }

            return true;
        }
    }
}
