using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using System;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class MoverEmSilencio : IAcao
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public AcaoTipo AcaoTipo { get; set; }
        public GatilhoDeAcao GatilhoDeAcao { get; set; }
        public Func<ConsequenciaDTO> Efeito { get; set; }

        public MoverEmSilencio()
        {
            GatilhoDeAcao = GatilhoDeAcao.MoverEmSilencio;
            Efeito = delegate { return Executar(); };
            Titulo = "Mover-se em silêncio";
            Descricao = "Tenta entrar em sala sem que os monstros te percebam. Se falhar, sofrerá ataque primeiro. Gasta 1 tocha";
        }

        public ConsequenciaDTO Executar(int? indice = null)
        {
            return null;
        }
    }
}
