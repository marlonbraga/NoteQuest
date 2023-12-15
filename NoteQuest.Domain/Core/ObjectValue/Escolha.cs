using NoteQuest.Domain.Core.Interfaces;

namespace NoteQuest.Domain.Core.ObjectValue
{
    public class Escolha : IEscolha
    {
        public IEvent Acao { get; set; }

        public Escolha(IEvent acao)
        {
            Acao = acao;
        }
    }
}
