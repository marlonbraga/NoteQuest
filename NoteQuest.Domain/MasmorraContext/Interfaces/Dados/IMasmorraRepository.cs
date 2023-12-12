namespace NoteQuest.Domain.MasmorraContext.Interfaces.Dados
{
    public interface IMasmorraRepository
    {
        public IMasmorraData DadosDeMasmorra { get; set; }
        IMasmorraData PegarDadosMasmorra(string nomeMasmorra = "Palacio");
        IMasmorraNomes PegarNomesMasmorra();
    }
}
