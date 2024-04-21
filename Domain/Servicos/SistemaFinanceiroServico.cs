using Domain.Interfaces.IDespesa;
using Domain.Interfaces.IServicos;
using Domain.Interfaces.ISistemaFinanceiro;
using Entities.Entidades;

namespace Domain.Servicos
{
    public class SistemaFinanceiroServico : ISistemaFinanceiroServico
    {
        private readonly ISistemaFinanceiro _iSistemafinanceiro;

        public SistemaFinanceiroServico(ISistemaFinanceiro iSistemafinanceiro)
        {
            _iSistemafinanceiro = iSistemafinanceiro;
        }
        public async Task AdicionarSistemaFinanceiro(SistemaFinanceiro sistemaFinanceiro)
        {
            var valido = sistemaFinanceiro.ValidarPropriedadeString(sistemaFinanceiro.Nome, "Nome");
            if (valido)
            {
                var data = DateTime.Now;

                sistemaFinanceiro.DiaFechamento = 1;
                sistemaFinanceiro.Ano = data.Year;
                sistemaFinanceiro.Mes = data.Month;
                sistemaFinanceiro.AnoCopia = data.Year;
                sistemaFinanceiro.MesCopia = data.Month;
                sistemaFinanceiro.GerarCopiaDespesa = true;

                await _iSistemafinanceiro.Add(sistemaFinanceiro);
            }
        }

        public async Task AtualizarSistemaFinanceiro(SistemaFinanceiro sistemaFinanceiro)
        {
            var valido = sistemaFinanceiro.ValidarPropriedadeString(sistemaFinanceiro.Nome, "Nome");
            if (!valido)
            {
                sistemaFinanceiro.DiaFechamento = 1;
                await _iSistemafinanceiro.Update(sistemaFinanceiro);
            }
        }
    }
}
