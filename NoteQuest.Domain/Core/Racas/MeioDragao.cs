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
    public class MeioDragao : IRaca
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
            Pv = 30;
            QtdMagias = 0;
            Nome = "Meio Dragão";
            Vantagem = "Começa o jogo com 3 usos da Magia Básica: Bola de Fogo.";
        }

        public ConsequenciaDTO Efeito()
        {
            throw new NotImplementedException();
        }
    }
}
