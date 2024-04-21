using Domain.Interfaces.IDespesa;
using Domain.Interfaces.IServicos;
using Entities.Entidades;

namespace Domain.Servicos
{
    public class DespesaServico : IDespesaServico
    {
        private readonly IDespesa _iDespesa;
        public DespesaServico(IDespesa iDespesa)
        {
             _iDespesa = iDespesa;
        }
        public async Task AdicionarDespesa(Despesa despesa)
        {
            var data = DateTime.UtcNow;
            despesa.DataCadastro = data;
            despesa.Ano = data.Year;
            despesa.Mes = data.Month;

            var valido = despesa.ValidarPropriedadeString(despesa.Nome, "Nome");
            if(valido)
                await _iDespesa.Add(despesa);
        }

        public async Task AtualizarDespesa(Despesa despesa)
        {
            var data = DateTime.UtcNow;
            despesa.DataCadastro = data;

            if (despesa.Pago)
            {
                despesa.DataPagamento = data;
            }

            var valido = despesa.ValidarPropriedadeString(despesa.Nome, "Nome");
            if (valido)
                await _iDespesa.Add(despesa);
        }
    }
}
