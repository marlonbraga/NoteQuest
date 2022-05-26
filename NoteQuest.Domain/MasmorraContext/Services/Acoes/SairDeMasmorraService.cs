using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Services;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class SairDeMasmorraService : ISairDeMasmorraService
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }

        public SairDeMasmorraService()
        {
            Titulo = "Sair de Masmorra";
            Descricao = "Pode voltar a cidade para se recuperar. Mas os montros da masmorra também restaurarão as energias.";
        }

        public ConsequenciaDTO Executar()
        {
            return null;
        }
    }
}
