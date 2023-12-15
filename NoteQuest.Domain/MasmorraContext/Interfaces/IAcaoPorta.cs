using NoteQuest.Domain.Core.Interfaces;

namespace NoteQuest.Domain.MasmorraContext.Interfaces
{
    public interface IAcaoPorta : IEvent
    {
        public IPortaComum Porta { get; set; }
    }
}
