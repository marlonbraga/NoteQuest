using NoteQuest.Domain.Core.DTO;
using System.Collections.Generic;

namespace NoteQuest.Domain.Core.Interfaces.Personagem
{
    public interface IEfeito
    {
        string EventTrigger { get; set; }
        IEvent EffectSubstitutionComposite(IEvent gameEvent);
        IEnumerable<ActionResult> Efeito(IEvent acao, int? indice = null);
    }
}
