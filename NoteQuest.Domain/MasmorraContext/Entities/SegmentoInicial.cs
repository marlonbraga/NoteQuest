using NoteQuest.Domain.Core.Acoes;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.ObjectValue;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;

namespace NoteQuest.Domain.MasmorraContext.Entities
{
    public class SegmentoInicial : BaseSegmento
    {
        public SegmentoTipo Segmento { get; set; }

        public SegmentoInicial(IPorta portaDeEntrada, int qtdPortas, string descricao, IMasmorraRepository masmorraRepository, ISegmentoFactory segmentoFactory) : base(descricao, qtdPortas)
        {
            IAcao acaoSairDeMasmorra = new SairDeMasmorra();
            IEscolha sairDeMasmorra = new Escolha(acaoSairDeMasmorra);
            portaDeEntrada = new PortaEntrada()
            {
                Escolhas = new() { sairDeMasmorra }
            };
            this.Portas.Add(portaDeEntrada);
        }
    }
}
