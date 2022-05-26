using NoteQuest.Domain.Core.Interfaces;

namespace NoteQuest.Domain.MasmorraContext.Interfaces.Services
{
    public interface IEntrarPelaPortaService : IAcao
    {
        public IPortaComum Porta { get; set; }
        public ISegmentoBuilder SegmentoFactory { get; set; }
    }
}
