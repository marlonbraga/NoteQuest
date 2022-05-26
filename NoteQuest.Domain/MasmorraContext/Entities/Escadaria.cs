using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;

namespace NoteQuest.Domain.MasmorraContext.Entities
{
    public class Escadaria : BaseSegmento, ISegmento
    {
        public Escadaria(ISegmentoBuilder segmentoFactory) : base(segmentoFactory)
        {
            //TODO: 1 única porta com um nível abaixo
        }
    }
}