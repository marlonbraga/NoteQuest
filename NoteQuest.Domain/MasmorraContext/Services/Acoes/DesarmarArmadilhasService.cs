using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Masmorra.Services;
using System;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class DesarmarArmadilhasService : IDesarmarArmadilhasService
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public AcaoTipo AcaoTipo { get; set; }
        public Func<ConsequenciaDTO> Execucao { get; set; }

        public DesarmarArmadilhasService()
        {
            Titulo = "Desarmar armadilhas";
            Descricao = "Torna sala segura quanto a armadilhas. Ação demorada. Gasta 1 tocha";
            AcaoTipo = AcaoTipo.Segmento;
        }

        public ConsequenciaDTO Executar()
        {
            return null;
        }
    }
}
