using Domain.Interfaces.ICategoria;
using Domain.Interfaces.IServicos;
using Entities.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaServico _IcategoriaServico;
        private readonly ICategoria _Icategoria;
        public CategoriaController(ICategoriaServico IcategoriaServico, ICategoria Icategoria) 
        {
            _IcategoriaServico = IcategoriaServico;
            _Icategoria = Icategoria;
        }

        [HttpGet("/api/ListarCategoriasUsuario")]
        [Produces("application/json")]
        public async Task<object> ListarCategoriasUsuario(string emailUsuario)
        {
            return _Icategoria.ListarCategoriasUsuario(emailUsuario);
        }

        [HttpPost("/api/AdicionarCategoria")]
        [Produces("application/json")]
        public async Task<object> AdicionarCategoria(Categoria categoria)
        {
            await _IcategoriaServico.AdicionarCategoria(categoria);
            return categoria;
        }

        [HttpGet("/api/ObterCategoria")]
        [Produces("application/json")]
        public async Task<object> ObterCategoria(int id)
        {
            return await _Icategoria.GetEntityById(id);

        }

        [HttpDelete("/api/DeleteCategoria")]
        [Produces("application/json")]
        public async Task<object> DeleteCategoria(int id)
        {
            try
            {
                var categoria = await _Icategoria.GetEntityById(id);
                await _Icategoria.Delete(categoria);

            }
            catch (Exception)
            {
                return false;
            }
            return true;
            
        }
    }
}
