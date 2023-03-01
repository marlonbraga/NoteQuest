using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Masmorra;
using NoteQuest.Domain.Core.Interfaces.Masmorra.Services;
using System;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class QuebrarPortaService : IQuebrarPortaService
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public AcaoTipo AcaoTipo { get; set; }
        public Func<ConsequenciaDTO> Execucao { get; set; }

        public QuebrarPortaService(IPortaComum porta)
        {
            Titulo = "Quebrar porta";
            Descricao = "Abre acesso a sala trancada sem gastar tochas. Se houver monstros, sofrerá ataque primeiro.";
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
