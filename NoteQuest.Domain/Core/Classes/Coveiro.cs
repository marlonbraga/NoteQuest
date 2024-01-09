using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces.Inventario.ItensEquipados;
using NoteQuest.Domain.Core.Interfaces.Personagem;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.ObjectValue;
using System;
using System.Collections.Generic;

namespace NoteQuest.Domain.Core.Classes
{
    public class Coveiro : IClasse
    {
        public int Indice { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Pv { get; set; }
        public string Vantagem { get; set; }
        public IArma ArmaInicial { get; set; }
        public int Dano { get; set; }
        public int QtdMagias { get; set; }
        public Type TipoAcao { get; set; }
        public IEvent Acao { get; set; }
        public string EventTrigger { get; set; }

        public IEvent EffectSubstitutionComposite(IEvent gameEvent) => gameEvent;

        public void Build(/*IAcao acao*/)
        {
            //Acao = acao;
            Pv = 4;
            Nome = "Coveiro";
            Vantagem = "Causa +2 de dano em Mortos-Vivos.";
            ArmaInicial = new Arma();
            ArmaInicial.Build("Pá", -1);
            QtdMagias = 0;
        }

        public IEnumerable<ActionResult> Efeito(IEvent acao, int? indice = null)
        {
            //TODO
            throw new NotImplementedException();
        }
    }
}
