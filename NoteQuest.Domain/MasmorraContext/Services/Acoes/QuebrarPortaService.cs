using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces.Masmorra;
using NoteQuest.Domain.MasmorraContext.Interfaces.Services;
using System;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class QuebrarPortaService : IQuebrarPortaService
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public Func<ConsequenciaDTO> Execucao { get; set; }

        public QuebrarPortaService(IPortaComum porta)
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
