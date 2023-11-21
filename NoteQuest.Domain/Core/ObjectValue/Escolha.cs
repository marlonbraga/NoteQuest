using NoteQuest.Domain.Core.Interfaces;

namespace NoteQuest.Domain.Core.ObjectValue
{
    public class Escolha : IEscolha
    {
        public IAcao Acao { get; set; }

        public Escolha(IAcao acao)
        {
            Acao = acao;
        }
    }
}
