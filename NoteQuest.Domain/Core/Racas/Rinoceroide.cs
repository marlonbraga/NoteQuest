using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces.Personagem;
using NoteQuest.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteQuest.Domain.Core.Racas
{
    public class Rinoceroide : IRaca
    {
        public int Indice { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Pv { get; set; }
        public int QtdMagias { get; set; }
        public string Vantagem { get; set; }
        public Type TipoAcao { get; set; }
        public IEvent Acao { get; set; }

        public IEvent EffectSubstitutionComposite(IEvent gameEvent) => gameEvent;

        public void Build(/*IAcao acao*/)
        {
            //Acao = acao;
            Pv = 24;
            QtdMagias = 0;
            Nome = "Rinoceróide";
            Vantagem = "Pode atacar com seu cifre (Dano 1D6) em vez de usar a arma.";
        }

        public ConsequenciaDTO Efeito()
        {
            throw new NotImplementedException();
        }
    }
}
