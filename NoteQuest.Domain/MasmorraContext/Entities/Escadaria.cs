using NoteQuest.Domain.MasmorraContext.Interfaces;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Entities
{
    public class Escadaria : Segmento
    {
        public Escadaria(IPorta portaDeEntrada, string descricao) : base(portaDeEntrada, descricao)
        {
            Descricao = descricao;
            //TODO: 1 única porta com um nível abaixo
        }
    }
}