using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.ObjectValue;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Services.Acoes;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Entities
{
    public class PortaEntrada : IPortaEntrada
    {
        public int IdPorta { get; set; }
        public EstadoDePorta EstadoDePorta { get; set; }
        public Posicao Posicao { get; set; }
        public BaseSegmento SegmentoAtual { get; set; }
        public BaseSegmento SegmentoInicial { get; set; }
        public List<IEscolha> Escolhas { get; set; }
        public int Andar { get; set; }
        public IMasmorra Masmorra { get; set; }

        public PortaEntrada()
        {
            EstadoDePorta = EstadoDePorta.aberta;
            Escolhas = AbrirPorta();
            Andar = 0;
        }

        public List<IEscolha> AbrirPorta()
        {
            return SairDaMasmorra();
        }

        private List<IEscolha> SairDaMasmorra()
        {
            return null;
        }
    }
}
