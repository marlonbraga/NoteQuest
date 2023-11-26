using NoteQuest.Domain.Core.Interfaces;

namespace NoteQuest.Domain.MasmorraContext.Interfaces
{
    public interface IAcaoPorta : IAcao
    {
        public IPortaComum Porta { get; set; }
        //public ISegmentoBuilder SegmentoFactory { get; set; }
    }
}
