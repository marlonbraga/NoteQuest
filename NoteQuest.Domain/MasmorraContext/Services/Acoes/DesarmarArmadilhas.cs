using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Personagem;
using NoteQuest.Domain.MasmorraContext.Entities;
using System;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class DesarmarArmadilhas : IEvent
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public AcaoTipo AcaoTipo { get; set; }
        public string EventTrigger { get; set; }
        public IDictionary<string, IEvent> ChainedEvents { get; set; }
        public IPersonagem Personagem { get; set; }
        public Func<IEnumerable<ActionResult>> Efeito { get; set; }
        public BaseSegmento Sala { get; set; }

        public DesarmarArmadilhas(BaseSegmento sala)
        {
            Sala = sala;
            EventTrigger = nameof(DesarmarArmadilhas);
            Efeito = delegate { return Executar(); };
            Titulo = "Desarmar armadilhas";
            Descricao = "Ação demorada. Gasta 1 tocha";
        }

        public IEnumerable<ActionResult> Executar(int? indice = null)
        {
            string texto = $"\n  {Personagem?.Nome} procura por dispositivos que acionam armadilhas.";
            texto = "[grey]Não foi encontrada nenhuma armadilha[/]";
            Sala.Conteudo.Armadilhas = false;

            DungeonConsequence consequencia = new(texto, Sala);

            IEnumerable<ActionResult> result = new List<ActionResult>() { consequencia };
            return result;
        }
    }
}
