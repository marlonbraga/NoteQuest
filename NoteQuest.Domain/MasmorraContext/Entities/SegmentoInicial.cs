using NoteQuest.Domain.MasmorraContext.Services.Acoes;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.ObjectValue;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;

namespace NoteQuest.Domain.MasmorraContext.Entities
{
    public class SegmentoInicial : BaseSegmento, ISegmentoInicial
    {
        public SegmentoTipo Segmento { get; set; }

        public SegmentoInicial(int qtdPortas, string descricao, IMasmorraRepository masmorraRepository, ISegmentoFactory segmentoFactory) : base(descricao, qtdPortas)
        {
            IAcao acaoSairDeMasmorra = new SairDeMasmorra();
            IEscolha sairDeMasmorra = new Escolha(acaoSairDeMasmorra);
            IPorta portaDeEntrada = new PortaEntrada()
            {
                Escolhas = new() { sairDeMasmorra }
            };
            this.Portas.Add(portaDeEntrada);
        }
    }
}
