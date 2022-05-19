using NoteQuest.Domain.MasmorraContext.DTO;

namespace NoteQuest.Domain.MasmorraContext.Interfaces.Dados
{
    public interface IMasmorraRepository
    {
        public IMasmorraData PegarDadosMasmorra(string nomeMasmorra);
    }
}
