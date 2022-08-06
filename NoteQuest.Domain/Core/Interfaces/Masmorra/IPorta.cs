using NoteQuest.Domain.MasmorraContext.Entities;
using System.Collections.Generic;

namespace NoteQuest.Domain.Core.Interfaces.Masmorra
{
    public enum EstadoDePorta : int
    {
        inverificada = 0,
        aberta = 1,
        fechada = 2,
        quebrada = 3
    }
    public interface IPorta
    {
        public BaseSegmento SegmentoAtual { get; set; }
        public IList<IEscolha> Escolhas { get; set; }
        public Direcao Direcao { get; set; }
    }
}
