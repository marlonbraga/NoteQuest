using NoteQuest.Domain.MasmorraContext.DTO;
using NoteQuest.Domain.MasmorraContext.Entities;
namespace NoteQuest.Domain.Core.Interfaces.Masmorra
{
    public interface ISegmentoInicial
    {
        public TabelaAPartirDe SegmentoSeguinte { get; set; }
    }
}
