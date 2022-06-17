using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.MasmorraContext.Interfaces.Services;
using System;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class SairDeMasmorraService : ISairDeMasmorraService
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public Func<ConsequenciaDTO> Execucao { get; set; }

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
