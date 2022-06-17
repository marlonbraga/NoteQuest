using NoteQuest.Domain.Core.Interfaces;
using System.Collections.Generic;

namespace NoteQuest.Domain.Core.Entities
{
    public class Personagem : IPersonagem
    {
        public IPontosDeVida Pv { get; set; }
        public string Nome { get; set; }
        public IRaca Raca { get; set; }
        public List<IClasse> Classes { get; set; }

        public IAcao ModificarAcao(IAcao acao)
        {
            IAcao acaoModificada;
            acaoModificada = Raca.AtualizarAcao(acao);
            foreach (IClasse classe in Classes)
            {
                acaoModificada = classe.AtualizarAcao(acao);
            }

            return acaoModificada;
        }
    }
}
