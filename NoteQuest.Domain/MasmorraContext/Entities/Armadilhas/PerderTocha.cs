using NoteQuest.Domain.Core;
using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Personagem;
using System;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Entities.Armadilhas
{
    public class PerderTocha : IEvent
    {
        public string Descricao { get; set; }
        public int QtdTochas { get; set; }
        public string Titulo { get; set; }
        public string EventTrigger { get; set; }
        public IPersonagem Personagem { get; set; }
        public Func<IEnumerable<ActionResult>> Efeito { get; set; }
        public IDictionary<string, IEvent> ChainedEvents { get; set; }

        public PerderTocha(string descricao, int tochas)
        {
            Efeito = delegate { return Executar(); };
            Descricao = descricao;
            QtdTochas = tochas;
        }

        public IEnumerable<ActionResult> Executar()
        {
            string texto = $"\n  {Descricao}";
            //TODO: Chamar evento de escuridão
            _ = Personagem.Inventario.GastarTochas((ushort)QtdTochas);

            ActionResult consequencia = new (texto);
            IEnumerable<ActionResult> result = new List<ActionResult>() { consequencia };

            return result;
        }
    }
}
