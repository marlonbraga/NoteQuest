namespace NoteQuest.Domain.MasmorraContext.Interfaces.Dados
{
    public interface IMasmorraRepository
    {
        IMasmorraData PegarDadosMasmorra(string nomeMasmorra);
        IMasmorraNomes PegarNomesMasmorra();
    }
}
