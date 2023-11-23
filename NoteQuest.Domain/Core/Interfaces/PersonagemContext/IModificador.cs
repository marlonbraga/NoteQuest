using NoteQuest.Domain.Core.Interfaces;

namespace NoteQuest.Domain.Core.Interfaces.PersonagemContext
{
    public interface IModificador
    {
        public IAcao AtualizarAcao(IAcao acao);
    }
}
