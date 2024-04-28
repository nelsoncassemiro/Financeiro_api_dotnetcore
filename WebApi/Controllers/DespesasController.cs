using Domain.Interfaces.IDespesa;
using Domain.Interfaces.IServicos;
using Entities.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DespesasController : ControllerBase
    {
        private readonly IDespesa _Idespesa;
        private readonly IDespesaServico _IdespesaServico;
        public DespesasController(IDespesa Idespesa, IDespesaServico IdespesaServico)
        {
                _Idespesa = Idespesa;
                _IdespesaServico = IdespesaServico;
        }

        [HttpGet("/api/ListarDespesasUsuario")]
        [Produces("application/json")]
        public async Task<object> ListarDespesasUsuario(string emailUsuario)
        {
            return await _Idespesa.ListarDespesasUsuario(emailUsuario);
        }

        [HttpPost("/api/AdicionarDespesa")]
        [Produces("application/json")]
        public async Task<object> AdicionarDespesa(Despesa despesa)
        {
            await _IdespesaServico.AdicionarDespesa(despesa);
            return despesa;
        }

        [HttpPut("/api/AtualizarDespesa")]
        [Produces("application/json")]
        public async Task<object> AtualizarDespesa(Despesa despesa)
        {
            await _IdespesaServico.AtualizarDespesa(despesa);
            return despesa;
        }

        [HttpGet("/api/ObterDespesa")]
        [Produces("application/json")]
        public async Task<object> ObterDespesa(int id)
        {
            return await _Idespesa.GetEntityById(id);     
        }

        [HttpDelete("/api/DeleteDespesa")]
        [Produces("application/json")]
        public async Task<object> DeleteDispesa(int id)
        {
            try
            {
                var despesa = await _Idespesa.GetEntityById(id);
                await _Idespesa.Delete(despesa);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        [HttpGet("/api/CarregaGraficos")]
        [Produces("application/json")]
        public async Task<object> CarregaGraficos(string emailUsuario)
        {
            return await _IdespesaServico.CarregaGraficos(emailUsuario);           
        }
    }
}
