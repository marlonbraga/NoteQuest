using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;

namespace NoteQuest.Domain.MasmorraContext.Entities
{
    public class Corredor : BaseSegmento, ISegmento
    {
        public Corredor(ISegmentoBuilder segmentoFactory) : base(segmentoFactory)
        {

        }
    }
}