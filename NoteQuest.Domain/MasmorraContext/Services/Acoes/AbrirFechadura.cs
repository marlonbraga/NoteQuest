using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class AbrirFechadura : IAcao
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }

        public AbrirFechadura(int indice, IPortaComum porta)
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
