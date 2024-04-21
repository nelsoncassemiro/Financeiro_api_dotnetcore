using Domain.Interfaces.ICategoria;
using Domain.Interfaces.IServicos;
using Entities.Entidades;

namespace Domain.Servicos
{
    public class CategoriaServico : ICategoriaServico
    {
        private readonly ICategoria _iCategoria;
        public CategoriaServico(ICategoria iCategoria)
        {
            _iCategoria = iCategoria;    
        }
        public async Task AdicionarCategoria(Categoria categoria)
        {
            var valido = categoria.ValidarPropriedadeString(categoria.Nome, "Nome");
            if (valido)
                await _iCategoria.Add(categoria);
        }

        public async Task AtualizarCategoria(Categoria categoria)
        {
            var valido = categoria.ValidarPropriedadeString(categoria.Nome, "Nome");
            if (valido)
                await _iCategoria.Update(categoria);
        }
    }
}
