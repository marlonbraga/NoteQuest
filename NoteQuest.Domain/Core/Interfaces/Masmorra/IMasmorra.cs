using NoteQuest.Domain.Core.Interfaces.Masmorra;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;

namespace NoteQuest.Domain.Core.Interfaces.Masmorra
{
    public interface IMasmorra
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public IPortaEntrada PortaEntrada { get; set; }
        public ISegmentoInicial SegmentoInicial { get; set; }
        public IClasseBasicaRepository MasmorraRepository { get; set; }

        public void Build(ushort indice1, ushort indice2, ushort indice3);
        public string GerarNome(ushort indice1, ushort indice2, ushort indice3);
    }
}
