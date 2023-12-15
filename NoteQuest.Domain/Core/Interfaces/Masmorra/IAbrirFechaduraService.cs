using NoteQuest.Domain.MasmorraContext.Interfaces;

namespace NoteQuest.Domain.Core.Interfaces.Masmorra
{
    public interface IAbrirFechaduraService : IEvent
    {
        public IPortaComum Porta { get; set; }
    }
}
