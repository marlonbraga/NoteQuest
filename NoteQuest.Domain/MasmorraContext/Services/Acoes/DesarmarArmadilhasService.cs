using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.MasmorraContext.Interfaces.Services;
using System;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class DesarmarArmadilhasService : IDesarmarArmadilhasService
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public Func<ConsequenciaDTO> Execucao { get; set; }

        public DesarmarArmadilhasService()
        {
            Titulo = "Desarmar armadilhas";
            Descricao = "Torna sala segura quanto a armadilhas. Ação demorada. Gasta 1 tocha";
        }

        public ConsequenciaDTO Executar()
        {
            return null;
        }
    }
}
