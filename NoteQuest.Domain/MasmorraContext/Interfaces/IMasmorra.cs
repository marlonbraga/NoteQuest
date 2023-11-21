using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.MasmorraContext.Entities;

namespace NoteQuest.Domain.MasmorraContext.Interfaces
{
    public interface IMasmorra
    {
        public string Nome { get; set; }
        public string Descrição { get; set; }
        public BaseSegmento SalaFinal { get; set; }
        public int QtdPortasInexploradas { get; set; }
        public bool FoiConquistada { get; set; }
        public IPortaEntrada PortaEntrada { get; set; }
        public ConsequenciaDTO EntrarEmMasmorra();
    }
}
