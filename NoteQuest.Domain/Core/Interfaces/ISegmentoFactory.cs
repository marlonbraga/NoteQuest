using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using System;

namespace NoteQuest.Domain.Core.Interfaces
{
    public interface ISegmentoFactory
    {
        public Tuple<string, BaseSegmento> GeraSegmentoInicial();
        public BaseSegmento GeraSegmento(IPortaComum portaDeEntrada, int indice);
        void Instancia(IMasmorraRepository masmorraRepository, object indice);
    }
}
