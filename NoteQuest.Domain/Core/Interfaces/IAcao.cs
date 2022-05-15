using NoteQuest.Domain.Core.DTO;

namespace NoteQuest.Domain.Core.Interfaces
{
    public interface IAcao
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }

        public ConsequenciaDTO Executar();
    }
}
