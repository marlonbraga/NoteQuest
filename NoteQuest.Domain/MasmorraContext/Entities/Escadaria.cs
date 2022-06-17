using NoteQuest.Domain.Core.Interfaces;

namespace NoteQuest.Domain.MasmorraContext.Entities
{
    public class Escadaria : BaseSegmento
    {
        public Escadaria(ISegmentoBuilder segmentoFactory) : base(segmentoFactory)
        {
            //TODO: 1 única porta com um nível abaixo
        }
    }
}