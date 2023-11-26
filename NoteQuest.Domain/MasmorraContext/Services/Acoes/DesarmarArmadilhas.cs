using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using System;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class DesarmarArmadilhas : IAcao
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public AcaoTipo AcaoTipo { get; set; }
        public GatilhoDeAcao GatilhoDeAcao { get; set; }
        public Func<ConsequenciaDTO> Execucao { get; set; }

        public DesarmarArmadilhas()
        {
            Titulo = "Desarmar armadilhas";
            Descricao = "Torna sala segura quanto a armadilhas. Ação demorada. Gasta 1 tocha";
        }

        public ConsequenciaDTO Executar(int? indice = null)
        {
            return null;
        }
    }
}
