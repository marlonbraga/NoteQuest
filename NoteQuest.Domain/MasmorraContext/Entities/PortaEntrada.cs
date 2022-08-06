using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Masmorra;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Entities
{
    public class PortaEntrada : IPortaEntrada
    {
        public int IdPorta { get; set; }
        public EstadoDePorta EstadoDePorta { get; set; }
        public Direcao Direcao { get; set; }
        public BaseSegmento SegmentoAtual { get; set; }
        public IList<IEscolha> Escolhas { get; set; }

        public PortaEntrada()
        {
            EstadoDePorta = EstadoDePorta.aberta;
            Escolhas = new List<IEscolha>();
        }
        public BaseSegmento Entrar()
        {
            //Sair de Masmorra
            return null;
        }
    }
}
