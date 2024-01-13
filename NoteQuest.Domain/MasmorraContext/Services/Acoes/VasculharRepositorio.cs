using NoteQuest.Domain.Core;
using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using NoteQuest.Domain.MasmorraContext.Services.Factories;
using System;
using System.Collections.Generic;
using NoteQuest.Domain.Core.Interfaces.Personagem;
using NoteQuest.Domain.Core.ObjectValue;
using NoteQuest.Domain.ItensContext.Interfaces;
using NoteQuest.Domain.ItensContext.Entities;
using System.Linq;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class VasculharRepositorio : IEvent
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string EventTrigger { get; set; }
        public Func<IEnumerable<ActionResult>> Efeito { get; set; }
        public IPersonagem Personagem {get; set; }
        public IDictionary<string, IEvent> ChainedEvents { get; set; }
        public IRepositorio RepositorioDeItens { get; set; }

        public VasculharRepositorio(BaseSegmento segmento, string titulo = "Pilhar itens da sala", string descricao = "Verifica objetos aparentes")
        {
            EventTrigger = nameof(VerificarPorta);
            Efeito = delegate { return Executar(); };
            ChainedEvents = new Dictionary<string, IEvent>();
            RepositorioDeItens = segmento.Conteudo?.Repositorio.FirstOrDefault(x => x.GetType() == typeof(RepositorioDeItens));
            Titulo = titulo;
            Descricao = descricao;
        }

        public IEnumerable<ActionResult> Executar(int? indicePorta = null, int? indiceArmadilha = null)
        {
            string texto = string.Empty;
            texto += $"\n  Você abre o baú";

            PersonagemConsequence consequencia = new(texto, RepositorioDeItens);
            IEnumerable<ActionResult> result = new List<ActionResult>() { consequencia };

            return result;
        }
    }
}
