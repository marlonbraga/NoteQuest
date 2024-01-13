using NoteQuest.Domain.Core;
using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Inventario;
using NoteQuest.Domain.Core.Interfaces.Personagem;
using NoteQuest.Domain.ItensContext.Entities;
using NoteQuest.Domain.ItensContext.Interfaces;
using NoteQuest.Domain.ItensContext.ObjectValue;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class AbrirUmBau : IEvent
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public AcaoTipo AcaoTipo { get; set; }
        public IPersonagem Personagem { get; set; }
        public Func<IEnumerable<ActionResult>> Efeito { get; set; }
        public string EventTrigger { get; set; }
        public IDictionary<string, IEvent> ChainedEvents { get; set; }
        public IMasmorra Masmorra { get; set; }
        public IBau Bau { get; set; }

        public AbrirUmBau(BaseSegmento segmento)
        {
            EventTrigger = nameof(AbrirUmBau);
            Masmorra = segmento.Masmorra;
            Bau = (IBau)segmento.Conteudo?.Repositorio.FirstOrDefault(x => x.GetType() == typeof(Bau));
            Efeito = delegate { return Executar(); };
            Descricao = "Encontra moedas e tesouros; Raramente tem armadilhas.";
            Titulo = "Abrir Baú";
        }

        public IEnumerable<ActionResult> Executar(int? indice1 = null, int? indice2 = null)
        {
            string texto = string.Empty;
            texto += $"\n  Você abre o baú";

            if (Bau.EstaFechado)
            {
                indice1 ??= D6.Rolagem();
                indice2 ??= D6.Rolagem();
                int qtdMoedas = Math.Max((int)indice1, (int)indice2);
                int qtdTesouros = Math.Min((int)indice1, (int)indice2);

                IDictionary<int, IItem> conteudoBau = new Dictionary<int, IItem>(7);
                Cabidela cabidela = new(qtdMoedas);
                conteudoBau.Add(new KeyValuePair<int, IItem>(1, cabidela));
                for (int i = 1; i <= qtdTesouros; i++)
                {
                    IItem item = Masmorra.GeraItem();
                    conteudoBau.Add(new KeyValuePair<int, IItem>(i + 1, item));
                }
                Bau.EstaAberto = true;
                Bau.Conteudo = conteudoBau;
            }

            PersonagemConsequence consequencia = new(texto, Bau);
            IEnumerable<ActionResult> result = new List<ActionResult>() { consequencia };

            return result;
        }
    }
}
