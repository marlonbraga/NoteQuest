using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Personagem;
using NoteQuest.Domain.ItensContext.Entities;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class AcharPassagemSecreta : IEvent
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public AcaoTipo AcaoTipo { get; set; }
        public string EventTrigger { get; set; }
        public IDictionary<string, IEvent> ChainedEvents { get; set; }
        public IPersonagem Personagem { get; set; }
        public Func<IEnumerable<ActionResult>> Efeito { get; set; }
        public BaseSegmento Sala { get; set; }

        public AcharPassagemSecreta(BaseSegmento sala)
        {
            Sala = sala;
            EventTrigger = nameof(AcharPassagemSecreta);
            Efeito = delegate { return Executar(); };
            Titulo = "Procurar passagem secreta";
            Descricao = "Ação demorada. Gasta 1 tocha";
        }

        public IEnumerable<ActionResult> Executar(int? indice = null)
        {
            string texto = $"\n  {Personagem?.Nome} procura por passagens secretas.";
            texto = "[grey]Não foi encontrada nenhuma passagem secreta[/]";
            Sala.Conteudo.PassagemSecreta = false;

            DungeonConsequence consequencia = new(texto, Sala);

            IEnumerable<ActionResult> result = new List<ActionResult>() { consequencia };
            return result;
        }
    }
}
