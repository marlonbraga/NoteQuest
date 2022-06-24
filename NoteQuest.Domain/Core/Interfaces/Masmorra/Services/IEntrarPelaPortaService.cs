using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Masmorra;

namespace NoteQuest.Domain.Core.Interfaces.Masmorra.Services
{
    public interface IEntrarPelaPortaService : IAcao
    {
        public IPortaComum Porta { get; set; }
        public ISegmentoBuilder SegmentoFactory { get; set; }
    }
}
