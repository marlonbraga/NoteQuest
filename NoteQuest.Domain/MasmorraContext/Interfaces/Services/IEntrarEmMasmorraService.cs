using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;

namespace NoteQuest.Domain.MasmorraContext.Interfaces.Services
{
    public interface IEntrarEmMasmorraService : IAcao
    {
        public IMasmorra Masmorra { get; set; }
        public IMasmorraRepository MasmorraRepository { get; set; }
        public IPortaEntrada PortaEntrada { get; set; }
        public ISegmentoBuilder SegmentoBuilder { get; set; }
    }
}
