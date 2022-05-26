using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;

namespace NoteQuest.Domain.MasmorraContext.Interfaces
{
    public interface IMasmorra
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public IPortaEntrada PortaEntrada { get; set; }
        public ISegmentoInicial SegmentoInicial { get; set; }
        public IMasmorraRepository MasmorraRepository { get; set; }

        public void Build(int indice1, int indice2, int indice3);
        public string GerarNome(int indice1, int indice2, int indice3);
    }
}
