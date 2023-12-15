using NoteQuest.Domain.Core.Interfaces;

namespace NoteQuest.Domain.MasmorraContext.Interfaces
{
    public interface IArmadilhaFactory
    {
        public IEvent GeraArmadilha(IMasmorra masmorra, int? indice = null);
    }
}
