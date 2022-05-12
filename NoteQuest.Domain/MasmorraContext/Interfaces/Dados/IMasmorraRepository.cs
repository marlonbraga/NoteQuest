using NoteQuest.Domain.MasmorraContext.DTO;

namespace NoteQuest.Domain.MasmorraContext.Interfaces.Dados
{
    public interface IMasmorraRepository
    {
        public MasmorraDataDTO PegarDadosMasmorra(string nomeMasmorra);
    }
}
