using NoteQuest.Domain.Core.Interfaces.Inventario.ItensEquipados;
using NoteQuest.Domain.Core.Interfaces.Personagem;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.ObjectValue;
using NoteQuest.Domain.Core.DTO;
using System.Collections.Generic;
using System;

namespace NoteQuest.Domain.Core.Classes
{
    public class Nobre : IClasse
    {
        public int Indice { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Pv { get; set; }
        public string Vantagem { get; set; }
        public IArma ArmaInicial { get; set; }
        public int QtdMagias { get; set; }
        public string EventTrigger { get; set; }

        public IEvent EffectSubstitutionComposite(IEvent gameEvent) => gameEvent;

        public void Build(/*IAcao acao*/)
        {
            //Acao = acao;
            Pv = 0;
            QtdMagias = 1;
            Nome = "Nobre";
            Vantagem = "Começa o jogo com +1 Magia Básica aleatória. Pode construir castelos.";
            ArmaInicial = new Arma();
            ArmaInicial.Build("Rapieira", +1);
            QtdMagias = 1;
        }

        public IEnumerable<ActionResult> Efeito(IEvent acao, int? indice = null)
        {
            throw new NotImplementedException();
        }
    }
}
