using NoteQuest.Domain.MasmorraContext.Interfaces;

namespace NoteQuest.Domain.Core.Interfaces.Masmorra
{
    public interface IAbrirFechaduraService : IAcao
    {
        public IPortaComum Porta { get; set; }
    }
}
