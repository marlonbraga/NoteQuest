using NoteQuest.Domain.Core.DTO;

namespace NoteQuest.Domain.MasmorraContext.Interfaces
{
    public interface IMasmorra
    {
        public string Descrição { get; set; }
        public int PortasInexploradas { get; set; }
        public bool FoiConquistada { get; set; }
        public IPortaEntrada PortaEntrada { get; set; }
        public ConsequenciaDTO EntrarEmMasmorra();
    }
}
