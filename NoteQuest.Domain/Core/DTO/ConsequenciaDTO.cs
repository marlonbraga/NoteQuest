using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Masmorra;
using System.Collections.Generic;

namespace NoteQuest.Domain.Core.DTO
{
    public class ConsequenciaDTO
    {
        public string Descricao { get; set; }
        public ISegmento Segmento { get; set; }
        public IList<IEscolha> Escolhas { get; set; }
        public IDictionary<int, IEscolha> EscolhasNumeradas
        {
            get
            {
                IDictionary<int, IEscolha> escolhaNumerada = new Dictionary<int, IEscolha>();
                int key = 1;
                foreach (var escolha in Escolhas)
                {
                    escolhaNumerada.Add(new KeyValuePair<int, IEscolha>(key, escolha));
                    key++;
                }

                return escolhaNumerada;
            }
        }
    }
}
