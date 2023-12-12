using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Personagem;
using System;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class DesarmarArmadilhas : IAcao
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public AcaoTipo AcaoTipo { get; set; }
        public GatilhoDeAcao GatilhoDeAcao { get; set; }
        public IPersonagem Personagem { get; set; }
        public Func<IEnumerable<ActionResult>> Efeito { get; set; }

        public DesarmarArmadilhas()
        {
            GatilhoDeAcao = GatilhoDeAcao.DesarmarArmadilha;
            Efeito = delegate { return Executar(); };
            Titulo = "Desarmar armadilhas";
            Descricao = "Torna sala segura quanto a armadilhas. Ação demorada. Gasta 1 tocha";
        }

        public IEnumerable<ActionResult> Executar(int? indice = null)
        {
            return null;
        }
    }
}
