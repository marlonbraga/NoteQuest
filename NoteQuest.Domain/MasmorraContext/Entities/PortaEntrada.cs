using NoteQuest.Domain.Core;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Services;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Entities
{
    public class PortaEntrada : IPortaEntrada
    {
        public int IdPorta { get; set; }
        public EstadoDePorta EstadoDePorta { get; set; }
        public Posicao Posicao { get; set; }
        public BaseSegmento SegmentoAtual { get; set; }

        public PortaEntrada()
        {
            EstadoDePorta = EstadoDePorta.aberta;
        }
        public BaseSegmento Entrar()
        {
            //Sair de Masmorra
            return null;
        }
    }
}
