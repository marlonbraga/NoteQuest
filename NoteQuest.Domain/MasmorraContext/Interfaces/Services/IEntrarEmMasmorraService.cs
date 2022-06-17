using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Masmorra;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;

namespace NoteQuest.Domain.MasmorraContext.Interfaces.Services
{
    public interface IEntrarEmMasmorraService : IAcao
    {
        public IMasmorra Masmorra { get; set; }
        public IClasseBasicaRepository MasmorraRepository { get; set; }
        public IPortaEntrada PortaEntrada { get; set; }
        public ISegmentoBuilder SegmentoBuilder { get; set; }
        public void Build(IMasmorra masmorra);
    }
}
