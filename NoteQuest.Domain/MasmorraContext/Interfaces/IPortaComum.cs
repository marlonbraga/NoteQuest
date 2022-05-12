using NoteQuest.Domain.MasmorraContext.Entities;

namespace NoteQuest.Domain.MasmorraContext.Interfaces
{
    public interface IPortaComum : IPorta
    {
        public IPortaComum InvertePorta();

        public BaseSegmento SegmentoAlvo { get; set; }
    }
}
