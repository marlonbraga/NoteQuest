using NoteQuest.Domain.Core.Interfaces.Inventario.ItensEquipados;
using NoteQuest.Domain.Core.Interfaces.Personagem;
using NoteQuest.Domain.Core.Interfaces;
using System;
using NoteQuest.Domain.Core.ObjectValue;
using NoteQuest.Domain.Core.DTO;
using System.Collections.Generic;

namespace NoteQuest.Domain.Core.Classes
{
    public class Mendigo : IClasse
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
            //Acao = null;
            Pv = 4;
            Nome = "Mendigo";
            Vantagem = "Nenhuma.";
            ArmaInicial = new Arma();
            ArmaInicial.Build("Pedaço de pau", -2);
            QtdMagias = 0;
        }

        public IEnumerable<ActionResult> Efeito(IEvent acao, int? indice = null)
        {
            throw new NotImplementedException();
        }
    }
}
