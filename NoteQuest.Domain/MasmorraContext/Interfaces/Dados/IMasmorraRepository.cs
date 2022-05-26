using NoteQuest.Domain.MasmorraContext.DTO;

namespace NoteQuest.Domain.MasmorraContext.Interfaces.Dados
{
    public interface IMasmorraRepository
    {
        public IMasmorraData DadosDeMasmorra { get; set; }

        public IMasmorraData PegarDadosMasmorra(string nomeMasmorra);
        public IMasmorraNomes PegarNomesMasmorra();
    }
}
