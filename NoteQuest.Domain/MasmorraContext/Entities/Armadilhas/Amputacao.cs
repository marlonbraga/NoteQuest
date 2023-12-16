using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Personagem;
using System;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Entities.Armadilhas
{
    public class Amputacao : IEvent
    {
        public string Descricao { get; set; }
        public string Titulo { get; set; }
        public string EventTrigger { get; set; }
        public IPersonagem Personagem { get; set; }
        public Func<IEnumerable<ActionResult>> Efeito { get; set; }
        public IDictionary<string, IEvent> ChainedEvents { get; set; }

        public Amputacao(string descricao)
        {
            Efeito = delegate { return Executar(); };
            Descricao = descricao;
        }

        public IEnumerable<ActionResult> Executar()
        {
            string texto = $"\n  {Descricao}";
            texto += $"\n  {Personagem.Nome} somente a percebeu quando parou de sentir sua prória mão.";
            texto += $"\n  {Personagem.Nome} teve seu braço decepado! (Limitando a quantidade de itens que pode segurar ao mesmo tempo)";
            if (Personagem.Inventario.Equipamentos.MaoEsquerda is not null)
                Personagem.Inventario.Equipamentos.MaoEsquerda = null;
            else
                Personagem.Inventario.Equipamentos.MaoDireita = null;

            ActionResult consequencia = new (texto);
            IEnumerable<ActionResult> result = new List<ActionResult>() { consequencia };

            return result;
        }
    }
}
