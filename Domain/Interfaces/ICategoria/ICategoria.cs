using Domain.Interfaces.Generics;
using Entities.Entidades;

namespace Domain.Interfaces.ICategoria
{
    public interface ICategoria : InterfaceGeneric<Categoria>
    {
        Task<IList<ICategoria>> ListarCategoriasUsuario(string emailUsuario);
    }
}