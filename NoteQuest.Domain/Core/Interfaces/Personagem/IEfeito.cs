namespace NoteQuest.Domain.Core.Interfaces.Personagem
{
    public interface IEfeito
    {
        //public IEvent AplicaEfeito(IEvent acao);
        IEvent EffectSubstitutionComposite(IEvent gameEvent);
    }
}
