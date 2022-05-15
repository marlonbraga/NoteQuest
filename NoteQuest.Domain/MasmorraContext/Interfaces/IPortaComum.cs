using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Entities;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Interfaces
{
    public interface IPortaComum : IPorta
    {
        public BaseSegmento SegmentoAlvo { get; set; }
        public EstadoDePorta EstadoDePorta { get; set; }

        public IPortaComum InvertePorta();
        public EstadoDePorta VerificarFechadura(int valorD6);
    }
}
