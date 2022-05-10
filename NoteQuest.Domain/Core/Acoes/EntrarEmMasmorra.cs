using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using NoteQuest.Domain.MasmorraContext.ObjectValue;
using NoteQuest.Domain.MasmorraContext.Services;

namespace NoteQuest.Domain.Core.Acoes
{
    public class EntrarEmMasmorra : IAcao
    {
        public int Indice { get; set; }
        public IMasmorra Masmorra { get; set; }
        public IMasmorraRepository MasmorraRepository { get; set; }
        public IPortaEntrada PortaEntrada { get; set; }
        private IMasmorraData MasmorraData;
        public ISegmentoFactory SegmentoFactory { get; }

        public EntrarEmMasmorra(int indice, IMasmorraRepository masmorraRepository, ISegmentoFactory segmentoFactory, IPortaEntrada portaEntrada, IMasmorraData masmorraData)
        {
            Indice = indice;
            MasmorraData = masmorraData;
            MasmorraRepository = masmorraRepository;
            SegmentoFactory = segmentoFactory;
            PortaEntrada = portaEntrada;
        }
        public EntrarEmMasmorra(IMasmorra masmorra)
        {
            Masmorra = masmorra;
        }

        public ResultadoAcao executar()
        {
            BaseSegmento segmentoInicial = ((SegmentoFactory)SegmentoFactory).GeraSegmentoInicial();
            ResultadoAcao acao = new()
            {
                Descrição = $"{ MasmorraData.Descricao} + {segmentoInicial.Descricao}"
            };
            
            return acao;
        }
    }
}
