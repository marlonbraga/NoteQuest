using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Masmorra;
using NoteQuest.Domain.Core.Interfaces.Masmorra.Services;
using System;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class MoverEmSilencioService : IMoverEmSilencioService
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public AcaoTipo AcaoTipo { get; set; }
        public Func<ConsequenciaDTO> Execucao { get; set; }

        public MoverEmSilencioService(IPortaComum porta)
        {
            Titulo = "Mover-se em silêncio";
            Descricao = "Tenta entrar em sala sem que os monstros te percebam. Se falhar, sofrerá ataque primeiro. Gasta 1 tocha";
            AcaoTipo = ObtemAcaoTipoPorPorta(porta.Direcao);
        }

        private AcaoTipo ObtemAcaoTipoPorPorta(Direcao direcao)
        {
            return direcao switch
            {
                Direcao.frente => AcaoTipo.PortaTras,
                Direcao.direita => AcaoTipo.PortaEsquerda,
                Direcao.tras => AcaoTipo.PortaFrente,
                Direcao.esquerda => AcaoTipo.PortaDireita,
                _ => AcaoTipo.Segmento,
            };
        }

        public ConsequenciaDTO Executar()
        {
            return null;
        }
    }
}
