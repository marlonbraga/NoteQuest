using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using System;

namespace NoteQuest.Domain.Core.Interfaces
{
    public interface ISegmentoFactory
    {
        public Tuple<string, BaseSegmento> GeraSegmentoInicial();
        public BaseSegmento GeraSegmento(IPortaComum portaDeEntrada, int indice);
    }
}
