namespace NoteQuest.Domain.MasmorraContext.Interfaces.Dados
{
    public interface IClasseBasicaRepository
    {
        public IMasmorraData DadosDeMasmorra { get; set; }

        public IMasmorraData PegarDadosMasmorra(string nomeMasmorra);
        public IMasmorraNomes PegarNomesMasmorra();
    }
}
