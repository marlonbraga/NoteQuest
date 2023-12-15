using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Personagem;
using System;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class SairDeMasmorra : IEvent
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public AcaoTipo AcaoTipo { get; set; }
        public string EventTrigger { get; set; }
        public IDictionary<string, IEvent> ChainedEvents { get; set; }
        public IPersonagem Personagem { get; set; }
        public Func<IEnumerable<ActionResult>> Efeito { get; set; }
        public SairDeMasmorra()
        {
            EventTrigger = nameof(SairDeMasmorra);
            Efeito = delegate { return Executar(); };
            Titulo = "Sair de Masmorra";
            Descricao = "Voltar para a cidade";
        }

        public IEnumerable<ActionResult> Executar(int? indice = null)
        {
            Console.WriteLine("[[Opção em desenvolvimento]]");
            return null;
        }
    }
}
