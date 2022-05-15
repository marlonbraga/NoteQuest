using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;

namespace NoteQuest.Domain.Core.Acoes
{
    public class AbrirFechadura : IAcao
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }

        public AbrirFechadura()
        {
            Titulo = "Abrir fechadura";
            Descricao = "Abre acesso a sala trancada sem alertar monstros. Ação demorada. Gasta 1 tocha";
        }

        public ConsequenciaDTO Executar()
        {
            return null;
        }
    }
}
