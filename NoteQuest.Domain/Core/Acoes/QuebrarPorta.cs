using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;

namespace NoteQuest.Domain.Core.Acoes
{
    public class QuebrarPorta : IAcao
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }

        public QuebrarPorta()
        {
            Titulo = "Quebrar porta";
            Descricao = "Abre acesso a sala trancada sem gastar tochas. Se houver monstros, sofrerá ataque primeiro.";
        }

        public ConsequenciaDTO Executar()
        {
            return null;
        }
    }
}
